using Microsoft.AspNetCore.Mvc;

namespace ShelbyBackEnd.Web.Controllers
{
    public class Warehouses : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
