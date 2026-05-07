using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyEComm.Services.Models;

namespace ShelbyBackEnd.Web.Models.ViewModels
{
    public class ProductsVM
    {

        public PaginatedList<Select_All_Products_ListResult> Products { get; set; }

        //[ValidateNever]
        //public IEnumerable<SelectListItem> SortByList { get; set; }
        //[ValidateNever]
        //public int SelectedSortById { get; set; }
        [ValidateNever]
        public int pagesize { get; set; }

        [ValidateNever]
        public int pageType { get; set; }


        [ValidateNever]
        public bool isProducts { get; set; }


    }

}
