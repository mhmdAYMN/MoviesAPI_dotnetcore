using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI_dotnetcore.Models
{
    public class Categ
    {
        [Required,DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public byte ID { get; set; }
        [Required,MaxLength(50)]
        public String Name { get; set; } 
        


    }
}
