using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI_dotnetcore.Dto;
using MoviesAPI_dotnetcore.Models;

namespace MoviesAPI_dotnetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategsController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 
        public CategsController (ApplicationDbContext context) { _context = context; }  

        [HttpGet] 
        public async Task<IActionResult> Getall() 
        {
         IEnumerable<Categ> Categs = await _context.categs.OrderBy(O=>O.Name).ToListAsync();
         return Ok(Categs);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategDto categdto)  
        {
            Categ categ = new  Categ  { Name = categdto.Name} ;   
            
            await _context.categs.AddAsync(categ);  
            _context.SaveChanges();
            return Ok(categ); 
        }
        [HttpPut("{id}")] 
        public async Task<IActionResult> Update( int id, [FromBody]  CategDto categdto) 
        {
            var categ = await _context.categs.FirstOrDefaultAsync(C => C.ID == id);
            if (categ == null)
            {
                return NotFound("no categ found with this id");
            }   
            categ.Name = categdto.Name; 
            _context.SaveChanges();
            return Ok(categ); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id )
        {
            var categ = await _context.categs.FirstOrDefaultAsync(C => C.ID == id);
            if (categ == null)
            {
                return NotFound("no categ found with this id");
            } 
            _context.Remove(categ);
            _context.SaveChanges();
            return Ok("categ deleted successfully  !!! ");

        }   

    }
}
