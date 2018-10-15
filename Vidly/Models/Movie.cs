using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models 
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        // Not required here, but it is on the ID which is the same for entity framework
        // This is because when posting a form you post the ID and genre stays null (which isnt allowed so an exception will be thrown)
        // So you put required on the ID rather than genre
        public Genre Genre { get; set; }

        // Required on the ID, not the genre
        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Please select a genre")]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

        public string Title {
            get {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }
    }
}