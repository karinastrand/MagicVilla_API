﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Model;
using MagicVilla_VillaAPI.Model.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;

namespace MagicVilla_VillaAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private string secretKey;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    public UserRepository(ApplicationDbContext db,IConfiguration configuration, 
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public bool IsUniqueUser(string username)
    {
        var user=_db.Users.FirstOrDefault(x=>x.UserName == username);
        if(user == null) 
        {
            return true;
        }
        return false;
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    {
        var user=_db.ApplicationUsers.FirstOrDefault(u=>u.UserName.ToLower()==loginRequestDTO.UserName.ToLower());
        bool isValid=await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
        if(user == null || isValid==false) 
        {
            return new LoginResponseDTO()
            {
                Token = "",
                User = null
            };
        }
        var roles=await _userManager.GetRolesAsync(user);    
        var tokenHandler = new JwtSecurityTokenHandler();
        var key=Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
               }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
        {
            Token = tokenHandler.WriteToken(token),
            User = _mapper.Map<UserDTO>(user),
           
        };
        return loginResponseDTO;
    }

    public async Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
    {
        ApplicationUser user = new ApplicationUser()
        {
            UserName = registerationRequestDTO.UserName,
            Email=registerationRequestDTO.UserName,
            NormalizedEmail=registerationRequestDTO.UserName.ToLower(),
            Name = registerationRequestDTO.Name,
         
        };
        try
        {
            var result=await _userManager.CreateAsync(user, registerationRequestDTO.Password);
            if (result.Succeeded) 
            {
                if(!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                    await _roleManager.CreateAsync(new IdentityRole("customer"));
                }
                await _userManager.AddToRoleAsync(user, "admin");
                var userToReturn=_db.ApplicationUsers.FirstOrDefault(u=>u.UserName == registerationRequestDTO.UserName);
                return _mapper.Map<UserDTO>(userToReturn);
            }
        }
        catch (Exception e)
        {
            
        }
      
        return new UserDTO();
    }
}
