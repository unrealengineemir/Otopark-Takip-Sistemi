using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AspNetCoreMvc.Data.Abstract;
using AspNetCoreMvc.Entities.Concrete;
using AspNetCoreMvc.Entities.Dtos;
using AspNetCoreMvc.Services.Abstract;
using AspNetCoreMvc.Services.Utilities;
using AspNetCoreMvc.Shared.Utilities.Results.Abstract;
using AspNetCoreMvc.Shared.Utilities.Results.ComplexTypes;
using AspNetCoreMvc.Shared.Utilities.Results.Concrete;

namespace AspNetCoreMvc.Services.Concrete
{
    public class AyarlarManager : IAyarlarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AyarlarManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<AyarlarDto>> GetAsync(int ayarId)
        {
            var ayarlars = await _unitOfWork.Ayarlars.GetAsync(a => a.Id == ayarId);
            if (ayarlars != null)
            {
                return new DataResult<AyarlarDto>(ResultStatus.Success,new AyarlarDto
                {
                    Ayar = ayarlars,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AyarlarDto>(ResultStatus.Error,Messages.Ayarlar.NotFound(isPlural:false),null);
        }

        
    }
}
