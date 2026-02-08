using Fas7ny.Application.DTOs.Account.Request;
using Fas7ny.Application.DTOs.Account.Response;
using Fas7ny.Application.ServivesInterfaces;
using Fas7ny.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fas7nyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        public AccountController(IJwtTokenService jwtTokenService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

            _jwtTokenService = jwtTokenService;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationRequestDTO registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new UserResponseDto
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            var user = new ApplicationUser
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                UserName = registerRequest.userName
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new UserResponseDto
                {
                    Success = false,
                    Message = "Registration failed",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            return Ok(new UserResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,

                Success = true,
                Message = "Registration successful",

            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestDTO userLoginRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new UserResponseDto
                {
                    Success = false,
                    Message = "Validation failed",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }


            var user = await _userManager.FindByEmailAsync(userLoginRequestDTO.Email);
            if (user == null)
            {
                return Unauthorized(new UserResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"
                });
            }

            var isPasswordValid =
                await _userManager.CheckPasswordAsync(user, userLoginRequestDTO.Password);

            if (!isPasswordValid)
            {
                return Unauthorized(new UserResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password"

                });
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return Ok(new UserResponseDto
            {
                UserId = user.Id,
                Email = user.Email!,
                FullName = user.FullName,
                Token = token,
                Success = true,
                Message = "Login successful",
                Errors = new List<string>()
            });
        }


        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            // await _signInManager.SignOutAsync();
            return Ok(new UserResponseDto
            {
                Success = true,
                Message = "Logout successfully"
            });
        }


        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword
            );

            if (!result.Succeeded)
            {
                return BadRequest(new UserResponseDto
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            await _userManager.UpdateSecurityStampAsync(user);

            return Ok(new UserResponseDto
            {
                Success = true,
                Message = "Password changed successfully"
            });
        }


        [HttpPost("Forget-Password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequestDto forgetPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgetPassword.Email);

            if (user == null)
                return NotFound(new UserResponseDto
                {
                    Success = false,
                    Message = "If the email exists, a reset link has been sent"
                });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(new UserResponseDto
            {
                Success = true,
                Message = "Password reset link sent if email exists"
            });


        }
        //check claims
        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            return Ok(new
            {
                User.Identity?.IsAuthenticated,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                Name = User.FindFirst(ClaimTypes.Name)?.Value,
                Roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(),
                AllClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
            });
        }
        [HttpPost("assign-admin/{email}")]
        public async Task<IActionResult> AssignAdminRole(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { message = "User not found" });

            var isInRole = await _userManager.IsInRoleAsync(user, "Admin");
            if (isInRole)
                return Ok(new { message = "User already has Admin role" });

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
                return Ok(new { message = "Admin role assigned successfully. Please login again to get a new token." });

            return BadRequest(new { message = "Failed to assign role", errors = result.Errors });
        }






    }
}
