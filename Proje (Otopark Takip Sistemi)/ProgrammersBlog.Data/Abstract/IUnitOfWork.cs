using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
      
        ICategoryRepository Categories { get; }
        
        IAracRepository Aracs { get; }

        IAyarlarRepository Ayarlars { get; }
        Task<int> SaveAsync();
    }
}
