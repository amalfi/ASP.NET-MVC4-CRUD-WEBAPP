using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC4_CRUD_WEBAPP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        
        }
    }
}