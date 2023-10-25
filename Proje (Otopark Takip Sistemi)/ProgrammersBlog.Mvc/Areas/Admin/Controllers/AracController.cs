using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreMvc.Entities.Dtos;
using AspNetCoreMvc.Mvc.Areas.Admin.Models;
using AspNetCoreMvc.Services.Abstract;
using AspNetCoreMvc.Shared.Utilities.Extensions;
using AspNetCoreMvc.Shared.Utilities.Results.ComplexTypes;

namespace AspNetCoreMvc.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class AracController : Controller
    {

        private readonly IAracService _aracService;
        private readonly ICategoryService _categoryService;
        private readonly IAyarlarService _ayarlarService;

        public AracController(IAracService aracService, ICategoryService categoryService, IAyarlarService ayarlarService)
        {
            _aracService = aracService;
            _categoryService = categoryService;
            _ayarlarService = ayarlarService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _aracService.GetAllByNonDeletedAsync();
            return View(result.Data);

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var resultKapasite = await _ayarlarService.GetAsync(1);
            var resultOnlineArac = await _aracService.GetAllOnlineAsync();
            if (resultKapasite.Data.Ayar.ParkSayisi < resultOnlineArac.Data.Aracs.Count + 1)
            {
                return PartialView("_AracKontrolPartial");
            }
            else
            {
                return PartialView("_AracAddPartial");

            }
        }
        [HttpPost] 
        public async Task<IActionResult> Add(AracAddDto aracAddDto)
        {
            if (ModelState.IsValid)
            {
                aracAddDto.IsActive = true;
                var result = await _aracService.AddAsync(aracAddDto, "Admin");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var aracAddAjaxModel = JsonSerializer.Serialize(new AracAddAjaxViewModel
                    {
                        AracDto = result.Data,
                        AracAddPartial = await this.RenderViewToStringAsync("_AracAddPartial", aracAddDto)
                    });
                    return Json(aracAddAjaxModel);
                }
            }
            var aracAddAjaxErrorModel = JsonSerializer.Serialize(new AracAddAjaxViewModel
            {
                AracAddPartial = await this.RenderViewToStringAsync("_AracAddPartial", aracAddDto)
            });
            return Json(aracAddAjaxErrorModel);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int aracId)
        {
            var result = await _aracService.GetAracUpdateDtoAsync(aracId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_AracUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(AracUpdateDto aracUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var resultEntity = await _aracService.GetAracUpdateDtoAsync(aracUpdateDto.Id);
                double fark = (aracUpdateDto.ExitDate.Subtract(resultEntity.Data.EnterDate).TotalMinutes) / 60;

                var resultAllCategory = await _categoryService.GetAllByNonDeletedAsync();
                var resultCategory = resultAllCategory.Data.Categories.FirstOrDefault(p => (double)p.Range1 < fark && (double)p.Range2 >= fark);
                aracUpdateDto.Price = resultCategory.Price;
                aracUpdateDto.TotalHours = (decimal)fark;
                aracUpdateDto.IsDeleted = true;
                aracUpdateDto.IsActive = true;
                aracUpdateDto.EnterDate = resultEntity.Data.EnterDate;
                var result = await _aracService.UpdateAsync(aracUpdateDto, "Admin");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var aracUpdateAjaxModel = JsonSerializer.Serialize(new AracUpdateAjaxViewModel
                    {
                        AracDto = result.Data,
                        AracUpdatePartial = await this.RenderViewToStringAsync("_AracUpdatePartial", aracUpdateDto)
                    });
                    return Json(aracUpdateAjaxModel);
                }
            }
            var aracUpdateAjaxErrorModel = JsonSerializer.Serialize(new AracUpdateAjaxViewModel
            {
                AracUpdatePartial = await this.RenderViewToStringAsync("_AracUpdatePartial", aracUpdateDto)
            });
            return Json(aracUpdateAjaxErrorModel);

        }

        public async Task<JsonResult> GetAllArac()
        {
            var result = await _aracService.GetAllByNonDeletedAsync();
            var aracs = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(aracs);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int aracId)
        {
            var result = await _aracService.DeleteAsync(aracId, "Admin");
            var deletedArac = JsonSerializer.Serialize(result.Data);
            return Json(deletedArac);
        }
    }
}
