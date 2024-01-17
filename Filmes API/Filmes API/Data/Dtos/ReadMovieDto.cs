using System.ComponentModel.DataAnnotations;

namespace Filmes_API.Data.Dtos
{
    public class ReadMovieDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public DateTime TimeChecked { get; set; } = DateTime.Now;   
    }
}
