using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMvc.Entities.Concrete;
using AspNetCoreMvc.Entities.Dtos;
using AspNetCoreMvc.Shared.Utilities.Results.Abstract;

namespace AspNetCoreMvc.Services.Abstract
{
    public interface IAyarlarService
    {
        Task<IDataResult<AyarlarDto>> GetAsync(int ayarId);
    
    }
}
