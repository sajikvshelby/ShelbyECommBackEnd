using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShelbyBackEnd.Infrastructure.Models;

namespace ShelbyBackEnd.Web.Models.ViewModels
{
    public class CategoriesVM
    {
        [ValidateNever]
        public Select_All_CategoriesResult? Category { get; set; }
     
        [ValidateNever]
        public IEnumerable<SelectListItem> categoryList {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> subCategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> sortByList { get; set; }

        [ValidateNever]
        public int maincategoryid { get; set; }

        [ValidateNever]
        public int subcategoryid { get; set; }

        [ValidateNever]
        public string ctg { get; set; }

        [ValidateNever]
        public string Title { get; set; }

    }
}
