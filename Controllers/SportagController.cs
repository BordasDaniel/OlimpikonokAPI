using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokAPI.Models;

namespace OlimpikonokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportagController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
                    List<Sportag> sportagak = context.Sportags.ToList();
                    return Ok(sportagak);
                }

            }catch (Exception ex)
            {
                List<Sportag> valasz = new();
                Sportag hiba = new()
                {
                    Id = -1,
                    Megnevezes = $"Hiba a betöltés közben: {ex.Message}",
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
                    Sportag valasz = context.Sportags.FirstOrDefault(o => o.Id == id);
                    return Ok(valasz);
                }
                catch (Exception ex)
                {
                    List<Sportag> valasz = new();
                    Sportag hiba = new()
                    {
                        Id = -1,
                        Megnevezes = $"Hiba a betöltés közben: {ex.Message}",
                    };
                    valasz.Add(hiba);
                    return BadRequest(valasz);

                }
            }
        }
    }
}
