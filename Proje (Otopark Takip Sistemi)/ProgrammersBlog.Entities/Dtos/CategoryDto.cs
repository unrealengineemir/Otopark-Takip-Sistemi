using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMvc.Entities.Concrete;
using AspNetCoreMvc.Shared.Entities.Abstract;

namespace AspNetCoreMvc.Entities.Dtos
{
    public class CategoryDto:DtoGetBase
    {
        public Category Category { get; set; }
    }
}
