using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyEComm.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly ShelbyECommContext _db;
        public ProductService(ShelbyECommContext db)
        {
            _db = db;
        }
        
        public async Task<PaginatedList<Select_All_Products_ListResult>> GetAllProducts(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default)
        {
            var Products = await _db.Procedures.Select_All_Products_ListAsync( cancellationToken: cancellationToken);
            pageSize = pageSize == 1 ? Products.Count : pageSize;
            return await PaginatedList<Select_All_Products_ListResult>.CreateAsync((Products), pageNumber ?? 1, pageSize ?? 20);
            
        }

        public async Task<PaginatedList<Select_All_LowInventory_ProductsResult>> GetAllLowInventoryProducts(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default)
        { 
            var Products = await _db.Procedures.Select_All_LowInventory_ProductsAsync(cancellationToken: cancellationToken);
            pageSize = pageSize == 1 ? Products.Count : pageSize;
            return await PaginatedList<Select_All_LowInventory_ProductsResult>.CreateAsync((Products), pageNumber ?? 1, pageSize ?? 20);

        }
    }
}
