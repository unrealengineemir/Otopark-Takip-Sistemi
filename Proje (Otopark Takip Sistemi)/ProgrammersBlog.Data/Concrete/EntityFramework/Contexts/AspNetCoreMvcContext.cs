using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMvc.Data.Concrete.EntityFramework.Mappings;
using AspNetCoreMvc.Entities.Concrete;

namespace AspNetCoreMvc.Data.Concrete.EntityFramework.Contexts
{
    public class AspNetCoreMvcContext : IdentityDbContext<User,Role,int,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Arac> Aracs { get; set; }
        public DbSet<Ayarlar> Ayarlars { get; set; }

        public AspNetCoreMvcContext(DbContextOptions<AspNetCoreMvcContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AracMap());
            modelBuilder.ApplyConfiguration(new AyarlarMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());

        }
    }
}
