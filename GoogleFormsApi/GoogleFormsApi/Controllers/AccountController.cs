using AutoMapper;
using BLL.Helpers;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoogleFormsApi.Controllers
{
    /// <summary>
    /// Controller to manage users
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;

        private readonly UserManager<AppUser> _userManager;

        private readonly JwtHelper _jwtHelper;

        private readonly SignInManager<AppUser> _signInManager;

        /// <summary>
        /// Constructor to initialize controller
        /// </summary>
        /// <param name="mapper">Maps requests, responses and models</param>
        /// <param name="userManager">Identity service to manage users</param>
        /// <param name="jwtHelper">Working with jwt</param>
        /// <param name="signInManager">Identity service to manage authentication</param>
        /// <param name="userService">Provides additional logic to manage users</param>
        public AccountController(IMapper mapper, UserManager<AppUser> userManager, JwtHelper jwtHelper, SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtHelper = jwtHelper;
            _signInManager = signInManager;
        }

        [HttpGet("IsLoggedInn")]
        public async Task<IActionResult> IsLoggedInn(string name)
        {
            return Ok(User != null && User.Identity != null && User.Identity.IsAuthenticated);
        }


        [HttpGet("IsLoggedIn")]
        public async Task<IActionResult> IsLoggedIn()
        {
            return Ok(User != null && User.Identity != null && User.Identity.IsAuthenticated);
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="request">Request model for registration</param>
        /// <returns>Registration confirmation or error message</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var user = _mapper.Map<AppUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join('.', result.Errors));
            }

            return Ok("Registered successfully");
        }

        /// <summary>
        /// Log in user
        /// </summary>
        /// <param name="request">Request model for login</param>
        /// <returns>Login confirmation or error message</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
            var a = User;
            if (result.Succeeded)
            {
                var token = _jwtHelper.GenerateJwtToken(user);

                return Ok(token);
            }
            return Unauthorized("Invalid email or password");
        }

        /// <summary>
        /// Log out user
        /// </summary>
        /// <returns>Log out confirmation</returns>
        [Authorize]
        [HttpGet("Log-out")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok("Logged out");
        }
    }
}
