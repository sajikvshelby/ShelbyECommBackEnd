using AutoMapper;
using Microsoft.Data.SqlClient;
using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using ShelbyBackEnd.Services.Models;
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

        public async Task<PaginatedList<Select_All_Products_ListResult>> GetAllProducts(int? pageNumber, int? pageSize, string sortorder, SearchSession searchSession, CancellationToken cancellationToken = default)
        {
            List<Select_All_Products_ListResult> lproduct = new List<Select_All_Products_ListResult>();
            lproduct = await _db.Procedures.Select_All_Products_ListAsync(searchSession.product_name, searchSession.product_code, searchSession.product_price_min, searchSession.product_price_max, searchSession.product_weight_min, searchSession.product_weight_max, searchSession.tab_product_desc, searchSession.category_id,
                searchSession.inactive, searchSession.restricted,
                cancellationToken: cancellationToken);
            if (!string.IsNullOrEmpty(sortorder))
            {
                ProductSort productSort = new ProductSort();
                lproduct = productSort.sortProducts(sortorder, lproduct);
            }
            pageSize = pageSize == 1 ? lproduct.Count : pageSize;
            return await PaginatedList<Select_All_Products_ListResult>.CreateAsync((lproduct), pageNumber ?? 1, pageSize ?? 20);
        }
        public async Task<PaginatedList<Select_All_LowInventory_ProductsResult>> GetAllLowInventoryProducts(int? pageNumber, int? pageSize, string sortorder, CancellationToken cancellationToken = default)
        {
            List<Select_All_LowInventory_ProductsResult> lproduct = new List<Select_All_LowInventory_ProductsResult>();
            lproduct = await _db.Procedures.Select_All_LowInventory_ProductsAsync(cancellationToken: cancellationToken);

            if (!string.IsNullOrEmpty(sortorder))
            {
                ProductSort productSort = new ProductSort();
                lproduct = productSort.lpsortProducts(sortorder, lproduct);
            }
            pageSize = pageSize == 1 ? lproduct.Count : pageSize;
            return await PaginatedList<Select_All_LowInventory_ProductsResult>.CreateAsync((lproduct), pageNumber ?? 1, pageSize ?? 20);
        }


    }
}
