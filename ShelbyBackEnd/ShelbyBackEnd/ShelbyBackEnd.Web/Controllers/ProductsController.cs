using Microsoft.AspNetCore.Mvc;
using ShelbyBackEnd.Services.Contract;

namespace ShelbyBackEnd.Web.Controllers
{
    public class ProductsController : Controller
    {
        IProductService _productService;    
      

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page)
        {
           int pagesize =  20 ;
            var products = await _productService.GetAllProducts(page, pagesize);
          
            return View(products);
        }
    }
}
