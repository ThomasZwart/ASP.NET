using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post(NewRentalDto newRentalDto)
        {
            // Not default because the API is internal
            var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId);

            var movies = _context.Movies.Where(c => newRentalDto.MovieIds.Contains(c.Id)).ToList();

            foreach (var movie in movies)
            {
                if ( movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available");
                }
                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);

            }
            _context.SaveChanges();

            // Not 'Created()' because there is not a single object but a list
            return Ok();
        }
    }
}