using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using System.Data.Entity;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies.Include(c => c.Genre);

            if (!String.IsNullOrEmpty(query))
            {
                moviesQuery = moviesQuery.Where(c => c.Name.Contains(query));
            }

            return Ok(moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>));
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null) {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }


        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            // Resful convention returns the URI (unified resource identifier) of the newly created object
            // It is of form "/api/movies/{id}"
            return Created(new Uri(Request.RequestUri + "" + movie.Id), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movie)
        {
            if (!ModelState.IsValid) {
                BadRequest();
            }

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null) {
                NotFound();
            }

            Mapper.Map(movie, movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
