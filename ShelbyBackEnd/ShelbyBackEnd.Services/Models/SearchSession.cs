using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Models
{
    public class SearchSession
    {
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string product_price_min { get; set; }
        public string product_price_max { get; set; }
        public string product_weight_min { get; set; }
        public string product_weight_max { get; set; }
        public string tab_product_desc { get; set; }
        //  public int parent_category_id { get; set; }
        public int category_id { get; set; }
        public bool inactive { get; set; }
        public bool restricted { get; set; }


    }
}
