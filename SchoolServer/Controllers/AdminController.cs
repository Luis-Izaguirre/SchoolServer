﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using SchoolServer.DTO;
using SchoolServer.Models;

namespace SchoolServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<CourseUser> userManager,
        JwtHandler jwtHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Loggin(LogginRequest logginRequest)
        {
            CourseUser? user = await userManager.FindByNameAsync(logginRequest.UserName);
            if (user == null)
            {
                return Unauthorized("Bad username :(");
            }
            bool success = await userManager.CheckPasswordAsync(user, logginRequest.Password);
            if (!success)
            {
                return Unauthorized("Wrong password :/");
            }
            JwtSecurityToken token = await jwtHandler.GetTokenAsync(user);
            string jwtString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new LogginResult
            {
                Success = true,
                Message = "Sweettt!",
                Token = jwtString,
            });
        }
    }
}