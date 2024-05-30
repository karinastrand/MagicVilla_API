﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Model;
using MagicVilla_VillaAPI.Model.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers;
[Route("api/VillaAPI")]
[ApiController]
public class VillaAPIController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public VillaAPIController(ApplicationDbContext db)
    {
        _db = db;    
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
    {
      
        return Ok(await _db.Villas.ToListAsync());

    }
    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetVilla(int id)
    {
        if (id == 0)
        {

            return BadRequest();
        }
        var villa =await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateVilla([FromBody] VillaCreateDTO villaDTO)
    {
        if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already exists!");
            return BadRequest(ModelState);
        }

        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
       
        Villa model = new()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
           
            ImageUrl = villaDTO.ImageUrl,
            Name = villaDTO.Name,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft
        };
        await  _db.Villas.AddAsync(model);
        await _db.SaveChangesAsync();
        return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
    }
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa =await  _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        _db.Villas.Remove(villa);
        await _db.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut("{id:int}",Name ="UdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
    {
        if (id == 0 || id != villaDTO.Id)
        {
            return BadRequest();
        }
        var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
        Villa model = new()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            Id = villaDTO.Id,
            ImageUrl = villaDTO.ImageUrl,
            Name = villaDTO.Name,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft
        };
        _db.Update(model);
        await _db.SaveChangesAsync();
        return NoContent();
    }
    [HttpPatch("{id:int}", Name = "UdatePartialVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult>  UpdatePartialVilla(int id,JsonPatchDocument<VillaUpdateDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }
        var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }
        VillaUpdateDTO villaDTO = new()
        {
            Amenity = villa.Amenity,
            Details = villa.Details,
            Id = villa.Id,
            ImageUrl = villa.ImageUrl,
            Name = villa.Name,
            Occupancy = villa.Occupancy,
            Rate = villa.Rate,
            Sqft = villa.Sqft
        };
        patchDTO.ApplyTo(villaDTO, ModelState);
        Villa model = new()
        {
            Amenity = villaDTO.Amenity,
            Details = villaDTO.Details,
            Id = villaDTO.Id,
            ImageUrl = villaDTO.ImageUrl,
            Name = villaDTO.Name,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqft = villaDTO.Sqft
        };
        _db.Villas.Update(model);
        await _db.SaveChangesAsync();
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        return NoContent() ;
    }

}