using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models{
    [ApiController]
    [Route ("[controller]")]
    public class ProdukcijaController : ControllerBase{
        public Context _context;

        public ProdukcijaController(Context context){
            _context = context;
        }

        [Route("VratiSveProdukcije")]
        [HttpGet]
        public async Task<ActionResult>VratiSveProdukcije(){
            try{
                var pro = await _context.Produkcije.Include(p => p.Kategorije).ToListAsync();
                return Ok(pro);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("VratiSveKategorijeOveProdukcije")]
        [HttpGet]
        public async Task<ActionResult>VratiSveKategorijeOveProdukcije(int idPro){
            try{
                var pro = await _context.Produkcije.Include(p => p.Kategorije).FirstOrDefaultAsync(p => p.ID == idPro);
                if(pro == null)
                return BadRequest("Produkcija sa ovim id-jem ne postoji");

                var kat = pro.Kategorije;
                return Ok(kat);
                
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviProdukciju")]
        [HttpPost]
        public async Task<ActionResult>NapraviProdukciju(Produkcija pro){
            try{
                _context.Produkcije.Add(pro);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena produkcija");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajKategorijuProdukciji")]
        [HttpPost]
        public async Task<ActionResult>DodajKategorijuProdukciji(int idPro ,int idKat){
            try{
                var pro = await _context.Produkcije.Include(p => p.Kategorije).FirstOrDefaultAsync(p => p.ID == idPro);
                if(pro == null)
                    return BadRequest("Produkcija sa unetim id-jem ne postoji");

                var kat = await _context.Kategorije.FindAsync(idKat);
                if(kat == null)
                    return BadRequest("Kategorija sa unetim id-jem ne postoji");

                pro.Kategorije.Add(kat);
                await _context.SaveChangesAsync();
                return Ok("Uspesno dodata kategorija produkciji");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        
    }
}