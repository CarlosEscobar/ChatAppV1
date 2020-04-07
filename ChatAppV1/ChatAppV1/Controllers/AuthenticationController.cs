using ChatAppV1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ChatAppV1.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> loginManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthenticationController(SignInManager<IdentityUser> loginManager, UserManager<IdentityUser> userManager)
        {
            this.loginManager = loginManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public ActionResult DoLogin([FromBody] UserModel user)
        {
            try
            { 
                if (loginManager.PasswordSignInAsync(user.UserName, user.Password, true, false).Result.Succeeded)
                {
                    return Ok(user.UserName);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/logout")]
        public ActionResult DoLogout()
        {
            try
            {
                loginManager.SignOutAsync().Wait();
                return Ok("Sucess");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/register")]
        public ActionResult DoRegister([FromBody] RegisterModel model)
        {
            try
            { 
                if(!model.Password.Any(char.IsDigit))
                {
                    throw new ArgumentOutOfRangeException("Password must contain a digit.");
                }

                if(!model.Password.Any(char.IsLower))
                {
                    throw new ArgumentOutOfRangeException("Password must contain at least one lower case letter.");
                }

                if(!model.Password.Any(char.IsUpper))
                {
                    throw new ArgumentOutOfRangeException("Password must contain at least one upper case letter.");
                }

                if(model.Password.Length < 8)
                {
                    throw new ArgumentOutOfRangeException("Password must contain at least 8 characters.");
                }

                if(userManager.FindByNameAsync(model.UserName).Result != null)
                {
                    throw new ArgumentOutOfRangeException("Username already taken.");
                }

                if(!model.Email.Contains('@'))
                {
                    throw new ArgumentOutOfRangeException("Invalid email format.");
                }

                if(userManager.FindByEmailAsync(model.Email).Result != null)
                {
                    throw new ArgumentOutOfRangeException("Email already taken.");
                }

                IdentityUser newUser = new IdentityUser();
                newUser.Id = Guid.NewGuid().ToString();
                newUser.UserName = model.UserName;
                newUser.Email = model.Email;
                newUser.PhoneNumber = model.PhoneNumber;
                if (userManager.CreateAsync(newUser, model.Password).Result.Succeeded)
                {
                    userManager.AddToRoleAsync(newUser, "Chatter").Wait();
                }
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}