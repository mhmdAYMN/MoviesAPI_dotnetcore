using System.ComponentModel.DataAnnotations;

namespace MoviesAPI_dotnetcore.Dto
{
    public class MoviesDto
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(2500)]
        public string Story { get; set; }

        [Required, Range(1, 10)]
        public double Rate { get; set; }
        [Required]
        public int Year { get; set; }

        public IFormFile Poster { get; set; }  


        public byte CategId { get; set; } 



    }
}
