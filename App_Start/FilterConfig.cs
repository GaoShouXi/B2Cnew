using B2CWebTemplate.Models;
using System.Web;
using System.Web.Mvc;

namespace B2CWebTemplate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ExceptionLogInfo());
            filters.Add(new ExpFilter());
        }
    }
}
