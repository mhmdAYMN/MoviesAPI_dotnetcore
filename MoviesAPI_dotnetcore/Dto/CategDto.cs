using System.ComponentModel.DataAnnotations;

namespace MoviesAPI_dotnetcore.Dto
{
    public class CategDto
    {
        [Required , MaxLength(50)] 
        public string  Name { get; set; } 
    }
}
