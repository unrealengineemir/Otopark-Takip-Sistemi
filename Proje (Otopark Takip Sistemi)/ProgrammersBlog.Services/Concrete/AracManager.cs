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
    public class AracManager:IAracService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AracManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<AracDto>> GetAsync(int aracId)
        {
            var aracs = await _unitOfWork.Aracs.GetAsync(a => a.Id == aracId);
            if (aracs != null)
            {
                return new DataResult<AracDto>(ResultStatus.Success,new AracDto
                {
                    Arac = aracs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AracDto>(ResultStatus.Error,Messages.Arac.NotFound(isPlural:false),null);
        }

        public async Task<IDataResult<AracListDto>> GetAllAsync()
        {
            var aracs = await _unitOfWork.Aracs.GetAllAsync(null);
            if (aracs.Count>-1)
            {
                return new DataResult<AracListDto>(ResultStatus.Success,new AracListDto
                {
                    Aracs = aracs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AracListDto>(ResultStatus.Error, Messages.Arac.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<AracListDto>> GetAllByNonDeletedAsync()
        {
            var aracs = await _unitOfWork.Aracs.GetAllAsync(a => !a.IsDeleted);
            if (aracs.Count > -1)
            {
                return new DataResult<AracListDto>(ResultStatus.Success, new AracListDto
                {
                    Aracs = aracs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AracListDto>(ResultStatus.Error, Messages.Arac.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<AracListDto>> GetAllOnlineAsync()
        {
            var aracs = await _unitOfWork.Aracs.GetAllAsync(a => a.TotalHours == 0 && !a.IsDeleted);
            if (aracs.Count > -1)
            {
                return new DataResult<AracListDto>(ResultStatus.Success, new AracListDto
                {
                    Aracs = aracs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AracListDto>(ResultStatus.Error, Messages.Arac.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<AracListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var aracs =
                await _unitOfWork.Aracs.GetAllAsync(a => !a.IsDeleted && a.IsActive);
            if (aracs.Count > -1)
            {
                return new DataResult<AracListDto>(ResultStatus.Success, new AracListDto
                {
                    Aracs = aracs,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<AracListDto>(ResultStatus.Error, Messages.Arac.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<AracDto>> AddAsync(AracAddDto aracAddDto, string createdByName)
        {
            var arac = _mapper.Map<Arac>(aracAddDto);
            arac.CreatedByName = createdByName;
            arac.ModifiedByName = createdByName;
            var addedArac = await _unitOfWork.Aracs.AddAsync(arac);
            await _unitOfWork.SaveAsync();
            return new DataResult<AracDto>(ResultStatus.Success, Messages.Arac.Add(addedArac.LicensePlate), new AracDto
            {
                Arac = addedArac,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Category.Add(addedArac.LicensePlate)
            });
        }

        public async Task<IDataResult<AracUpdateDto>> GetAracUpdateDtoAsync(int aracId)
        {
            var result = await _unitOfWork.Aracs.AnyAsync(c => c.Id == aracId);
            if (result)
            {
                var arac = await _unitOfWork.Aracs.GetAsync(c => c.Id == aracId);
                var aracUpdateDto = _mapper.Map<AracUpdateDto>(arac);
                return new DataResult<AracUpdateDto>(ResultStatus.Success, aracUpdateDto);
            }
            else
            {
                return new DataResult<AracUpdateDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
            }
        }

        public async Task<IDataResult<AracDto>> UpdateAsync(AracUpdateDto aracUpdateDto, string modifiedByName)
        {
            var oldArac = await _unitOfWork.Aracs.GetAsync(c => c.Id == aracUpdateDto.Id);
            var arac = _mapper.Map<AracUpdateDto, Arac>(aracUpdateDto, oldArac);
            arac.ModifiedByName = modifiedByName;
            var updatedArac = await _unitOfWork.Aracs.UpdateAsync(arac);
            await _unitOfWork.SaveAsync();
            return new DataResult<AracDto>(ResultStatus.Success, Messages.Arac.Update(updatedArac.LicensePlate), new AracDto
            {
                Arac = updatedArac,
                ResultStatus = ResultStatus.Success,
                Message = Messages.Arac.Update(updatedArac.LicensePlate)
            });
        }

        public async Task<IDataResult<AracDto>> DeleteAsync(int aracId, string modifiedByName)
        {
            var arac = await _unitOfWork.Aracs.GetAsync(c => c.Id == aracId);
            if (arac != null)
            {
                arac.IsDeleted = true;
                arac.ModifiedByName = modifiedByName;
                arac.ModifiedDate = DateTime.Now;
                var deletedArac = await _unitOfWork.Aracs.UpdateAsync(arac);
                await _unitOfWork.SaveAsync();
                return new DataResult<AracDto>(ResultStatus.Success, Messages.Category.Delete(deletedArac.LicensePlate), new AracDto
                {
                    Arac = deletedArac,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.Arac.Delete(deletedArac.LicensePlate)
                });
            }
            return new DataResult<AracDto>(ResultStatus.Error, Messages.Arac.NotFound(isPlural: false), new AracDto
            {
                Arac = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Arac.NotFound(isPlural: false)
            });
        }

        public async Task<IResult> HardDeleteAsync(int aracId)
        {
            var result = await _unitOfWork.Aracs.AnyAsync(a => a.Id == aracId);
            if (result)
            {
                var arac = await _unitOfWork.Aracs.GetAsync(a => a.Id == aracId);
                await _unitOfWork.Aracs.DeleteAsync(arac);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Arac.HardDelete(arac.LicensePlate));
            }
            return new Result(ResultStatus.Error, Messages.Arac.NotFound(isPlural:false));
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var aracsCount = await _unitOfWork.Aracs.CountAsync();
            if (aracsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, aracsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var aracsCount = await _unitOfWork.Aracs.CountAsync(a=>!a.IsDeleted);
            if (aracsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, aracsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }
    }
}
