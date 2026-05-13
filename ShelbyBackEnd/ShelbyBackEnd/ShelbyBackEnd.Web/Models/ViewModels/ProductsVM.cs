using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyEComm.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace ShelbyBackEnd.Web.Models.ViewModels
{
    public class ProductsVM
    {
        
        public PaginatedList<Select_All_Products_ListResult> Products { get; set; }

        public PaginatedList<Select_All_search_ProductsResult> SearchProducts { get; set; }
        public PaginatedList<Select_All_LowInventory_ProductsResult> lowInventoryProducts { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> categoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> subCategoryList { get; set; }



        [ValidateNever]
        public Select_ProductResult? Product { get; set; }

        [ValidateNever]
        public int parent_category_id { get; set; }

        [ValidateNever]
        public int category_id { get; set; }


        [ValidateNever]
        public bool isInactive { get; set; } = false;

        [ValidateNever]
        public bool isRestricted { get; set; } = false;


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
        public bool isProductSearch { get; set; } = false;


        [ValidateNever]
        public bool isLIProducts { get; set; } = false;

        [ValidateNever]
        public string Title { get; set; }

         public string product_price_min { get; set; }

        public string product_price_max { get; set; }
        public string product_weight_min { get; set; }

        public string product_weight_max { get; set; }




    }

}
