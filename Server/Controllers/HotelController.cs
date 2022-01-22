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
    public class HotelController : ControllerBase
    {
        public ContextKlasa Context { get; set; }
        public HotelController(ContextKlasa context)
        {
            Context = context;
        }

        [Route("/Hotel/")]
        [HttpGet]
        public async Task<List<Hotel>> PreuzmiHotele()
        {
            return await Context.Hoteli.Include(h => h.Sobe).Include(h => h.Racuni).ToListAsync();
        }

        [Route("/Hotel/")]
        [HttpPost]
        public async Task<Hotel> UpisiHotel([FromBody] Hotel hotel)
        {
            Context.Hoteli.Add(hotel);
            await Context.SaveChangesAsync();
            var hotelResp = await Context.Hoteli.ToListAsync();
            return hotelResp.Where(h=> h.Naziv==hotel.Naziv && h.Adresa==hotel.Adresa && h.BrojSobaPoTipu==hotel.BrojSobaPoTipu).FirstOrDefault();
        }

        [Route("/Hotel/")]
        [HttpPut]
        public async Task IzmeniHotel([FromBody] Hotel hotel)
        {
            Context.Update<Hotel>(hotel);
            await Context.SaveChangesAsync();
        }

        [Route("/Hotel/")]
        [HttpDelete]
        public async Task IzbrisiHotel(int id)
        {
            var nizSoba = Context.Sobe.Where(s => s.Hotel.ID == id);
            await nizSoba.ForEachAsync(s =>
           {
               Context.Remove(s);
           });

            var nizRacuna = Context.Racuni.Where(r => r.Hotel.ID == id);
            await nizRacuna.ForEachAsync(r =>
           {
               Context.Remove(r);
           });

            var hotel = await Context.Hoteli.FindAsync(id);
            Context.Remove(hotel);
            await Context.SaveChangesAsync();
        }

    }
}
