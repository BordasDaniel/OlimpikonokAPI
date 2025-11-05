using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
                    List<Orszag> orszagok = context.Orszags.ToList();
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
    }
}
