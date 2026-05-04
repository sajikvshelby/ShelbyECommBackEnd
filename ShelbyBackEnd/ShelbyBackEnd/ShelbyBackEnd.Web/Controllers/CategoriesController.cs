using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyBackEnd.Web.Models.ViewModels;

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


        public async Task<IActionResult> CreateCategories()
        {

            var categories = await _categorieService.GetCategories();
            CategoriesVM categoriesVM = new()
            {
               


               categoryList = categories.Where(l=>l.parent_category_id==0).Select(u => new SelectListItem
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

            return View(categoriesVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategories(CategoriesVM obj)
        {

            var errors = ModelState.Values
    .SelectMany(v => v.Errors)
    .Select(e => e.ErrorMessage)
    .ToList();

          
             await  _categorieService.Insert_Categories(obj.Category);
                TempData["success"] = "created successFully";
                return RedirectToAction(nameof(Index));
         

           

        }


        public async Task<IActionResult> UpdateCategories(int categoryid)
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
        public IActionResult UpdateCategories(Select_CategoriesResult obj)
        {


            if (ModelState.IsValid && obj.category_id > 0)
            {

                //_categorieService.UpdateCategories(obj);

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
        public async Task<IActionResult> DeleteCategories(Select_CategoriesResult obj)
        {

            var categories = await _categorieService.GetAllCategories();

            if (categories is not null)
            {


                //  _categorieService.DeleteCategoriesAsync(categories);

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


    }
}