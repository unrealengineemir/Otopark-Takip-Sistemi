using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AspNetCoreMvc.Entities.Concrete;
using AspNetCoreMvc.Entities.Dtos;

namespace AspNetCoreMvc.Services.AutoMapper.Profiles
{
    public class AracProfile:Profile
    {
        public AracProfile()
        {
            CreateMap<AracAddDto, Arac>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<AracUpdateDto, Arac>().ForMember(dest=>dest.ModifiedDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<Arac, AracUpdateDto>();
        }
    }
}
