using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WeBanHang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["ThisDay"] = 0;
            Application["Yesterday"] = 0;
            Application["ThisWeek"] = 0;
            Application["LastWeek"] = 0;
            Application["ThisMonth"] = 0;
            Application["LastMonth"] = 0;
            Application["SinceFirstDay"] = 0;
            Application["visitors_online"] = 0;

        }
        void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 150;
            Application.Lock();
            Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"])+1;
            Application.UnLock();
            try
            {
                var item = WeBanHang.Models.Commons.StatisticalAccess.Statistic();
                if (item != null )
                {
                    Application["ThisDay"] = long.Parse("0" + item.ThisDay.ToString("#,###"));
                    Application["Yesterday"] = long.Parse("0" + item.Yesterday.ToString("#,###"));
                    Application["ThisWeek"] = long.Parse("0" + item.ThisWeek.ToString("#,###"));
                    Application["LastWeek"] = long.Parse("0" + item.LastWeek.ToString("#,###"));
                    Application["ThisMonth"] = long.Parse("0" + item.ThisMonth.ToString("#,###"));
                    Application["LastMonth"] = long.Parse("0" + item.LastMonth.ToString("#,###"));
                    Application["SinceFirstDay"] = long.Parse(item.SinceFirstDay.ToString("#,###"));
                }
            }
            catch(Exception ex)
            {

            }
        }
        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["visitors_online"] = Convert.ToInt32(Application["visitors_online"]) - 1;
            Application.UnLock();
        }
    }
}
