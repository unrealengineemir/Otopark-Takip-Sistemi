using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMvc.Data.Abstract;
using AspNetCoreMvc.Data.Concrete.EntityFramework.Contexts;
using AspNetCoreMvc.Entities.Concrete;
using AspNetCoreMvc.Shared.Data.Concrete.EntityFramework;

namespace AspNetCoreMvc.Data.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository:EfEntityRepositoryBase<Category>,ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await ProgrammersBlogContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);
        }

        private AspNetCoreMvcContext ProgrammersBlogContext
        {
            get
            {
                return _context as AspNetCoreMvcContext;
            }
        }
    }
}
