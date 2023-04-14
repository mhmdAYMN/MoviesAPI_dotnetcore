using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI_dotnetcore.Dto;
using MoviesAPI_dotnetcore.Models;

namespace MoviesAPI_dotnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase 
    { 
        private readonly ApplicationDbContext _context ;  
        private List<string> allowed_extension = new List<string> {".jpg", ".png" } ;
        private const long maxallowedsize = 1048576; 
        public MoviesController (ApplicationDbContext context) { _context = context; }







         // get all movies : 
         [HttpGet]
          public async Task<IActionResult> Get()
          {
               IEnumerable<Movie> movies = await _context.Movies.Include(G=>G.Categ).OrderByDescending(O=>O.Rate).ToListAsync(); 

              return Ok(movies);
          }

        // get movies by id : 
        [HttpGet("{id}")] 
        public async Task<IActionResult> Get(int id )
        {
           var movie = await _context.Movies.Include(G=>G.Categ).FirstOrDefaultAsync(O =>O.ID == id) ;
            if (movie is null) 
                return NotFound("no matched movie  with this id");

            return Ok(movie);
        }
        // get movies by categID : 
        [HttpGet("GetMoviesByCategId")] 
        public async Task<IActionResult> Get(byte categId)
        {
            var movies = await _context.Movies.Include(G => G.Categ).Where(O =>O.CategId == categId).ToListAsync();
            if (!movies.Any())  
                return NotFound("no matched categ with this id"); 
            return Ok(movies);  
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MoviesDto dto)
        {
            if (!allowed_extension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only png & jpg extension is allowed !!!");

            if (dto.Poster.Length > maxallowedsize)
                return BadRequest("max size is 1 mg ");

            using var image = new MemoryStream();
            await dto.Poster.CopyToAsync(image);
            Movie movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Story = dto.Story,
                Rate = dto.Rate,
                Poster = image.ToArray(),
                CategId = dto.CategId,



            };
            await _context.AddAsync(movie);
            _context.SaveChanges();



            return Ok(movie);

        }
        [HttpDelete("{id}")] 
        public async Task<IActionResult> Delete(int id) 
        {  
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
                return NotFound("no matched movie  with this id");
             _context.Movies.Remove(movie);
             await _context.SaveChangesAsync();


            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , [FromForm] MoviesDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null)
                return NotFound("No matched movie with this id ");
            if (!allowed_extension.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only png & jpg extension is allowed !!!");

            if (dto.Poster.Length > maxallowedsize)
                return BadRequest("max size is 1 mg ");

            using var image = new MemoryStream();
            await dto.Poster.CopyToAsync(image);


            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Story = dto.Story;
            movie.Rate = dto.Rate;
            movie.Poster = image.ToArray();
            movie.CategId = dto.CategId;

            _context.SaveChanges();


            return Ok(movie);
        }



    }
}
