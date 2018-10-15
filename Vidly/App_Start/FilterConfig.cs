using System.Web;
using System.Web.Mvc;

namespace Vidly {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());

            // The site can only be accessed by logged-in users
            filters.Add(new AuthorizeAttribute());

            // The site can only be accessed via Https protocol
         //   filters.Add(new RequireHttpsAttribute());
        }
    }
}
