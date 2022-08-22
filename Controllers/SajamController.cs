using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace JobFair.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SajamController : ControllerBase
    {
        public SajamContext Context { get; set; }

        public SajamController(SajamContext context)
        {
           Context = context;
        }
        [Route("PrikaziSajmove")]
        [HttpGet]
        public async Task<ActionResult> PrikaziSajmove()
        {
            return Ok(await Context.Sajmovi.Select( p => new {Id = p.ID, Naziv = p.Naziv}).ToListAsync());
        }

        [Route("PreuzetiSajmove")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiSveSajmove()
        {
            try
            {
                var sajmovi = await Context.Sajmovi
                .Select(p =>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv,
                    BrojSala = p.BrojSala,
                    Kompanije = p.Kompanije.Select( r =>
                        new
                        {
                            NazivKompanije = r.Naziv,
                            HR = r.HR

                        }               
                     )
                }).ToListAsync();
                return Ok(sajmovi);
            }
            catch(Exception e)
            {
                return BadRequest("Doslo je do greske:" + e.Message);
            }
        }

        [Route("PreuzetiKompanije/{SajamID}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiKompanije(int SajamID)
        {
            if(SajamID <= 0)
            {
                return BadRequest("Nepostojeci sajam!");
            }
            try
            {
                var sajam = Context.Sajmovi.Where(p=>p.ID == SajamID).FirstOrDefault();
                if (sajam == null)
                    return BadRequest("Ne postoji taj sajam!");
                var kompanije = await Context.Kompanije
                .Where(pr => pr.Sajam == sajam)
                .Select(p =>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv,
                    HR = p.HR,
                    Oblast = p.Oblast,
                    GodinaOsnivanja= p.GodinaOsnivanja                    
                    
                }).ToListAsync();
                return Ok(kompanije);
            }
            catch(Exception e)
            {
                return BadRequest("Doslo je do greske:" + e.Message);
            }
        }

        [Route("PreuzetiSale/{SajamID}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiSale(int SajamID)
        {
            if(SajamID <= 0)
            {
                return BadRequest("Nepostojeci sajam!");
            }
            try
            {
                var sajam = Context.Sajmovi.Where(p=>p.ID == SajamID).FirstOrDefault();
                if (sajam == null)
                    return BadRequest("Ne postoji taj sajam!");
                var sale = await Context.Sale
                .Where(pr => pr.Sajam == sajam)
                .Select(p =>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv                   
                    
                }).ToListAsync();
                return Ok(sale);
            }
            catch(Exception e)
            {
                return BadRequest("Doslo je do greske:" + e.Message);
            }
        }

        
    }
}
