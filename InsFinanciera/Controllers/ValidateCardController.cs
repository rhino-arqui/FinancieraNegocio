using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using InsFinanciera.Models;

namespace InsFinanciera.Controllers
{
    public class ValidateCardController : ApiController
    {
        private masterEntitiesHistorial2 db = new masterEntitiesHistorial2();
        private masterEntities decard = new masterEntities();
        // GET: api/ValidateCard
        public IQueryable<Historial> GetHistorial()
        {
            return db.Historial;
        }

        // GET: api/ValidateCard/5
        [ResponseType(typeof(Historial))]
        public IHttpActionResult GetHistorial(int id)
        {
            Historial historial = db.Historial.Find(id);
            if (historial == null)
            {
                return NotFound();
            }

            return Ok(historial);
        }

        // PUT: api/CARDS
        // CASO DE USO
        [ResponseType(typeof(Respuesta))]
        public IHttpActionResult PutCARDS(Peticion pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CARDS cARDS = decard.CARDS.Find(pet.Doc);

            System.Diagnostics.Debug.WriteLine("aaaaaaaaaaa", cARDS);
            System.Diagnostics.Debug.WriteLine(cARDS.Pass.ToString());
            System.Diagnostics.Debug.WriteLine(pet.Pass.ToString());
            System.Diagnostics.Debug.WriteLine(cARDS.Avaliable);
            System.Diagnostics.Debug.WriteLine(pet.Value);
            System.Diagnostics.Debug.WriteLine(cARDS.Pass.Trim(' ').Equals(pet.Pass.Trim(' ')));
            System.Diagnostics.Debug.WriteLine(cARDS.Avaliable >= pet.Value);

            Respuesta response = new Respuesta();
            if (cARDS.Pass.Trim(' ').Equals(pet.Pass.Trim(' ')) && cARDS.Avaliable >= pet.Value)
            {
                cARDS.Avaliable = cARDS.Avaliable - pet.Value;
                decard.Entry(cARDS).State = EntityState.Modified;

                System.Diagnostics.Debug.WriteLine("aaaaaaaaaaa");
                DateTime thisDay = DateTime.Today;
                Historial history = new Historial();
                history.Id = 0;
                history.Doc = pet.Doc;
                System.Diagnostics.Debug.WriteLine("aaaaaaaaaaa");
                history.Fecha = thisDay.ToString();
                history.Pass = pet.Pass.Trim(' ');
                System.Diagnostics.Debug.WriteLine("aaaaaaaaaaa");
                history.Value = pet.Value;
                System.Diagnostics.Debug.WriteLine("aaaaaaaaaaa1");
          
                db.Historial.Add(history);
                db.SaveChanges();
                System.Diagnostics.Debug.WriteLine("aaaaaaaaaaa2");

                response.NumAprobacion = history.Id;
                response.FechaAprobacion = thisDay.ToString();
                System.Diagnostics.Debug.WriteLine("aaaaaaaaaaa3");

            }
            else
            {

                response.NumAprobacion = 0;
                DateTime thisDayMal = DateTime.Today;
                response.FechaAprobacion = thisDayMal.ToString();
                return Ok(response);
            }

            
                decard.SaveChanges();
               
            
         
            return Ok(response);

        }

        // PUT: api/ValidateCard/5
        /*[ResponseType(typeof(void))]
        public IHttpActionResult PutHistorial(int id, Historial historial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historial.Id)
            {
                return BadRequest();
            }

            db.Entry(historial).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        */
        // POST: api/ValidateCard
        [ResponseType(typeof(Historial))]
        public IHttpActionResult PostHistorial(Historial historial)
        {
            //System.Diagnostics.Debug.WriteLine(historial.Id);
            /*System.Diagnostics.Debug.WriteLine(historial.Fecha);
            System.Diagnostics.Debug.WriteLine(historial.Doc);
            System.Diagnostics.Debug.WriteLine(historial.Pass);
            System.Diagnostics.Debug.WriteLine(historial.TipoDoc);
            */
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Historial.Add(historial);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = historial.Id }, historial);
        }

        // DELETE: api/ValidateCard/5
        [ResponseType(typeof(Historial))]
        public IHttpActionResult DeleteHistorial(int id)
        {
            Historial historial = db.Historial.Find(id);
            if (historial == null)
            {
                return NotFound();
            }

            db.Historial.Remove(historial);
            db.SaveChanges();

            return Ok(historial);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CARDSExists(decimal id)
        {
            return decard.CARDS.Count(e => e.Doc == id) > 0;
        }

        private bool HistorialExists(int id)
        {
            return db.Historial.Count(e => e.Id == id) > 0;
        }
    }
}