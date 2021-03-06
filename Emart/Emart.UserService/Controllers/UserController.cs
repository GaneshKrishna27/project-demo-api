﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Emart.UserService.Models;
using Emart.UserService.Repositories;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Emart.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IConfiguration configuration;
        public UserController(IUserRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            this.configuration = configuration;
        }
       
        
        [HttpGet]
        [Route("BuyerSignin/{username}/{password}")]
        public IActionResult BuyerSignin(string username, string password)
        {
            Token token = null;
            try
            {
                Buyer buyer = _repo.BuyerSignin(username, password);
                if(buyer!=null)
                {
                    token = new Token() { Bid = buyer.Bid, token = GenerateJwtToken(username), msg = "Success" };
                }
                else
                {
                    token = new Token() { token = "", msg = "Unsuccess" };
                }
                return Ok(token);
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException.Message);
            }
        }
        private string GenerateJwtToken(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role,username)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // recommended is 5 min
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));
            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            //var response = new Token
            //{
            //    username = username,
            //    token = new JwtSecurityTokenHandler().WriteToken(token)
            //};
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet]
        [Route("SellerSignin/{username}/{password}")]
        public IActionResult SellerSignin(string username, string password)
        {
            Token token = null;
            try
            {
                Seller seller = _repo.SellerSignin(username, password);
                if (seller != null)
                {
                    token = new Token() { Sid = seller.Sid, token = GenerateJwtToken(username), msg = "Success" };
                }
                else
                {
                    token = new Token() { token = "", msg = "Unsuccess" };
                }
                return Ok(token);
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException.Message);
            }
        }
        [HttpPost]
        [Route("BuyerSignup")]
        public IActionResult BuyerSignup(Buyer obj)
        {
            try
            {
                _repo.BuyerSignup(obj);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException.Message);
            }
        }
        [HttpPost]
        [Route("SellerSignup")]
        public IActionResult SellerSignup(Seller obj)
        {
            try
            {
                _repo.SellerSignup(obj);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException.Message);
            }
        }
    }
}