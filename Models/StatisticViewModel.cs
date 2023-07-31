using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeBanHang.Models
{
    public class StatisticViewModel
    {
        public int ThisDay { get; set; }
        public int Yesterday { get; set; }
        public int ThisWeek { get; set; }
        public int LastWeek { get; set; }
        public int ThisMonth { get; set; }
        public int LastMonth { get; set; }
        public int SinceFirstDay { get; set; }
    }
    public class StatisticModel
    {
        public string ThisDay { get; set; }
        public string Yesterday { get; set; }
        public string ThisWeek { get; set; }
        public string LastWeek { get; set; }
        public string ThisMonth { get; set; }
        public string LastMonth { get; set; }
        public string SinceFirstDay { get; set; }
    }
}