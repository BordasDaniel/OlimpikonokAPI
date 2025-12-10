using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
                    List<Orszag> orszagok = await context.Orszags.ToListAsync();
                    return Ok(orszagok);
                }

            }catch (Exception ex)
            {
                List<Orszag> valasz = new();
                Orszag hiba = new()
                {
                    Id = -1,
                    Nev = $"Hiba a betöltés közben: {ex.Message}",
                };
                valasz.Add(hiba);
                return BadRequest(valasz);
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            using (var context = new OlimpikonokContext())
            {
                try
                {
                    Orszag valasz = context.Orszags.FirstOrDefault(o => o.Id == id);
                    if (valasz != null)
                    {
                        return Ok(valasz);
                    }
                    else
                    {
                        Orszag hiba = new()
                        {
                            Id = -1,
                            Nev = $"Hiba a betöltés közben",
                        };
                        return StatusCode(404, hiba);
                    }
                }
                catch (Exception ex)
                {
                    Orszag hiba = new()
                    {
                        Id = -1,
                        Nev = $"Hiba a betöltés közben: {ex.Message}",
                    };
                    return BadRequest(hiba);
                }
            }
        }

        [HttpPut("ModositOrszag")]
        public async Task<IActionResult> PutOrszag(Orszag orszag)
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
              
                    if (!context.Orszags.Select(o => o.Id).Contains(orszag.Id))
                    {
                        context.Orszags.Update(orszag);
                        await context.SaveChangesAsync();
                        return Ok("Sikeres módosítás");
                    }
                    else
                    {
                        return BadRequest("Hiba nincs ilyen!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UjOrszag")]
        public async Task<IActionResult> PostOrszag(Orszag orszag)
        {
            using(var context  = new OlimpikonokContext())
            {
                try
                {
                    List<Orszag> orszagok = context.Orszags.ToList();
                    orszagok.Add(orszag);
                    await context.SaveChangesAsync();
                    return Ok("Sikeres hozzáadás!");

                } catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
        }
    }
}
