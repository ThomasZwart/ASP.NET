using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek!"};


            // When invoking view(model) the view method assigns the model (movie in this case) to the 'model' property in the dictionary viewData
            var viewResult = new ViewResult();
            var model = viewResult.ViewData.Model;

            return View(movie);
            //return Content("<h2>hi</hi>");
            //return HttpNotFound(); 
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy ="name" });
        }

        // Moet 'id' heten om met URL een parameter mee te geven, want zo heet t in de router
        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }

        // Optional parameters, int? is now nullable
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue) {
                pageIndex = 1;
            }

            if (string.IsNullOrWhiteSpace(sortBy)) {
                sortBy = "Name";
            }
            return Content(String.Format("pageIndex={0}$sortBy{1}", pageIndex, sortBy));
        }

        [Route("movie/released/{year:regex(^\\d{4}$)}/{month:regex(^\\d{2}$):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


    }
}