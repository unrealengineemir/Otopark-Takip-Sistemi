using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMvc.Entities.Concrete;
using AspNetCoreMvc.Shared.Entities.Abstract;
using AspNetCoreMvc.Shared.Utilities.Results.ComplexTypes;

namespace AspNetCoreMvc.Entities.Dtos
{
    public class AyarlarDto : DtoGetBase
    {
        public Ayarlar Ayar { get; set; }
    }
}
