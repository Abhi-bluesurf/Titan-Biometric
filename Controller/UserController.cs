using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Titan_Biometric.EFCore;

namespace Titan_Biometric.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EF_DataContext _context;
        public UserController(EF_DataContext context)
        {
            _context = context;
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfo user)
        {
            if (user == null)
            {
                return BadRequest("Invalid information! Please fill complete info.");
            }
            else
            {
                var userInfo = _context.UsersInfo.FirstOrDefault(u => u.Email.Equals(user.Email));
                userInfo.FirstName = user.FirstName;
                userInfo.LastName = user.LastName;
                userInfo.PhoneNumber = user.PhoneNumber;
                userInfo.Height = user.Height;
                userInfo.HeightUnit = user.HeightUnit;
                userInfo.Weight = user.Weight;
                userInfo.WeightUnit = user.WeightUnit;
                userInfo.Gender = user.Gender;
                userInfo.BMI = user.BMI;
                userInfo.DOB = user.DOB;
                userInfo.SkinTone = user.SkinTone;
                _context.SaveChanges();
                return Ok("Profile Updated!");
            }
        }

    }
}
