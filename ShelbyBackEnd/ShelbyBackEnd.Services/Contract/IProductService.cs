using ShelbyBackEnd.Infrastructure.Models;
using ShelbyEComm.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Contract
{
    public interface IProductService
    {

        public Task<PaginatedList<Select_All_Products_ListResult>> GetAllProducts(int? pageNumber, int? pageSize, string sortorder, CancellationToken cancellationToken = default);
        public Task<PaginatedList<Select_All_LowInventory_ProductsResult>> GetAllLowInventoryProducts(int? pageNumber, int? pageSize, string sortorder, CancellationToken cancellationToken = default);
        public  Task<List<Select_Search_ProductsResult>> GetSearchProducts(int? pageNumber, int? pageSize, string sortorder,
           string product_name = "", string product_code = "", string product_price = "", string product_weight = "", string tab_product_desc = "", int category_id = 0, CancellationToken cancellationToken = default);
    }
}
