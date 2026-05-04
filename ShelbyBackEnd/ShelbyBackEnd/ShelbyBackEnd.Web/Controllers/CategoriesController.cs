using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
        public async Task<IActionResult> CreateCategories()
        {

            var categoriesVM = await GetCategoriesVM(0, 0);
            categoriesVM.Title = "";
            return View(categoriesVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoriesVM obj)
        {
            obj.Category.parent_category_id = (obj.Title == "Sub Sub") ? obj.Category.category_id : obj.Category.parent_category_id;

            await _categorieService.Insert_Categories(obj.Category);
            TempData["success"] = "created successFully";
            return RedirectToAction(nameof(Index));

        }


        [HttpGet("Categories/CreateCategories/{cid}")]
        public async Task<IActionResult> CreateCategories(int cid)
        {
           

            var categoriesVM = await GetCategoriesVM(cid, 0);
            categoriesVM.Title = "Sub";
            return View(categoriesVM);
        }


        [HttpGet("Categories/CreateCategories/{pid}/{cid}")]
        public async Task<IActionResult> CreateCategories(int pid, int cid)
        {
            var categoriesVM = await GetCategoriesVM(pid, cid);
            categoriesVM.Title = "Sub Sub";

            return View(categoriesVM);
        }
        public async Task<IActionResult> UpdateCategories(int cid)
        {
            var categories = await _categorieService.GetAllCategories(0, cid);

            var category = categories.SingleOrDefault(l => l.category_id == cid);
            if (categories == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(category);

        }

        [HttpPost]
        public IActionResult UpdateCategories(Select_All_CategoriesResult obj)
        {


            if (ModelState.IsValid && obj.category_id > 0)
            {
                TempData["success"] = "update successFully";
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }


        public async Task<IActionResult> DeleteCategories(int categoryid)
        {

            var categories = await _categorieService.GetAllCategories();
            var category = categories.SingleOrDefault(l => l.category_id == categoryid);

            if (categories == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(category);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategories(Select_All_CategoriesResult obj)
        {
            var categories = await _categorieService.GetAllCategories();
            if (categories is not null)
            {
                TempData["success"] = "Deleted successFully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "category could not be deleted";
            return View(categories);
        }
        public async Task<IActionResult> GetSubcategories(int parentCategoryId)
        {
            var categories = await _categorieService.GetCategories();
            var subCategories = categories.Where(c => c.parent_category_id == parentCategoryId)
                .Select(s => new { s.category_id, s.category_name })
                .ToList();

            return Json(subCategories);
        }


        private async Task<CategoriesVM> GetCategoriesVM(int pid, int cid)
        {

            var categories = await _categorieService.GetCategories();
            CategoriesVM categoriesVM = new()
            {

              
                Category = new Select_All_CategoriesResult { parent_category_id = pid, category_id = cid },

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