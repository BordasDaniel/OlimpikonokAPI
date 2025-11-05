using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpikonokAPI.DTO;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SportoloController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
          using (var context = new OlimpikonokContext())
            {
                try
                {
                    var valasz = context.Sportolos.Include(s => s.Orszag).Include(s => s.Sportag).ToList();
                    return Ok(valasz);


                }
                catch (Exception ex)
                {
                    List<Sportolo> hiba = new();
                    Sportolo valasz = new()
                    {
                        Id = -1,
                        Nev = $"Hiba az adatok betöltése közben: {ex.Message}"
                    };
                    hiba.Add(valasz);
                    return BadRequest(hiba);
                }
            }
        }

        [HttpGet("SportoloOrszagSportagAll")]
        public IActionResult GetSportoloSOAll()
        {
           using (var context = new OlimpikonokContext())
           {
                try
                {
                    List<Sportolo> sportolok = context.Sportolos.Include(s => s.Orszag).Include(s => s.Sportag).ToList();
                    List<SportoloSO> valasz = new();
                    foreach(Sportolo sportolo in sportolok)
                    {
                        valasz.Add(new SportoloSO
                        {
                            Id = sportolo.Id,
                            Nev = sportolo.Nev,
                            Orszag = sportolo.Orszag.Nev,
                            Sportag = sportolo.Sportag.Megnevezes
                        });
                    }
                    return Ok(valasz);
                    


                } catch (Exception ex)
                {
                    List<SportoloSO> valasz = new();
                    SportoloSO hiba = new()
                    {
                        Id = -1,
                        Nev = $"Hiba az adatok betöltése közben {ex.Message}"
                    };
                    valasz.Add(hiba);
                    return BadRequest(valasz);
                }
           }


        }


    }
}
