using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW6.ViewModels
{
    public class ProductVM
    {

        public int product_id { get; set; }
        public int order_id { get; set; }
        public string product_name { get; set; }
        public decimal list_price { get; set; }
        public int quantity { get; set; }
    }
}