using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrgChartApi.Configuration;
using OrgChartApi.Models;
using OrgChartApi.Models.DTOs.Requests;
using OrgChartApi.Models.DTOs.Responses;

namespace OrgChartApi.Controllers
{
    [Route("v1/[controller]")] // api/authManagement
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly OrgChartContext _context;


        public AuthManagementController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if(ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByNameAsync(user.Username);

                if(user.Password != user.ConfirmPassword)
                {
                    return BadRequest(new UserLoginResponse(){
                            Errors = new List<string>() {
                                "Confirm password does not match"
                            },
                            Success = false
                    });
                }

                if(existingUser != null)
                {
                    return BadRequest(new UserLoginResponse(){
                            Errors = new List<string>() {
                                "Username already in use"
                            },
                            Success = false
                    });
                }

                var newUser = new IdentityUser() { UserName = user.Username};
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if(isCreated.Succeeded)
                {
                   var jwtToken =  GenerateJwtToken( newUser);

                   return Ok(new UserLoginResponse() {
                       Success = true,
                       Token = jwtToken
                   });
                } else {
                    return BadRequest(new UserLoginResponse(){
                            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                            Success = false
                    });
                }
            }

            return BadRequest(new UserLoginResponse(){
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                    Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(user.Username);

                if(existingUser == null) {
                        return BadRequest(new UserLoginResponse(){
                            Errors = new List<string>() {
                                "Invalid login request"
                            },
                            Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(!isCorrect) {
                      return BadRequest(new UserLoginResponse(){
                            Errors = new List<string>() {
                                "Invalid login request"
                            },
                            Success = false
                    });
                }

                var jwtToken  = GenerateJwtToken(existingUser);

                return Ok(new UserLoginResponse() {
                    Success = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new UserLoginResponse(){
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                    Success = false
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id", user.Id), 
                    // new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    // new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}