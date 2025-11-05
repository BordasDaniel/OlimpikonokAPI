using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
           throw new NotImplementedException();
        }


    }
}
