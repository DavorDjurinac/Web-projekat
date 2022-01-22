using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("Soba")]
    public class SobaController : ControllerBase
    {
        public ContextKlasa Context { get; set; }
        public SobaController(ContextKlasa context)
        {
            Context = context;
        }

        [Route("/Soba/{idHotela}")]
        [HttpGet]
        public async Task<List<Soba>> PreuzmiSobe(int idHotela)
        {
            return await Context.Sobe.Where(soba => soba.Hotel.ID == idHotela).OrderBy(soba => soba.BrojSobe).ToListAsync();
        }

        [Route("/Soba/{idHotela}")]
        [HttpPost]
        public async Task UpisiSobu(int idHotela, [FromBody] Soba soba)
        {
            var hotel = await Context.Hoteli.FindAsync(idHotela);
            soba.Hotel = hotel;
            Context.Sobe.Add(soba);
            Console.WriteLine("Dodavanje sobe: ");
            await Context.SaveChangesAsync();
        }

        [Route("/Soba/")]
        [HttpPut]
        public async Task IzmeniSobu([FromBody] Soba soba)
        {
            Context.Update<Soba>(soba);
            await Context.SaveChangesAsync();
        }

        [Route("/Soba/{idSobe}")]
        [HttpDelete]
        public async Task IzbrisiSobu(int idSobe)
        {
            var soba = await Context.Sobe.FindAsync(idSobe);
            Context.Remove(soba);
            await Context.SaveChangesAsync();
        }

    }
}

