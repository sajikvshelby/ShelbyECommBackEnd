using AutoMapper;
using Microsoft.Data.SqlClient;
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
        private readonly IMapper _mapper;
        public ProductService(ShelbyECommContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PaginatedList<Select_All_Products_ListResult>> GetAllProducts(int? pageNumber, int? pageSize, string sortorder, CancellationToken cancellationToken = default)
        {

            List<Select_All_Products_ListResult> lproduct = new List<Select_All_Products_ListResult>();
            lproduct = await _db.Procedures.Select_All_Products_ListAsync(cancellationToken: cancellationToken);
            if (!string.IsNullOrEmpty(sortorder))
            {
                ProductSort productSort = new ProductSort();
                lproduct = productSort.sortProducts(sortorder, lproduct);
            }
            //pageSize = pageSize == 1 ? lproduct.Count : pageSize;
            //return await PaginatedList<Select_All_Products_ListResult>.CreateAsync((lproduct), pageNumber ?? 1, pageSize ?? 20);
            return await GetProductsPaginated(pageNumber, pageSize, sortorder, lproduct);

        }

        public async Task<PaginatedList<Select_All_Products_ListResult>> GetProductsPaginated(int? pageNumber, int? pageSize, string sortorder, List<Select_All_Products_ListResult> lproduct)
        {
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

        public async Task<List<Select_Search_ProductsResult>> GetSearchProducts(int? pageNumber, int? pageSize, string sortorder,
            string product_name= "",string product_code = "",string product_price = "",string product_weight = "",string tab_product_desc = "",int category_id = 0,           CancellationToken cancellationToken = default)
        {
        
            return await _db.Procedures.Select_Search_ProductsAsync(product_name, product_code, product_price, product_weight, tab_product_desc, category_id, cancellationToken: cancellationToken);
         
        }
    }
}
