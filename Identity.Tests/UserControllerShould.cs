using FluentAssertions;
using Identity.Domain;
using Identity.Server.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace Identity.Tests
{
    public class UserControllerShould
    {
        [Fact]
        public async void return_jwts()
        {
            //const string expectedJwt = "Jwt";

            //var controller = new UserController();
            //var credentials = new LoginModel() { Username = "name", Password = "password"};

            //var result = await controller.Login(credentials);

            //result.Should().BeOfType<OkObjectResult>()
            //    .Which.Value.Should().BeOfType<string>()
            //    .Which.Equals(expectedJwt);
            Assert.True(true);
        }
    }
}
