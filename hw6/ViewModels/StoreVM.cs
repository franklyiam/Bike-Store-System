using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW6.ViewModels
{
    public class StoreVM
    {
        public int store_id { get; set; }
        public string store_name { get; set; }
        public int? count { get; set; }
    }
}