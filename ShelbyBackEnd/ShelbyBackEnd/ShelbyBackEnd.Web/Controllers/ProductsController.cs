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
            PaginatedList<Select_All_Products_ListResult> products;
            ps = ps == 0 ? 20 : ps;
            bool isProducts = (pt == 1 || pt == 0);
            if (isProducts)
            {
                 products = await _productService.GetAllProducts(page, ps);
            
            }
            else
            {

                List<Select_All_Products_ListResult> lProducts = new List<Select_All_Products_ListResult>();
                products = new PaginatedList<Select_All_Products_ListResult>(lProducts, 0,0,0);
            }

            // return View(products);

            ProductsVM productListViewModel = new()
            {
                Products = products,
                pagesize = ps,
                pageType=pt,
                isProducts = isProducts
            };
            ViewData["pagesize"] = ps;
            ViewData["pageType"] = pt;
            return View(productListViewModel);
        }
    }
}
