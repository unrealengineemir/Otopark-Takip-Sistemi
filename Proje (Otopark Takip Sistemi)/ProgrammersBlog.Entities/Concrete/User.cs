using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AspNetCoreMvc.Shared.Entities.Abstract;

namespace AspNetCoreMvc.Entities.Concrete
{
    public class User:IdentityUser<int>
    {
        public string Picture { get; set; }
    }
}
