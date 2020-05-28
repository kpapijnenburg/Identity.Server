using FluentAssertions;
using Identity.Server.Controllers;
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
            const string expectedJwt = "Jwt";

            var controller = new UserController();
            object credentials = null;
            var result = await controller.Login(credentials);

            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<string>()
                .Which.Equals(expectedJwt);
        }
    }
}
