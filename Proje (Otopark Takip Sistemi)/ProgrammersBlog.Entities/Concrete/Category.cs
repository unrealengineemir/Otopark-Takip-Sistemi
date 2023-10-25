using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMvc.Shared.Entities.Abstract;

namespace AspNetCoreMvc.Entities.Concrete
{
    public class Category:EntityBase,IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Range1 { get; set; }
        public decimal Range2 { get; set; }
    }
}
