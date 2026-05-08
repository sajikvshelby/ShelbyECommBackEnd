using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index(int page, int ps, int pt)
        {
            ProductsVM vm = new();
            ps = ps == 0 ? 20 : ps;
          
                     
            if (pt == 1 || pt == 0)
            {
                vm = await ManageProducts(page, ps, pt);
            }
            else if (pt == 2)
            {
                vm = await ManageLowInventoryProducts(page, ps, pt);
            }
            else
            {
                vm = DefaultProducts(page, ps, pt);
            }
           
            ViewData["pagesize"] = ps;
            ViewData["pageType"] = pt;
            return View(vm);
        }


      


        private async Task<ProductsVM> ManageProducts(int page, int ps, int pt)
        {
        var   products = await _productService.GetAllProducts(page, ps);
               
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


        private async Task<ProductsVM> ManageLowInventoryProducts(int page, int ps, int pt)
        {
       
          var liProducts = await _productService.GetAllLowInventoryProducts(page, ps);
            ProductsVM productListViewModel = new()
            {
                lowInventoryProducts = liProducts,
                pagesize = ps,
                pageType = pt,
                isLIProducts = true,
                totalPages = liProducts.TotalPages,
                currentPage = liProducts.PageIndex,
                Title="Products : Low Inventory"
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
