using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("")]
    public class RacunController : ControllerBase
    {
        public ContextKlasa Context { get; set; }
        public RacunController(ContextKlasa context)
        {
            Context = context;
        }

        [Route("/Racun/{idHotela}")]
        [HttpGet]
        public async Task<List<Racun>> PreuzmiRacune(int idHotela)
        {
            return await Context.Racuni.Where(racun => racun.Hotel.ID == idHotela).OrderBy(racun => racun.BrojSobe).ToListAsync();
        }

        [Route("/Racun/{idHotela}")]
        [HttpPost]
        public async Task UpisiRacun(int idHotela, [FromBody] Racun racun)
        {
            var hotel = await Context.Hoteli.FindAsync(idHotela);
            racun.Hotel = hotel;
            Context.Racuni.Add(racun);
            await Context.SaveChangesAsync();
        }

        [Route("/Racun/")]
        [HttpPut]
        public async Task IzmeniRacun([FromBody] Racun racun)
        {
            Context.Update<Racun>(racun);
            await Context.SaveChangesAsync();
        }

        [Route("/Racun/{idRacuna}")]
        [HttpDelete]
        public async Task IzbrisiRacun(int idRacuna)
        {
            var racun = await Context.Racuni.FindAsync(idRacuna);
            Context.Remove(racun);
            await Context.SaveChangesAsync();
        }
    }

}