using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Helpers;
using Task_Manager.Models;
using Task_Manager.UnitOfWork;
using Task_Manager.ViewModels;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISha256HashService _sha256HashService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(
            IUnitOfWork unitOfWork,
            ISha256HashService sha256HashService,
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _unitOfWork = unitOfWork;
            _sha256HashService = sha256HashService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, data = ValidationsHelper.GetErrorMsgs(ModelState.Values) });

                string hashedPassword = _sha256HashService.Hash(model.Password);
                var user = _unitOfWork.UserRepository.GetOne(u => u.Email == model.Email && u.Password == hashedPassword);

                if(user == null)
                    return BadRequest(new { success = false, data = new List<string> { "Invalid Email or Password" } });

                // Creating the token
                var claims = CreateClaims(user.Id);
                var jwtToken = GenerateToken(claims);
                var tokenJson = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                return Ok(new { success = true, data = new { access_token = tokenJson, userId = user.Id, name = user.FullName } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { success = false, data = ValidationsHelper.GetErrorMsgs(ModelState.Values) });

                string hashedPassword = _sha256HashService.Hash(model.Password);
                var user = _unitOfWork.UserRepository.GetOne(u => u.Email == model.Email);

                if (user != null)
                    return BadRequest(new { success = false, data = new List<string> { "Email is Already Taken" } });

                model.Password = _sha256HashService.Hash(model.Password);
                user = _mapper.Map<User>(model);
                _unitOfWork.UserRepository.Add(user);
                var saveResult =_unitOfWork.Save();

                if (!saveResult)
                    return BadRequest(new { success = false, data = new List<string> { "Error Occured While Saving" } });

                return Ok(new { success = true, data = new List<string> { "User Created Successfully" } });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, data = new List<string> { ex.Message } });
            }
        }

        #region Methods
        private List<Claim> CreateClaims(string userId)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId)
            };
        }

        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            string secret = _configuration.GetSection("Constants").GetSection("Secret").Value;
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes(secret));
            var signingCredientials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                _configuration.GetSection("Constants").GetSection("Issuer").Value,
                _configuration.GetSection("Constants").GetSection("Audience").Value,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredientials
                );
        }
        #endregion
    }
}
