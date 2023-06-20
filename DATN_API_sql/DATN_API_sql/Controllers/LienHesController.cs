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
using DATN_API_sql.Models;

namespace DATN_API_sql.Controllers
{
    public class LienHesController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/LienHes
        public IQueryable<LienHe> GetLienHes()
        {
            return db.LienHes;
        }

        // GET: api/LienHes/5
        [ResponseType(typeof(LienHe))]
        public IHttpActionResult GetLienHe(int id)
        {
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return NotFound();
            }

            return Ok(lienHe);
        }

        // PUT: api/LienHes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLienHe(int id, LienHe lienHe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lienHe.id)
            {
                return BadRequest();
            }

            db.Entry(lienHe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LienHeExists(id))
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

        // POST: api/LienHes
        [ResponseType(typeof(LienHe))]
        public IHttpActionResult PostLienHe(LienHe lienHe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LienHes.Add(lienHe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lienHe.id }, lienHe);
        }

        // DELETE: api/LienHes/5
        [ResponseType(typeof(LienHe))]
        public IHttpActionResult DeleteLienHe(int id)
        {
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return NotFound();
            }

            db.LienHes.Remove(lienHe);
            db.SaveChanges();

            return Ok(lienHe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LienHeExists(int id)
        {
            return db.LienHes.Count(e => e.id == id) > 0;
        }
    }
}