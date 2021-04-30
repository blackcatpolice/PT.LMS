using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PageTechsLMS.DataCore.AdminUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PageTechsLMS.Admin.DbContexts
{
    public class PTAuthenticationDbContext : IdentityDbContext<PTUserEntity, PTRoleEntity, string>
    { 
        public PTAuthenticationDbContext(DbContextOptions<PTAuthenticationDbContext> options)
         : base(options)
        {
        }
    }
}
