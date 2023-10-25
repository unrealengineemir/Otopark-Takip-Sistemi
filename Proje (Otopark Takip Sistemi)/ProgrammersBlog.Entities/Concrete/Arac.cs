using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMvc.Shared.Entities.Abstract;

namespace AspNetCoreMvc.Entities.Concrete
{
    public class Arac:EntityBase,IEntity
    {
        public DateTime EnterDate { get; set; }
        public DateTime ExitDate { get; set; }
        public int TotalHours { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public string LicensePlate { get; set; }
        public bool Subscriber { get; set; }
        public string SubscriberRFID { get; set; }

    }
}
