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
    public class PrezentovanjeController : ControllerBase
    {
        public SajamContext Context { get; set; }

        public PrezentovanjeController(SajamContext context)
        {
           Context = context;
        }

        [Route("IzmeniPrezentovanje/{ID}/{IDSajam}/{IDSala}/{IDKompanija}/{Datum}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniPrezentovanje(int ID, int IDSajam, int IDSala, int IDKompanija, string Datum )
        {
            if(ID <= 0)
            {
                return BadRequest("Nevalidan ID prezentovanja!");
            }
           if(IDSajam <= 0)
            {
                return BadRequest("Nevalidan ID Sajma!");
            }

            if(IDSala <= 0)
            {
                return BadRequest("Nevalidan ID Sale!");
            }

            if(IDKompanija <= 0)
            {
                return BadRequest("Nevalidan ID Kompanije!");
            }

            
            try
            {
                var prezentovanjeZaIzmenu=Context.Prezentovanja.Find(ID);
                if(prezentovanjeZaIzmenu==null)
                return BadRequest("Ne postoji to prezentovanje!");

               var sajam = Context.Sajmovi.Find(IDSajam);
                
                if (sajam == null)
                    return BadRequest("Ne postoji taj sajam!");
                var kompanija = Context.Kompanije.Find(IDKompanija);
                if (kompanija == null)
                    return BadRequest("Ne postoji ta kompanija!");
                var sala = Context.Sale.Find(IDSala);
                if (sala == null)
                    return BadRequest("Ne postoji ta sala!");
                var datum = DateTime.ParseExact(Datum, "yyyy-MM-dd HH:mm", null);


                 var kompanijaZaProveru = Context.Kompanije.Where(p=> p==kompanija)
                                            .Include(p => p.Sajam)
                                            .Where(p => p.Sajam == sajam)
                                            .FirstOrDefault();
                if (kompanijaZaProveru == null)
                {
                    return BadRequest("Izabrana kompanija ne pripada tom sajmu");
                }
                
                var salaZaProveru = Context.Sale.Where(p=> p==sala)
                                            .Include(p => p.Sajam)
                                            .Where(p => p.Sajam == sajam)
                                            .FirstOrDefault();
                if (salaZaProveru == null)
                {
                    return BadRequest("Izabrana sala ne pripada tom sajmu");
                }

                 var prezentovanjeZaProveru = Context.Prezentovanja.Where(p => DateTime.Compare(p.Datum.Date,datum.Date)==0)
                                            .Include(p => p.Sala)
                                            .Where(p => p.Sala == sala)
                                            .FirstOrDefault();
                if (prezentovanjeZaProveru != null && prezentovanjeZaIzmenu.ID != prezentovanjeZaProveru.ID)
                {
                    return BadRequest("Izabrana sala je vec zauzeta tog dana!");
                }
                
                prezentovanjeZaIzmenu.Sajam=sajam;
                prezentovanjeZaIzmenu.Kompanija=kompanija;
                prezentovanjeZaIzmenu.Datum=datum;
                prezentovanjeZaIzmenu.Sala=sala;



                Context.Prezentovanja.Update(prezentovanjeZaIzmenu);
                await Context.SaveChangesAsync();
                return Ok("Zamenjeno prezentovanje!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodatiPrezentovanje/{IDSajam}/{IDSala}/{IDKompanija}/{Datum}")]
        [HttpPost]
        public async Task<ActionResult> DodajPrezentovanje(int IDSajam, int IDSala, int IDKompanija, string Datum)
        {
            if(IDSajam <= 0)
            {
                return BadRequest("Nevalidan ID Sajma!");
            }

            if(IDSala <= 0)
            {
                return BadRequest("Nevalidan ID Sale!");
            }

            if(IDKompanija <= 0)
            {
                return BadRequest("Nevalidan ID Kompanije!");
            }

            try
            {
                var sajam = Context.Sajmovi.Find(IDSajam);
                
                if (sajam == null)
                    return BadRequest("Ne postoji taj sajam!");
                var kompanija = Context.Kompanije.Find(IDKompanija);
                if (kompanija == null)
                    return BadRequest("Ne postoji ta predstava!");
                var sala=  Context.Sale.Find(IDSala);
                if (sala == null)
                    return BadRequest("Ne postoji ta sala!");
                var datum = DateTime.ParseExact(Datum, "yyyy-MM-dd HH:mm", null);

                

                var kompanijaZaProveru = Context.Kompanije.Where(p=> p==kompanija)
                                            .Include(p => p.Sajam)
                                            .Where(p => p.Sajam == sajam)
                                            .FirstOrDefault();
                if (kompanijaZaProveru == null)
                {
                    return BadRequest("Izabrana kompanija ne pripada tom sajmu");
                }
                
                var salaZaProveru = Context.Sale.Where(p=> p==sala)
                                            .Include(p => p.Sajam)
                                            .Where(p => p.Sajam == sajam)
                                            .FirstOrDefault();
                if (salaZaProveru == null)
                {
                    return BadRequest("Izabrana sala ne pripada tom sajmu");
                }

                var prezentovanjeZaProveru = Context.Prezentovanja.Where(p => DateTime.Compare(p.Datum.Date,datum.Date)==0)
                                            .Include(p => p.Sala)
                                            .Where(p => p.Sala == sala)
                                            .FirstOrDefault();
                if (prezentovanjeZaProveru != null)
                {
                    return BadRequest("Izabrana sala je vec zauzeta tog dana!");
                }

                Prezentovanje i = new Prezentovanje
                {
                    
                    Kompanija = kompanija,
                    Sajam = sajam,
                    Sala = sala,
                    Datum = datum
                };
                Context.Prezentovanja.Add(i);
               
               
                await Context.SaveChangesAsync();
               
                return Ok("Uspesno dodato prezentovanje!");
            }
            catch(Exception e)
            {
                return BadRequest("Doslo je do greske:" + e.Message);
            }
        }

        [Route("ObrisatiPrezentovanje/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiPrezentovanje(int id)
        {
            if(id<=0)
            {
                return BadRequest("Pogresan broj id-a!");
            }

            try
            {
                var izv = await Context.Prezentovanja.FindAsync(id);
                if (izv == null)
                    return BadRequest("Ne postoji prezentovanje!");

                Context.Prezentovanja.Remove(izv);
                await Context.SaveChangesAsync();
                return Ok("Izbrisano prezentovanje!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }



        }

        [Route("PreuzetiRasporedSajma/{SajamID}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiRasporedSajma( int SajamID)
        {
            if(SajamID <= 0)
            {
                return BadRequest("Nepostojece!");
            }
           
            try
            {
                var sajam= Context.Sajmovi.Where(p => p.ID == SajamID).FirstOrDefault();
                if(sajam==null)
                return BadRequest("Ne postoji taj sajam!");
                 var prezentovanja = await Context.Prezentovanja.Where(i => i.Sajam==sajam)
                 .Select(m =>                  
                 new
                 {
                    ID=m.ID,
                    Datum = m.Datum.ToShortDateString(),
                    Vreme = m.Datum.ToShortTimeString(),
                    Sala = m.Sala.Naziv,
                    Kompanija = m.Kompanija.Naziv
                 }
                 )
                 .ToListAsync();

                
                return Ok(prezentovanja);
            }
            catch(Exception e)
            {
                return BadRequest("Doslo je do greske:" + e.Message);
            }
        }



        [Route("DodatiPrezentovanje2/{IDSajam}/{IDSala}/{IDKompanija}/{Datum}")]
        [HttpPost]
        public async Task<ActionResult> DodajPrezentovanje2(int IDSajam, int IDSala, int IDKompanija, DateTime Datum)
        {
            if(IDSajam <= 0)
            {
                return BadRequest("Nevalidan ID Sajma!");
            }

            if(IDSala <= 0)
            {
                return BadRequest("Nevalidan ID Sale!");
            }

            if(IDKompanija <= 0)
            {
                return BadRequest("Nevalidan ID Kompanije!");
            }

            try
            {
                var sajam = Context.Sajmovi.Find(IDSajam);
                
                if (sajam == null)
                    return BadRequest("Ne postoji taj sajam!");
                var kompanija = Context.Kompanije.Find(IDKompanija);
                if (kompanija == null)
                    return BadRequest("Ne postoji ta kompanija!");
                var sala=  Context.Sale.Find(IDSala);
                if (sala == null)
                    return BadRequest("Ne postoji ta sala!");
                var datum = Datum;

                

                var kompanijaZaProveru = Context.Kompanije.Where(p=> p==kompanija)
                                            .Include(p => p.Sajam)
                                            .Where(p => p.Sajam == sajam)
                                            .FirstOrDefault();
                if (kompanijaZaProveru == null)
                {
                    return BadRequest("Izabrana kompanija ne pripada tom sajmu");
                }
                
                var salaZaProveru = Context.Sale.Where(p=> p==sala)
                                            .Include(p => p.Sajam)
                                            .Where(p => p.Sajam == sajam)
                                            .FirstOrDefault();
                if (salaZaProveru == null)
                {
                    return BadRequest("Izabrana sala ne pripada tom sajmu");
                }

                var prezentovanjeZaProveru = Context.Prezentovanja.Where(p => DateTime.Compare(p.Datum.Date,datum.Date)==0)
                                            .Include(p => p.Sala)
                                            .Where(p => p.Sala == sala)
                                            .FirstOrDefault();
                if (prezentovanjeZaProveru != null)
                {
                    return BadRequest("Izabrana sala je vec zauzeta tog dana!");
                }

                Prezentovanje i = new Prezentovanje
                {
                    
                    Kompanija = kompanija,
                    Sajam = sajam,
                    Sala = sala,
                    Datum=datum
                };
                Context.Prezentovanja.Add(i);
               
               
                await Context.SaveChangesAsync();
               
                return Ok("Uspesno dodato prezentovanje na drugi nacin!");
            }
            catch(Exception e)
            {
                return BadRequest("Doslo je do greske:" + e.Message);
            }
        }
       
    }
}
