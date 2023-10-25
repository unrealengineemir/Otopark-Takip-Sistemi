using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AspNetCoreMvc.Entities.Concrete;

namespace AspNetCoreMvc.Data.Concrete.EntityFramework.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Price).IsRequired();
            builder.Property(c => c.Range1).IsRequired();
            builder.Property(c => c.Range2).IsRequired();
            builder.Property(c => c.Price).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired();
            builder.Property(c => c.ModifiedDate).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.ToTable("Categories");

            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "0-1 Saat",
                    Description = "0-1 Saat",
                    Price = 10,
                    Range1= 0,
                    Range2 = 1,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                },
                new Category
                {
                    Id = 2,
                    Name = "1-3 Saat",
                    Description = "1-3 Saat",
                    Price = 20,
                    Range1 = 1,
                    Range2 = 3,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                },

                new Category
                {
                    Id = 3,
                    Name = "3-6 Saat",
                    Description = "3-6 Saat",
                    Price = 30,
                    Range1 = 3,
                    Range2 = 6,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                },
                  new Category
                  {
                      Id = 4,
                      Name = "6-12 Saat",
                      Description = "6-12 Saat",
                      Price = 40,
                      Range1 = 6,
                      Range2 = 12,
                      IsActive = true,
                      IsDeleted = false,
                      CreatedByName = "InitialCreate",
                      CreatedDate = DateTime.Now,
                      ModifiedByName = "InitialCreate",
                      ModifiedDate = DateTime.Now,
                  },
                   new Category
                   {
                       Id = 5,
                       Name = "Günlük",
                       Description = "Günlük",
                       Price = 50,
                       Range1 = 24,
                       Range2 = 168,
                       IsActive = true,
                       IsDeleted = false,
                       CreatedByName = "InitialCreate",
                       CreatedDate = DateTime.Now,
                       ModifiedByName = "InitialCreate",
                       ModifiedDate = DateTime.Now,
                   }
            );
        }
    }
}
