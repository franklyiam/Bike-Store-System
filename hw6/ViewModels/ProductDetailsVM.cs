using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW6.Models;

namespace HW6.ViewModels
{
    public class ProductDetailsVM
    {
       public product product { get; set; }

        public List<StoreVM> stores { get; set; }
    }
}