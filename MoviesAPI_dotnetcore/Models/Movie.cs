using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI_dotnetcore.Models
{
    public class Movie
    {
        
        public int ID { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(2500)]
        public string Story  { get; set; }

        [Required,Range(1,10)]
        public double  Rate  { get; set; }
        [Required]
        public  int  Year   { get; set; } 
        
        public byte[] Poster { get; set; } 


        public byte CategId { get; set; }  
        
        public Categ Categ { get; set; } 





    }
}
