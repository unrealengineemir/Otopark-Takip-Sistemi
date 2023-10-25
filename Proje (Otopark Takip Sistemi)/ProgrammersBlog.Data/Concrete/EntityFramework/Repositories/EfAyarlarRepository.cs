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
    public class EfAyarlarRepository : EfEntityRepositoryBase<Ayarlar>,IAyarlarRepository
    {
        public EfAyarlarRepository(DbContext context) : base(context)
        {
        }

        public async Task<Ayarlar> GetById(int ayarId)
        {
            return await ProgrammersBlogContext.Ayarlars.SingleOrDefaultAsync(c => c.Id == ayarId);
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
