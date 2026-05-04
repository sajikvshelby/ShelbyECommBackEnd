using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShelbyBackEnd.Infrastructure.Models;

namespace ShelbyBackEnd.Web.Models.ViewModels
{
    public class CategoriesVM
    {
        [ValidateNever]
        public Select_CategoriesResult? Category { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> categoryList {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> sortByList { get; set; }
    }
}
