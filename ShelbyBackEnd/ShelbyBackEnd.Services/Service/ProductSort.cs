using ShelbyBackEnd.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Service
{
    public class ProductSort
    {

        public List<Select_All_Products_ListResult> sortProducts(string sortorder, List<Select_All_Products_ListResult> lproduct)
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


        public List<Select_Search_ProductsResult> sortSearchProducts(string sortorder, List<Select_Search_ProductsResult> lproduct)
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
        public List<Select_All_LowInventory_ProductsResult> lpsortProducts(string sortorder, List<Select_All_LowInventory_ProductsResult> lproduct)
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
