using AspNetCoreMvc.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc.Entities.Concrete
{
    public class Ayarlar : EntityBase, IEntity
    {
        public int ParkSayisi { get; set; } = 0;
    }
}
