using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyBackEnd.Services.Models;
using ShelbyBackEnd.Services.Service;
using ShelbyBackEnd.Web.Models;
using ShelbyBackEnd.Web.Models.ViewModels;
using ShelbyEComm.Services.Models;
using System.Collections;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;
using static ShelbyBackEnd.Application.Common.Service.CryptoLib;

namespace ShelbyBackEnd.Web.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;
        ICategorieService _categorieService;

        public ProductsController(IProductService productService, ICategorieService categorieService)
        {
            _productService = productService;
            _categorieService = categorieService;



        }

        [HttpGet]

        public async Task<IActionResult> Index(int page, int ps, int pt, string so)
        {
            ProductsVM vm = new();
            ps = ps == 0 ? 20 : ps;


            if (pt == 1 || pt == 0)
            {
                var products = await _productService.GetAllProducts(page, ps, so);
                vm = await SetProducts(products, page, ps, pt, so);
            }
            else if (pt == 2)
            {
                vm = await ManageLowInventoryProducts(page, ps, pt, so);
            }

            else if (pt == 4)
            {

                string jsonString = HttpContext.Session.GetString("searchSession");

                if (jsonString != null)
                {
                    var searchSession = JsonSerializer.Deserialize<SearchSession>(jsonString);
                    var products = await _productService.GetAllSearchProducts(page, ps, so, searchSession);
                    vm = await SetSearchProducts(products, page, ps, pt, so);

                }

            }
            else
            {
                vm = DefaultProducts(page, ps, pt);
            }


            setViewData(ps, pt, so);

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ProductSearch()
        {
            ProductsVM vm = new();
            await GetCategory(vm);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ProductSearch(ProductsVM? obj)
        {
            SearchSession _searchSession = (new SearchSession
            {
                product_name = obj?.Product?.product_name,
                product_code = obj?.Product?.product_code,
                product_price_min = obj?.product_price_min,
                product_price_max = obj?.product_price_max,
                product_weight_min = obj?.product_weight_min,
                product_weight_max = obj?.product_weight_max,
                tab_product_desc = obj?.Product?.tab_product_desc,
                category_id = (obj?.category_id == 0 || obj?.category_id == null) ? obj?.parent_category_id ?? 0 : obj?.category_id ?? 0,
                inactive= obj.isInactive,
                restricted=obj.isRestricted
            });
            string jsonString = JsonSerializer.Serialize(_searchSession);
            HttpContext.Session.SetString("searchSession", jsonString);
            var products = await _productService.GetAllSearchProducts(1, 20, null, _searchSession);
            ProductsVM vm = await SetSearchProducts(products, 1, 20, 4, null);
            setViewData(20, 4, null);
            return View("Index", vm);
        }


        public async Task<IActionResult> ManageProduct()
        {
            ProductsVM vm = new();
            await GetCategory(vm);
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.Archive_Product(id, 0);
            return RedirectToAction(nameof(Index));
        }


        private void setViewData(int ps, int pt, string so)
        {
            ViewData["PCSort"] = string.IsNullOrEmpty(so) ? "pc_desc" : "";
            ViewData["PNSort"] = so == "pn_asc" ? "pn_desc" : "pn_asc";
            ViewData["PPSort"] = so == "pp_asc" ? "pp_desc" : "pp_asc";
            ViewData["SSSort"] = so == "ss_asc" ? "ss_desc" : "ss_asc";
            ViewData["HSort"] = so == "h_asc" ? "h_desc" : "h_asc";
            ViewData["LQASort"] = so == "lqa_asc" ? "lqa_desc" : "lqa_asc";
            ViewData["RQSort"] = so == "rq_asc" ? "rq_desc" : "rq_asc";
            ViewData["ATPSort"] = so == "atp_asc" ? "atp_desc" : "atp_asc";
            ViewData["LPQSort"] = so == "lpq_asc" ? "lpq_desc" : "lpq_asc";
            ViewData["LPDSort"] = so == "lpd_asc" ? "lpd_desc" : "lpd_asc";
            ViewData["pagesize"] = ps;
            ViewData["pageType"] = pt;
            ViewData["CurrentSort"] = so;
        }

        private async Task<ProductsVM> SetProducts(PaginatedList<Select_All_Products_ListResult> products, int page, int ps, int pt, string so)
        {
            ProductsVM productListViewModel = new()
            {
                Products = products,
                pagesize = ps,
                pageType = pt,
                isProducts = true,
                totalPages = products.TotalPages,
                currentPage = products.PageIndex,
                Title = "All Products"
            };
            return productListViewModel;
        }

        private async Task<ProductsVM> SetSearchProducts(PaginatedList<Select_All_search_ProductsResult> products, int page, int ps, int pt, string so)
        {
            ProductsVM productListViewModel = new()
            {
                SearchProducts = products,
                pagesize = ps,
                pageType = pt,
                isProductSearch = true,
                totalPages = products.TotalPages,
                currentPage = products.PageIndex,
                Title = "Searched Products"
            };
            return productListViewModel;
        }

        private async Task<ProductsVM> ManageLowInventoryProducts(int page, int ps, int pt, string so)
        {

            var liProducts = await _productService.GetAllLowInventoryProducts(page, ps, so);
            ProductsVM productListViewModel = new()
            {
                lowInventoryProducts = liProducts,
                pagesize = ps,
                pageType = pt,
                isLIProducts = true,
                totalPages = liProducts.TotalPages,
                currentPage = liProducts.PageIndex,
                Title = "Products : Low Inventory"
            };
            return productListViewModel;
        }



        private ProductsVM DefaultProducts(int page, int ps, int pt)
        {
            ProductsVM vm = new()
            {
                Products = new PaginatedList<Select_All_Products_ListResult>(new List<Select_All_Products_ListResult>(), 0, 1, ps),
                pagesize = ps,
                pageType = pt

            };
            return vm;
        }


        private async Task<ProductsVM> GetCategory(ProductsVM vm)
        {
            var categories = await _categorieService.GetAllCategories();

            vm.categoryList = categories.Where(l => l.parent_category_id == 0).Select(u => new SelectListItem
            {
                Text = u.category_name,
                Value = u.category_id.ToString()
            });

            return vm;
        }



    }


}
