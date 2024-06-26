﻿using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.VM;
using MagicVilla_Web.Services;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using MagicVilla_Web.Models.VM;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using MagicVilla_Utility;
namespace MagicVilla_Web.Controllers;

public class VillaNumberController : Controller
{
    private readonly IVillaNumberService _villaNumberService;
    private readonly IVillaService _villaService;
    private readonly IMapper _mapper;
    public VillaNumberController(IVillaNumberService villaNumberService,IVillaService villaService, IMapper mapper)
    {
        _villaNumberService = villaNumberService;
        _villaService = villaService;
        _mapper = mapper;
    }
    public async Task<IActionResult> IndexVillaNumber()
    {
        List<VillaNumberDTO> list = new List<VillaNumberDTO>();
        var response = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
        }
        return View(list);
    }
    [Authorize(Roles ="admin")]
    public async Task<IActionResult> CreateVillaNumber()
    {
        VillaNumberCreateVM villaNumberVM= new();
        
        var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(response.Result)).Select(i=>new SelectListItem
                {
                    Text=i.Name,
                    Value=i.Id.ToString()
                });   
        }
        
        return View(villaNumberVM);
    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
    {

        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Number created successfully";
               
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            else
            {
                if(response.ErrorMessages.Count>0)
                {
                    ModelState.AddModelError("ErrorMessages",response.ErrorMessages.FirstOrDefault());
                }
            }
        }
        var villaResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        VillaNumberCreateVM villaNumberVM = new();
        if (villaResponse != null && villaResponse.IsSuccess)
        {
            model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(villaResponse.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
        }
        TempData["error"] = "Error encountered";
        return View(model);
    }
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateVillaNumber(int VillaNo)
    {
        VillaNumberUpdateVM villaNumberVM = new();
        var response=await _villaNumberService.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));  
        
        if (response != null && response.IsSuccess)
        {
            VillaNumberDTO model=JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
            villaNumberVM.VillaNumber = _mapper.Map<VillaNumberUpdateDTO>(model);
               
        }
        var villaResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        if (villaResponse != null && villaResponse.IsSuccess)
        {
            villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(villaResponse.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            return View(villaNumberVM);
        }

   
        return NotFound();
    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
    {

        if (ModelState.IsValid)
        {
            var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Number updated successfully";
              
                return RedirectToAction(nameof(IndexVillaNumber));
            }
            else
            {
                if (response.ErrorMessages.Count > 0)
                {
                    ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                }
            }
        }
        var villaResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        VillaNumberCreateVM villaNumberVM = new();
        if (villaResponse != null && villaResponse.IsSuccess)
        {
            model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(villaResponse.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
        }
        TempData["error"] = "Error encountered";
        return View(model);
    }
    
    public async Task<IActionResult> DeleteVillaNumber(int VillaNo)
    {
        VillaNumberDeleteVM villaNumberVM = new();
        var response = await _villaNumberService.GetAsync<APIResponse>(VillaNo, HttpContext.Session.GetString(SD.SessionToken));

        if (response != null && response.IsSuccess)
        {
            VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
            villaNumberVM.VillaNumber = model;

        }
        var villaResponse = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        if (villaResponse != null && villaResponse.IsSuccess)
        {
            villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                (Convert.ToString(villaResponse.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            return View(villaNumberVM);
        }


        return NotFound();
    }
    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
    {
        
        var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "Villa Number created successfully";
           
            return RedirectToAction(nameof(IndexVillaNumber));
        }
        TempData["error"] = "Error encountered";
        return View(model);
    }
}
