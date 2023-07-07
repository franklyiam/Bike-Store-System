using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW6.ViewModels
{
    public class OrderVM
    {
        public int order_id { get; set; }

        public string order_date { get; set; }

        public List<ProductVM> products { get; set; }

        public decimal total { get; set; }
    }
}