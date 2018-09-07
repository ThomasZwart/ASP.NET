using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var viewModel = new MoviesFormViewModel() {
                Genres = _context.Genres.ToList(),
            };

            return View("MoviesForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            // If the form values are not valid, user will be send back to the form with validation error messages
            if (!ModelState.IsValid) {
                var viewModel = new MoviesFormViewModel(movie) {
                    Genres = _context.Genres.ToList(),
                };
                return View("MoviesForm", viewModel);
            }

            if (movie.Id == 0){
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null) {
                return HttpNotFound();
            }

            var viewModel = new MoviesFormViewModel(movie) {
                Genres = _context.Genres.ToList(),
            };

            return View("MoviesForm", viewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null) {
                return HttpNotFound();
            }

            return View(movie);
        }

        /*

        public ActionResult Random()
        {
            var movie = new List<Movie>() { new Movie { Name = "Shrek!", Id = 1 } };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1", Id = 1},
                new Customer { Name = "Customer 2", Id = 2},
                new Customer { Name = "Customer 3", Id = 3},
                new Customer { Name = "Customer 4", Id = 4},
            };

            var viewModel = new RandomMovieViewModel {
                Movies = movie,
                Customers = customers
            };

            return View(viewModel);

            // When invoking view(model) the view method assigns the model (movie in this case) to the 'model' property in the dictionary viewData
            //var viewResult = new ViewResult();
            //var model = viewResult.ViewData.Model;


            //return Content("<h2>hi</hi>");
            //return HttpNotFound(); 
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy ="name" });
        }

        // Optional parameters, int? is now nullable
        public ActionResult OptPara(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue) {
                pageIndex = 1;
            }

            if (string.IsNullOrWhiteSpace(sortBy)) {
                sortBy = "Name";
            }
            return Content(String.Format("pageIndex={0}$sortBy{1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year:regex(^\\d{4}$)}/{month:regex(^\\d{2}$):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        */
    }
}