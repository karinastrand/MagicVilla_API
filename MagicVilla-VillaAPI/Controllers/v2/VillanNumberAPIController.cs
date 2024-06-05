using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Model;
using MagicVilla_VillaAPI.Model.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaNumberAPI.Controllers.v2;
[Route("api/v{version:apiVersion}/VillaNumberAPI")]
[ApiController]
[ApiVersion("2.0")]

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

    [HttpGet("GetString")]
    public IEnumerable<string> Get()
    {
        return new string[] { "string1", "DotNetMastery" };
    }




}
