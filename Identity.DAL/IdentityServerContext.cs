using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity.DAL
{
    public class IdentityServerContext : IdentityDbContext
    {
        public IdentityServerContext(DbContextOptions<IdentityServerContext> options)
            : base(options)
        {
        }
    }
}
