﻿namespace MagicVilla_VillaAPI.Model.Dto;

public class LoginResponseDTO
{
    public  UserDTO User { get; set; }
    public string Token { get; set; }
}
