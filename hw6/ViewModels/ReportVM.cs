using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW6.ViewModels
{
    public class ReportVM
    {
          
        public int count { get; set; }

        public string month { get; set; }

        public ReportVM(int c, string m) {

            count = c;
            month = m;
        }
    }
}