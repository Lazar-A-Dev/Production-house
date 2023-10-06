using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class KategorijaController : ControllerBase
    {
        public Context _context;

        public KategorijaController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveKategorije")]
        [HttpGet]
        public async Task<ActionResult> VratiSveKategorije()
        {
            try
            {
                var kat = await _context.Kategorije.ToListAsync();
                return Ok(kat);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiSveFilmoveOveKategorije/{idKat}")]
        [HttpGet]
        public async Task<ActionResult> VratiSveFilmoveOveKategorije(int idKat)
        {
            try
            {
                var kat = await _context.Kategorije.Include(k => k.Filmovi).FirstOrDefaultAsync(k => k.ID == idKat);
                if (kat == null)
                    return BadRequest("Kategorija sa ovim id-jem ne postoji");

                //var filmovi = kat.Filmovi;
                return Ok(kat);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiBrojFilmova/{idKat}")]
        [HttpGet]
        public async Task<ActionResult> VratiBrojFilmova(int idKat)
        {
            try
            {
                var katNiz = _context.Kategorije.Where(k => k.ID == idKat).Select(k => k.Filmovi).FirstOrDefault();
                
                if (katNiz == null)
                    return BadRequest("Kategorija sa ovim id-jem ne postoji ili nema filmova");

                int brojFilmova = katNiz.Count(); // Broj filmova unutar kategorije
                if(brojFilmova <= 3){
                    return Ok(katNiz);
                }

                float min = 10;
                float max = 0;
                int middle = 0;
                foreach (var film in katNiz)
                {
                    if (max < film.Ocena)
                        max = film.Ocena;
                    if (min > film.Ocena)
                        min = film.Ocena;
                }

                middle = (int)Math.Round((max + min) / 2.0);

                int a = 0;
                int b = 0;
                int c = 0;

                List<Film> novaLista = new List<Film>();
                foreach (var film in katNiz)
                {
                    if (a == 0)
                    {
                        if (min == film.Ocena)
                        {
                            novaLista.Add(film);
                            a++;
                        }
                    }

                    if (b == 0)
                    {
                        if (middle == film.Ocena || middle - 1 == film.Ocena || middle + 1 == film.Ocena)
                        {
                            novaLista.Add(film);
                            b++;
                        }
                    }

                    if (c == 0)
                    {
                        if (max == film.Ocena)
                        {
                            novaLista.Add(film);
                            c++;
                        }
                    }

                }

                return Ok(novaLista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("NapraviKategoriju")]
        [HttpPost]
        public async Task<ActionResult> NapraviKategoriju(Kategorija kat)
        {
            try
            {
                _context.Kategorije.Add(kat);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljen film");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajFilmKategoriji")]
        [HttpPost]
        public async Task<ActionResult> DodajFilmKategoriji(int idKat, int idFilma)
        {
            try
            {
                var kat = await _context.Kategorije.Include(k => k.Filmovi).FirstOrDefaultAsync(k => k.ID == idKat);
                if (kat == null)
                    return BadRequest("Kategorija sa unetim id-jem ne postoji");

                var film = await _context.Filmovi.FindAsync(idFilma);
                if (film == null)
                    return BadRequest("Film sa unetim id-jem ne postoji");

                kat.Filmovi.Add(film);
                await _context.SaveChangesAsync();
                return Ok("Uspesno dodat film kategoriji");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}