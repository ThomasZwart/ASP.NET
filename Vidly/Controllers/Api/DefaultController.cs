using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers.Api
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return Content("apennoten");
        }
    }
}