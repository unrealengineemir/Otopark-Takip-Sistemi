using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMvc.Data.Abstract;
using AspNetCoreMvc.Data.Concrete.EntityFramework.Contexts;
using AspNetCoreMvc.Data.Concrete.EntityFramework.Repositories;

namespace AspNetCoreMvc.Data.Concrete
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AspNetCoreMvcContext _context;
        private EfCategoryRepository _categoryRepository;
        private EfAracRepository _aracRepository;
        private EfAyarlarRepository _ayarlarRepository;

        public UnitOfWork(AspNetCoreMvcContext context)
        {
            _context = context;
        }

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);
        public IAracRepository Aracs => _aracRepository ?? new EfAracRepository(_context);
        public IAyarlarRepository Ayarlars => _ayarlarRepository ?? new EfAyarlarRepository(_context);
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
