using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc.Entities.Dtos;

namespace AspNetCoreMvc.Mvc.Areas.Admin.Models
{
    public class AracUpdateAjaxViewModel
    {
        public AracUpdateDto AracUpdateDto { get; set; }
        public string AracUpdatePartial { get; set; }
        public AracDto AracDto { get; set; }
    }
}
