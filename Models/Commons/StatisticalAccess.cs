using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace WeBanHang.Models.Commons
{
    public class StatisticalAccess
    {
        public static string strConnection = ConfigurationManager.ConnectionStrings["DBWebBanHang"].ToString();
        public static StatisticViewModel Statistic()
        {
            using (var cnn = new SqlConnection(strConnection))
            {
                var item = cnn.QueryFirstOrDefault<StatisticViewModel>("sp_ThongKe2", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}