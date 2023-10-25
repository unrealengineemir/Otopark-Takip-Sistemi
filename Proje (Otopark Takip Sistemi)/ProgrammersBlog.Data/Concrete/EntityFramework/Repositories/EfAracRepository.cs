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
    public class EfAracRepository: EfEntityRepositoryBase<Arac>,IAracRepository
    {
        public EfAracRepository(DbContext context) : base(context)
        {
        }

        public async Task<Arac> GetById(int aracId)
        {
            return await ProgrammersBlogContext.Aracs.SingleOrDefaultAsync(c => c.Id == aracId);
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
