using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyBackEnd.Web.Models.ViewModels;
using ShelbyEComm.Services.Models;
using System.Collections;
using static ShelbyBackEnd.Application.Common.Service.CryptoLib;

namespace ShelbyBackEnd.Web.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;


        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page, int ps, int pt, string so)
        {
            ProductsVM vm = new();
            ps = ps == 0 ? 20 : ps;


            if (pt == 1 || pt == 0)
            {
                vm = await ManageProducts(page, ps, pt, so);
            }
            else if (pt == 2)
            {
                vm = await ManageLowInventoryProducts(page, ps, pt, so);
            }
            else
            {
                vm = DefaultProducts(page, ps, pt);
            }

            setViewData(ps, pt, so);

            return View(vm);
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



        private async Task<ProductsVM> ManageProducts(int page, int ps, int pt, string so)
        {
            var products = await _productService.GetAllProducts(page, ps, so);

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
    }
}
