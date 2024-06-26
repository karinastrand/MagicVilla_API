﻿using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Model;
using MagicVilla_VillaAPI.Model.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaNumberAPI.Controllers.v1;
[Route("api/v{version:apiVersion}/VillaNumberAPI")]
[ApiController]
[ApiVersion("1.0")]


public class VillaNumberAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IVillaNumberRepository _dbVillaNumber;
    private readonly IVillaRepository _dbVilla;
    private readonly IMapper _mapper;
    public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
    {
        _dbVillaNumber = dbVillaNumber;    
        _mapper = mapper;
        this._response= new APIResponse();
        _dbVilla = dbVilla;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetVillaNumbers()
    {
        try
        {
            IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync(includeProperties:"Villa");
            _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
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
  

    [HttpGet("{id:int}", Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {

                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
            if (villaNumber == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
    {
        try
        {
            if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDTO.VillaNo)!=null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa already exists!");
                return BadRequest(ModelState);
            }

            if(await _dbVilla.GetAsync(u=>u.Id==createDTO.VillaID)==null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDTO);

            await _dbVillaNumber.CreateAsync(villaNumber);
            _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
            _response.StatusCode = System.Net.HttpStatusCode.Created;
            return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
            return _response;
        }
       
    }

    [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode=System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
            if (villaNumber == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            await _dbVillaNumber.RemoveAsync(villaNumber);
            await _dbVillaNumber.SaveAsync();

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
    [HttpPut("{id:int}",Name ="UpdateVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
    {
        try
        {
            if (id == 0 || id != updateDTO.VillaNo)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (await _dbVilla.GetAsync(u => u.Id == updateDTO.VillaID) == null)
            {
                ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid");
                return BadRequest(ModelState);
            }

            VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);

            await _dbVillaNumber.UpdateAsync(model);
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
