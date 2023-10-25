using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMvc.Entities.Concrete;
using AspNetCoreMvc.Mvc.Areas.Admin.Models;
using AspNetCoreMvc.Services.Abstract;
using AspNetCoreMvc.Shared.Utilities.Results.ComplexTypes;

namespace AspNetCoreMvc.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IAracService _aracService;
        private readonly IAyarlarService _ayarlarService;
        private readonly UserManager<User> _userManager;

        public HomeController(ICategoryService categoryService, IAracService aracService, IAyarlarService ayarlarService)
        {
            _categoryService = categoryService;
            _ayarlarService = ayarlarService;
            _aracService = aracService;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesCountResult = await _categoryService.CountByNonDeletedAsync();
            var ayarlarCountResult = await _ayarlarService.GetAsync(1);
            var araclarCountResult = await _aracService.GetAllOnlineAsync();
            var araclarResult = await _aracService.GetAllAsync();
         
            if (categoriesCountResult.ResultStatus == ResultStatus.Success && ayarlarCountResult.ResultStatus == ResultStatus.Success && araclarCountResult.ResultStatus == ResultStatus.Success)
            {
                return View(new DashboardViewModel
                {
                    CategoriesCount = categoriesCountResult.Data,
                    KapasiteCount = ayarlarCountResult.Data.Ayar.ParkSayisi,
                    OnlineCount = araclarCountResult.Data.Aracs.Count,
                    Aracs = araclarResult.Data.Aracs.Where(p => p.IsDeleted == true && p.Price!=0).ToList(),
                }); 
            }

            return NotFound();

        }
    }
}
