using ChatAppV1.Controllers;
using ChatAppV1.Models;
using ChatAppV1.UnitTests.MockIdentityManagers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace ChatAppV1.UnitTests
{
    public class RegisterUnitTest
    {
        private AuthenticationController authenticationController = new AuthenticationController(new MockSignInManager(), new MockUserManager());

        [Fact]
        public void TestNoDigitPassword()
        {
            var registerResult = (BadRequestObjectResult)authenticationController.DoRegister(new RegisterModel
            {
                UserName = "test",
                Password = "Password",
                Email = "test@test.com",
                PhoneNumber = "123456789"
            });
            Assert.IsType<ArgumentOutOfRangeException>(registerResult.Value);
            Assert.Equal("Password must contain a digit.", ((ArgumentOutOfRangeException)registerResult.Value).ParamName);
        }

        [Fact]
        public void TestNoLowerCasePassword()
        {
            var registerResult = (BadRequestObjectResult)authenticationController.DoRegister(new RegisterModel
            {
                UserName = "test",
                Password = "PASSW0RD",
                Email = "test@test.com",
                PhoneNumber = "123456789"
            });
            Assert.IsType<ArgumentOutOfRangeException>(registerResult.Value);
            Assert.Equal("Password must contain at least one lower case letter.", ((ArgumentOutOfRangeException)registerResult.Value).ParamName);
        }

        [Fact]
        public void TestNoUpperCasePassword()
        {
            var registerResult = (BadRequestObjectResult)authenticationController.DoRegister(new RegisterModel
            {
                UserName = "test",
                Password = "passw0rd",
                Email = "test@test.com",
                PhoneNumber = "123456789"
            });
            Assert.IsType<ArgumentOutOfRangeException>(registerResult.Value);
            Assert.Equal("Password must contain at least one upper case letter.", ((ArgumentOutOfRangeException)registerResult.Value).ParamName);
        }

        [Fact]
        public void TestInvalidLengthPassword()
        {
            var registerResult = (BadRequestObjectResult)authenticationController.DoRegister(new RegisterModel
            {
                UserName = "test",
                Password = "Passw0r",
                Email = "test@test.com",
                PhoneNumber = "123456789"
            });
            Assert.IsType<ArgumentOutOfRangeException>(registerResult.Value);
            Assert.Equal("Password must contain at least 8 characters.", ((ArgumentOutOfRangeException)registerResult.Value).ParamName);
        }
    }
}
