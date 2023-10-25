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
    public interface IAracService
    {
        Task<IDataResult<AracDto>> GetAsync(int aracId);
        Task<IDataResult<AracListDto>> GetAllAsync();
        Task<IDataResult<AracListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<AracListDto>> GetAllOnlineAsync();
        Task<IDataResult<AracListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<AracUpdateDto>> GetAracUpdateDtoAsync(int aracId);
        Task<IDataResult<AracDto>> AddAsync(AracAddDto aracAddDto, string createdByName);
        Task<IDataResult<AracDto>> UpdateAsync(AracUpdateDto aracUpdateDto, string modifiedByName);
        Task<IDataResult<AracDto>> DeleteAsync(int aracId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int aracId);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
