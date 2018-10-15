using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        // Not required here, but it is on the ID which is the same for entity framework
        // This is because when posting a form you post the ID and genre stays null (which isnt allowed so an exception will be thrown)
        // So you put required on the ID rather than genre
        public GenreDto Genre { get; set; }

        // Required on the ID, not the genre
        [Required]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(1, 20)]
        public byte NumberInStock { get; set; }

        public int NumberAvailable { get; set; }
    }
}