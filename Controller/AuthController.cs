using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Titan_Biometric.EFCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Titan_Biometric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        
        private readonly EF_DataContext _context;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, EF_DataContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            var user = new IdentityUser { UserName = $"{request.FirstName}", Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var res = await AddNewUser(request);
            if(res.Succeeded)
                return Ok("User registered successfully.");
            
            return Ok("Error occured. Try again.");
        }

        private async Task<IdentityResult> AddNewUser(SignupRequest request)
        {
            try
            {
                UserInfo user = new UserInfo();
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.Password = getHash(request.Password);
                user.PhoneNumber = string.Empty;
                user.Height = 0;
                user.HeightUnit = string.Empty;
                user.Weight = 0;
                user.WeightUnit = string.Empty;
                user.BMI = 0;
                user.Gender = string.Empty;
                user.IsEmailVarified = false;
                user.IsPhoneVarified = false;
                user.SkinTone = 0;

                _context.Add(user);
                await _context.SaveChangesAsync();

                return IdentityResult.Success;
            }
            catch
            {
                return IdentityResult.Failed();
            }
        }

        private string getHash(string password)
        {
            string salt = getSalt();
            password += salt; 
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private string getSalt()
        {
            byte[] bytes = new byte[128 / 8];
            using (var keyGenerator = RandomNumberGenerator.Create())
            {
                keyGenerator.GetBytes(bytes);
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized("Invalid credentials.");

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token,Expires = DateTime.Now.AddMinutes(30) });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class SignupRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
