﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AspNetCoreMvc.Entities.Concrete;

namespace AspNetCoreMvc.Data.Concrete.EntityFramework.Mappings
{
    public class UserRoleMap:IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(
                new UserRole
            {
                RoleId = 1,
                UserId = 1
            },
                new UserRole
                {
                    RoleId = 2,
                    UserId = 2
                }
            );
        }
    }
}
