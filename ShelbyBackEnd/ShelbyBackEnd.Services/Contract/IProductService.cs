using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Models;
using ShelbyEComm.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Contract
{
    public interface IProductService
    {
        public  Task<PaginatedList<Select_All_Products_ListResult>> GetAllProducts(int? pageNumber, int? pageSize, string sortorder, CancellationToken cancellationToken = default);

        public Task<PaginatedList<Select_All_LowInventory_ProductsResult>> GetAllLowInventoryProducts(int? pageNumber, int? pageSize, string sortorder, CancellationToken cancellationToken = default);
        public Task<PaginatedList<Select_All_search_ProductsResult>> GetAllSearchProducts(int? pageNumber, int? pageSize, string sortorder, SearchSession searchSession, CancellationToken cancellationToken = default);



    }
}
