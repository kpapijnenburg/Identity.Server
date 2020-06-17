using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain
{
    public class LoginOutputModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
