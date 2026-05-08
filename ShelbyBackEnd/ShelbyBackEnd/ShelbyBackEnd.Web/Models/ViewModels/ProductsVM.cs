using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyEComm.Services.Models;

namespace ShelbyBackEnd.Web.Models.ViewModels
{
    public class ProductsVM
    {

        public PaginatedList<Select_All_Products_ListResult> Products { get; set; }

        public PaginatedList<Select_All_LowInventory_ProductsResult> lowInventoryProducts { get; set; }

   

        //[ValidateNever]
        //public IEnumerable<SelectListItem> SortByList { get; set; }
        //[ValidateNever]
        //public int SelectedSortById { get; set; }
        [ValidateNever]
        public int pagesize { get; set; }

        [ValidateNever]
        public int pageType { get; set; }


        [ValidateNever]
        public int totalPages { get; set; }

        [ValidateNever]
        public int currentPage { get; set; }

        [ValidateNever]
        public bool isProducts { get; set; } = false;

        [ValidateNever]
        public bool isLIProducts { get; set; } = false;

        [ValidateNever]
        public string Title { get; set; }
    }

}
