using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Model;
using MagicVilla_VillaAPI.Model.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MagicVilla_VillaAPI.Controllers.v1;
[Route("api/v{version:apiVersion}/VillaAPI")]
[ApiController]
[ApiVersion("1.0")]
public class VillaAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IVillaRepository _dbVilla;
    private readonly IMapper _mapper;
    public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
    {
        _dbVilla = dbVilla;
        _mapper = mapper;
        _response = new APIResponse();
    }

    [HttpGet]
    [ResponseCache(CacheProfileName="Default30")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetVillas([FromQuery(Name ="filterOccupancy")]int? occupancy,
        [FromQuery] string? search, int pagesize=0,int pageNumber=1)
    {
        try
        {
            IEnumerable<Villa> villaList;
            if (occupancy >0) 
            {
                villaList=await _dbVilla.GetAllAsync(u=>u.Occupancy==occupancy,pageSize:pagesize,pageNumber:pageNumber);
            }
            else
            {
                villaList = await _dbVilla.GetAllAsync(pageSize: pagesize, pageNumber: pageNumber);
            }
            if (!string.IsNullOrEmpty(search)) 
            {
                villaList=villaList.Where(u=>u.Name.ToLower().Contains(search));
            }
            Pagination pagination = new()
            {
                PageNumger=pageNumber,
                PageSize=pagesize
            };
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagination));
            _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;

        }

    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    [ResponseCache(Duration = 30)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetVilla(int id)
    {
        try
        {
            if (id == 0)
            {

                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }

    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDTO createDTO)
    {
        try
        {
            if (await _dbVilla.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            Villa villa = _mapper.Map<Villa>(createDTO);

            await _dbVilla.CreateAsync(villa);
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = System.Net.HttpStatusCode.Created;
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }

    }

    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            await _dbVilla.RemoveAsync(villa);
            await _dbVilla.SaveAsync();

            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }

    }
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]

    public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDTO)
    {
        try
        {
            if (id == 0 || id != updateDTO.Id)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Villa model = _mapper.Map<Villa>(updateDTO);

            await _dbVilla.UpdateAsync(model);
            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }


    }
    [HttpPatch("{id:int}", Name = "UdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<APIResponse>> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
    {
        try
        {
            if (patchDTO == null || id == 0)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id, tracked: false);
            if (villa == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = _mapper.Map<Villa>(villaDTO);
            await _dbVilla.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }

    }

}
