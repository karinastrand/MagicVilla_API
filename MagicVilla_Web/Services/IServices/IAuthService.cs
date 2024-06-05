﻿using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Identity.Data;

namespace MagicVilla_Web.Services.IServices;

public interface IAuthService
{
    Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
    Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate);
}
