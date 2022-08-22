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
    public class KompanijaController : ControllerBase
    {
        public SajamContext Context { get; set; }
        public KompanijaController (SajamContext context)
        {
           Context = context;
        }

       
        [Route("PreuzmiTaskoveKompanije/{KompanijaID}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiTaskoveKompanije(int KompanijaID)
        {
            if(KompanijaID <= 0)
            {
                return BadRequest("Nepostojeca kompanija!");
            }
            
            try
            {
                var kompanija = Context.Kompanije.Where(p => p.ID == KompanijaID).FirstOrDefault();
                var taskovi = await Context.Taskovi.Where(p => p.Kompanija == kompanija)
                .Select(u =>
                new
                {
                    Pozicija= u.Pozicija,
                    Mentor = u.Mentor.Ime + " " + u.Mentor.Prezime
                }
                )
                .ToListAsync();
                return Ok(taskovi);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            } 
        }

        


    }
}
