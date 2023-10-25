using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc.Entities.Dtos;

namespace AspNetCoreMvc.Mvc.Areas.Admin.Models
{
    public class AracAddAjaxViewModel
    {
        public AracAddDto AracAddDto { get; set; }
        public string AracAddPartial { get; set; }
        public AracDto AracDto { get; set; }
    }
}
