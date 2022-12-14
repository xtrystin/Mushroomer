using AuthService.Data;
using AuthService.Microserives;
using AuthService.Microservices.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IUserMicroservice _userMicroservice;

        public UserController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            ILogger<UserController> logger,
            IUserMicroservice userMicroservice)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _userMicroservice = userMicroservice;
        }

        //[HttpGet]
        //public UserModel GetById()
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    return _userData.GetUserById(userId).First();
        //}

        public record UserRegistrationModel(
            string FirstName,
            string LastName,
            string EmailAddress,
            string Password);

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationModel user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);
                if (existingUser is null)
                {
                    IdentityUser newUser = new()
                    {
                        Email = user.EmailAddress,
                        EmailConfirmed = true,       // email confirmation is not implemented
                        UserName = user.EmailAddress
                    };

                    IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                    if (result.Succeeded)
                    {
                        existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);

                        if (existingUser is null)
                        {
                            return BadRequest();
                        }

                        UserDto userDto = new UserDto
                        {
                            Id = new Guid(existingUser.Id),
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            EmailAddress = user.EmailAddress
                        };
                        await _userMicroservice.Post(userDto);

                        return Ok();
                    }
                }
            }

            return BadRequest();
        }

    }
}