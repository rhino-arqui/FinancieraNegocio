using InsFinanciera.Models;
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
using System.Web.Http.Results;

namespace InsFinanciera
{
    public class CARDSController : ApiController
    {
        private masterEntities db = new masterEntities();

        // GET: api/CARDS
        public IQueryable<CARDS> GetCARDS()
        {
            return db.CARDS;
        }

        // GET: api/CARDS/5
        [ResponseType(typeof(CARDS))]
        public IHttpActionResult GetCARDS(decimal id)
        {
            CARDS cARDS = db.CARDS.Find(id);
            if (cARDS == null)
            {
                return NotFound();
            }

            return Ok(cARDS);
        }


        // PUT: api/CARDS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCARDS(decimal id, CARDS cARDS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cARDS.Doc)
            {
                return BadRequest();
            }

            db.Entry(cARDS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CARDSExists(id))
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
        
        // POST: api/CARDS
        [ResponseType(typeof(CARDS))]
        public IHttpActionResult PostCARDS(CARDS cARDS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CARDS.Add(cARDS);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CARDSExists(cARDS.Doc))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cARDS.Doc }, cARDS);
        }

        

        // DELETE: api/CARDS/5
        [ResponseType(typeof(CARDS))]
        public IHttpActionResult DeleteCARDS(decimal id)
        {
            CARDS cARDS = db.CARDS.Find(id);
            if (cARDS == null)
            {
                return NotFound();
            }

            db.CARDS.Remove(cARDS);
            db.SaveChanges();

            return Ok(cARDS);
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
            return db.CARDS.Count(e => e.Doc == id) > 0;
        }
    }
}