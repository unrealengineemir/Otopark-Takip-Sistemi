using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreMvc.Entities.Concrete;

namespace AspNetCoreMvc.Entities.Dtos
{
    public class AracAddDto
    {
        [DisplayName("Plaka")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100,ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(6, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string LicensePlate { get; set; }

   
        [DisplayName("Giriş Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode = true,DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EnterDate { get; set; }

        [DisplayName("Çıkış Tarih")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExitDate { get; set; }

        [DisplayName("Abone Mi?")]
        public bool Subscriber { get; set; }

        [DisplayName("Abone Numarası")]
        [MaxLength(15, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string SubscriberRFID { get; set; }

        [DisplayName("Not")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string Note { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public bool IsActive { get; set; }
    }
}
