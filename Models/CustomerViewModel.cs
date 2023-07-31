using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeBanHang.Models
{
    public class CustomerViewModel
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public int Payment { get; set; }

    }
}