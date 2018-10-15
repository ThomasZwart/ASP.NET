using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Movie required")]
        public Movie Movie { get; set; }

        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}