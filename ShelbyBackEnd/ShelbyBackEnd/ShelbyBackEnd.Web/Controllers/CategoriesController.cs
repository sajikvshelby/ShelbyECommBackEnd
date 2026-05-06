using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyBackEnd.Web.Models.ViewModels;
using System.Linq;
using System.Security.Cryptography;

namespace ShelbyBackEnd.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategorieService _categorieService;

        public CategoriesController(ICategorieService categorieService)
        {
            _categorieService = categorieService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int CatID)
        {
            var categories = await _categorieService.GetAllCategories();
            return View(categories);
        }
        public async Task<IActionResult> CreateCategories(int pid, int cid, string ctg)
        {
            var categoriesVM = await ManageCreate(pid, cid, ctg);
            return View(categoriesVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriesVM obj)
        {
            obj.Category.parent_category_id = (obj.Title == "Sub Sub") ? obj.Category.category_id : obj.Category.parent_category_id;
            await _categorieService.Insert_Categories(obj.Category);
            return RedirectToAction(nameof(Index));

        }
     

        public async Task<IActionResult> UpdateCategories(int pid, int cid, string ctg)
        {

            var categoriesVM = await ManageUpdate(pid, cid, ctg);

            return View(categoriesVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategories(CategoriesVM obj)
        {
            obj.Category.parent_category_id = (obj.ctg == "2") ? obj.maincategoryid : (obj.ctg == "3") ? obj.subcategoryid : 0;
            await _categorieService.Update_Categories(obj.Category);
            return RedirectToAction(nameof(Index));
        }

          [HttpPost]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            await _categorieService.Archive_CategoriesAsync(id, 0);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetSubcategories(int parentCategoryId)
        {
            var subCategories = await _categorieService.GetAllCategories(parentCategoryId, 0);
            return Json(subCategories);
        }
        private async Task<CategoriesVM> ManageUpdate(int pid, int cid, string ctg)
        {
            var categories = await _categorieService.GetAllCategories();
            CategoriesVM categoriesVM = new();
            categoriesVM.ctg = ctg;
            categoriesVM.Category = categories.SingleOrDefault(c => c.category_id == cid);
            categoriesVM.categoryList = categories.Where(l => l.parent_category_id == 0).Select(u => new SelectListItem
            {
                Text = u.category_name,
                Value = u.category_id.ToString()
            });

            if (ctg == "2")
            {
                categoriesVM.Title = "Sub";

                categoriesVM.subCategoryList = categories.Where(l => l.parent_category_id == pid).Select(u => new SelectListItem
                {
                    Text = u.category_name,
                    Value = u.category_id.ToString()
                });
                categoriesVM.maincategoryid = pid;
            }
            if (ctg == "3")
            {
                categoriesVM.Title = "Sub Sub";
                categoriesVM.subCategoryList = categories.Where(l => l.category_id == pid).Select(u => new SelectListItem
                {
                    Text = u.category_name,
                    Value = u.category_id.ToString()
                });
                categoriesVM.maincategoryid = categories.SingleOrDefault(c => c.category_id == pid)?.parent_category_id ?? 0;
                categoriesVM.subcategoryid = pid;

            }
            categoriesVM.sortByList = (await _categorieService.GetSortByList()).Select(u => new SelectListItem
            {
                Text = u.sort_by_desc,
                Value = u.sort_by_id.ToString()
            });

            return categoriesVM;
        }
    
        private async Task<CategoriesVM> ManageCreate(int pid, int cid, string ctg)
        {

            var categories = await _categorieService.GetAllCategories();
            CategoriesVM categoriesVM = new()
            {
                Category = new Select_All_CategoriesResult { parent_category_id = pid, category_id = cid },


                Title = ctg == "1" ? "Sub" : ctg == "2" ? "Sub Sub" : "",

                categoryList = categories.Where(l => l.parent_category_id == 0).Select(u => new SelectListItem
                {
                    Text = u.category_name,
                    Value = u.category_id.ToString()
                }),
                subCategoryList = categories.Where(l => l.parent_category_id == pid).Select(u => new SelectListItem
                {
                    Text = u.category_name,
                    Value = u.category_id.ToString()
                }),
                sortByList = (await _categorieService.GetSortByList()).Select(u => new SelectListItem
                {
                    Text = u.sort_by_desc,
                    Value = u.sort_by_id.ToString()
                })
            };

            return categoriesVM;
        }
    }
}