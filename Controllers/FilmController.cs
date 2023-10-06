using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models{
    [ApiController]
    [Route ("[controller]")]
    public class FilmController : ControllerBase{
        public Context _context;

        public FilmController(Context context){
            _context = context;
        }

        [Route("VratiSveFilmove")]
        [HttpGet]
        public async Task<ActionResult>VratiSveFilmove(){
            try{
                var film = await _context.Filmovi.ToListAsync();
                return Ok(film);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviFilm")]
        [HttpPost]
        public async Task<ActionResult>NapraviFilm(Film film){
            try{
                if(film.Ocena > 10 || film.Ocena < 1)
                return BadRequest("Ocena mora biti u obsegu izmedju 1 i 10");
                _context.Filmovi.Add(film);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljen film");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("OceniFilm")]
        [HttpPut]
        public async Task<ActionResult>OceniFilm(int idFilma, float ocena){
            try{
                var film = await _context.Filmovi.FindAsync(idFilma);
                if(film == null)
                return BadRequest("Film sa unetim Id-jem ne postoji");

                if(ocena < 1 || ocena > 10)
                return BadRequest("Ocena mora biti u obsegu izmedju 1 i 10");

                film.Ocena = (film.Ocena + ocena) / 2;

                await _context.SaveChangesAsync();
                return Ok("Uspesno ocenjen film. Nova ocena je sad: " + film.Ocena);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}