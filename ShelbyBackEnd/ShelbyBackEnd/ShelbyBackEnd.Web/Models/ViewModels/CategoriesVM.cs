using Microsoft.AspNetCore.Mvc.Rendering;
using ShelbyBackEnd.Infrastructure.Models;

namespace ShelbyBackEnd.Web.Models.ViewModels
{
    public class CategoriesVM
    {
       public Select_CategoriesResult? Category { get; set; }
        public IEnumerable<SelectListItem> categoryList {  get; set; }
    }
}
