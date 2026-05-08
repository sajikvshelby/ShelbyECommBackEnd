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
        public ProductService(ShelbyECommContext db)
        {
            _db = db;
        }

        public async Task<PaginatedList<Select_All_Products_ListResult>> GetAllProducts(int? pageNumber, int? pageSize, string sortorder, CancellationToken cancellationToken = default)
        {

            List<Select_All_Products_ListResult> lproduct = new List<Select_All_Products_ListResult>();
            lproduct = await _db.Procedures.Select_All_Products_ListAsync(cancellationToken: cancellationToken);


            if (!string.IsNullOrEmpty(sortorder))
            {
                lproduct = sortProducts(sortorder, lproduct);
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
                lproduct = lpsortProducts(sortorder, lproduct);
            }
            pageSize = pageSize == 1 ? lproduct.Count : pageSize;
            return await PaginatedList<Select_All_LowInventory_ProductsResult>.CreateAsync((lproduct), pageNumber ?? 1, pageSize ?? 20);

        }


        private List<Select_All_Products_ListResult> sortProducts(string sortorder, List<Select_All_Products_ListResult> lproduct)
        {
            switch (sortorder)
            {
                case "pc_desc":
                    lproduct = lproduct.OrderByDescending(e => e.product_code).ToList();
                    break;

                case "pn_asc":
                    lproduct = lproduct.OrderBy(s => s.product_name).ToList();
                    break;
                case "pn_desc":
                    lproduct = lproduct.OrderByDescending(s => s.product_name).ToList();
                    break;
                case "pp_desc":
                    lproduct = lproduct.OrderByDescending(e => e.product_price).ToList();
                    break;
                case "pp_asc":
                    lproduct = lproduct.OrderBy(e => e.product_price).ToList();
                    break;
                case "ss_desc":
                    lproduct = lproduct.OrderByDescending(e => e.stockstatus).ToList();
                    break;
                case "ss_asc":
                    lproduct = lproduct.OrderBy(e => e.stockstatus).ToList();
                    break;
                case "h_desc":
                    lproduct = lproduct.OrderByDescending(e => e.hidden).ToList();
                    break;
                case "h_asc":
                    lproduct = lproduct.OrderBy(e => e.hidden).ToList();
                    break;

                default:
                    lproduct = lproduct.OrderBy(e => e.product_code).ToList(); ;
                    break;
            }
            return lproduct;
        }

        private List<Select_All_LowInventory_ProductsResult> lpsortProducts(string sortorder, List<Select_All_LowInventory_ProductsResult> lproduct)
        {
            switch (sortorder)
            {
                case "pc_desc":
                    lproduct = lproduct.OrderByDescending(e => e.product_code).ToList();
                    break;

                case "pn_asc":
                    lproduct = lproduct.OrderBy(s => s.product_name).ToList();
                    break;
                case "pn_desc":
                    lproduct = lproduct.OrderByDescending(s => s.product_name).ToList();
                    break;
                case "pp_desc":
                    lproduct = lproduct.OrderByDescending(e => e.product_price).ToList();
                    break;
                case "pp_asc":
                    lproduct = lproduct.OrderBy(e => e.product_price).ToList();
                    break;
                case "ss_desc":
                    lproduct = lproduct.OrderByDescending(e => e.stockstatus).ToList();
                    break;
                case "ss_asc":
                    lproduct = lproduct.OrderBy(e => e.stockstatus).ToList();
                    break;
                case "h_desc":
                    lproduct = lproduct.OrderByDescending(e => e.hidden).ToList();
                    break;
                case "h_asc":
                    lproduct = lproduct.OrderBy(e => e.hidden).ToList();
                    break;




                case "lqa_desc":
                    lproduct = lproduct.OrderByDescending(e => e.stocklowqtyalarm).ToList();
                    break;
                case "lqa_asc":
                    lproduct = lproduct.OrderBy(e => e.stocklowqtyalarm).ToList();
                    break;

                case "rq_desc":
                    lproduct = lproduct.OrderByDescending(e => e.stockreorderqty).ToList();
                    break;
                case "rq_asc":
                    lproduct = lproduct.OrderBy(e => e.stockreorderqty).ToList();
                    break;

                case "atp_desc":
                    lproduct = lproduct.OrderByDescending(e => e.addtopo_now).ToList();
                    break;
                case "atp_asc":
                    lproduct = lproduct.OrderBy(e => e.addtopo_now).ToList();
                    break;

                case "lpq_desc":
                    lproduct = lproduct.OrderByDescending(e => e.lastpo_qty).ToList();
                    break;
                case "lpq_asc":
                    lproduct = lproduct.OrderBy(e => e.lastpo_qty).ToList();
                    break;

                case "lpd_desc":
                    lproduct = lproduct.OrderByDescending(e => e.lastpo_date).ToList();
                    break;
                case "lpd_asc":
                    lproduct = lproduct.OrderBy(e => e.lastpo_date).ToList();
                    break;

                default:
                    lproduct = lproduct.OrderBy(e => e.product_code).ToList(); ;
                    break;
            }
            return lproduct;
        }


    }
}
