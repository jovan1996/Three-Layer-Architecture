using System.Web;
using System.Web.Mvc;

namespace jovan_somborski_63_15
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
