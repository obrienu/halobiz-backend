using Google.Apis.Auth;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using HaloBiz.DTOs.TransferDTOs;
using HaloBiz.MyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HaloBiz.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {      
        private readonly AppSettings appSettings;
        private readonly IUserProfileService userProfileService;

        public AuthController(AppSettings appSettings,
            IUserProfileService userProfileService)
        {
            this.appSettings = appSettings;
            this.userProfileService = userProfileService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginReceivingDTO loginReceiving)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(loginReceiving.IdToken);
            }
            catch (InvalidJwtException invalidJwtException)
            {
                return StatusCode(404, invalidJwtException.Message);
            }

            if (!payload.EmailVerified)
            {
                return StatusCode(404, "Email verification failed.");
            }

            var email = payload.Email;

            var response = await userProfileService.FindUserByEmail(email);
            if (response.StatusCode >= 400)
                return StatusCode(response.StatusCode, response);
            var user = ((ApiOkResponse)response).Result;
            var userProfile = (UserProfileTransferDTO)user;

            var key = appSettings.JWTSecretKey;
            var buffer = Encoding.UTF8.GetBytes(key);
            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateJwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                expires: DateTime.UtcNow.AddMinutes(10),
                subject: new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, userProfile.Id.ToString()),
                    new Claim(ClaimTypes.Email, userProfile.Email)                   
                }),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(buffer), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha512Digest));

            var jwtToken = handler.WriteToken(token);
        
            return Ok(new LoginTransferDTO { Token = jwtToken, ExpiryDate = token.ValidTo });
        }
    }
}
