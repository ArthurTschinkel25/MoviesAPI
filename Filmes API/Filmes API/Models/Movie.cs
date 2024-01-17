using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies_API.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "A correct title is required")]
        [MaxLength(50, ErrorMessage = "The title's size cannot exceed 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "A correct genre is required")]
        [MaxLength(50, ErrorMessage = "The genre's size cannot exceed 50 characters")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "A correct duration of the movie is required")]
        [Range(70, 600, ErrorMessage = "The movie's duration must be between 70 and 600 minutes")]
        public int Duration { get; set; }

    }
}