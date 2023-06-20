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
    public class ChiTietHDNsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/ChiTietHDNs
        public IQueryable<ChiTietHDN> GetChiTietHDNs()
        {
            return db.ChiTietHDNs;
        }

        // GET: api/ChiTietHDNs/5
        [ResponseType(typeof(ChiTietHDN))]
        public IHttpActionResult GetChiTietHDN(int id)
        {
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(id);
            if (chiTietHDN == null)
            {
                return NotFound();
            }

            return Ok(chiTietHDN);
        }

        // PUT: api/ChiTietHDNs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChiTietHDN(int id, ChiTietHDN chiTietHDN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chiTietHDN.CTHdnID)
            {
                return BadRequest();
            }

            db.Entry(chiTietHDN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietHDNExists(id))
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

        // POST: api/ChiTietHDNs
        [ResponseType(typeof(ChiTietHDN))]
        public IHttpActionResult PostChiTietHDN(ChiTietHDN chiTietHDN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ChiTietHDNs.Add(chiTietHDN);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chiTietHDN.CTHdnID }, chiTietHDN);
        }

        // DELETE: api/ChiTietHDNs/5
        [ResponseType(typeof(ChiTietHDN))]
        public IHttpActionResult DeleteChiTietHDN(int id)
        {
            ChiTietHDN chiTietHDN = db.ChiTietHDNs.Find(id);
            if (chiTietHDN == null)
            {
                return NotFound();
            }

            db.ChiTietHDNs.Remove(chiTietHDN);
            db.SaveChanges();

            return Ok(chiTietHDN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChiTietHDNExists(int id)
        {
            return db.ChiTietHDNs.Count(e => e.CTHdnID == id) > 0;
        }
    }
}