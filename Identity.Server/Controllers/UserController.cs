using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.Server
{
    public class UserController
    {
        public UserController()
        {

        }

        public Task<IActionResult> Login(object credentials)
        {
            throw new NotImplementedException();
        }
    }
}