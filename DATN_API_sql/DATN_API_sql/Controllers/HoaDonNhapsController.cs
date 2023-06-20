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
    public class HoaDonNhapsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/HoaDonNhaps
        public IQueryable<HoaDonNhap> GetHoaDonNhaps()
        {
            return db.HoaDonNhaps;
        }

        // GET: api/HoaDonNhaps/5
        [ResponseType(typeof(HoaDonNhap))]
        public IHttpActionResult GetHoaDonNhap(int id)
        {
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }

            return Ok(hoaDonNhap);
        }

        // PUT: api/HoaDonNhaps/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHoaDonNhap(int id, HoaDonNhap hoaDonNhap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hoaDonNhap.HdnID)
            {
                return BadRequest();
            }

            db.Entry(hoaDonNhap).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoaDonNhapExists(id))
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

        // POST: api/HoaDonNhaps
        [ResponseType(typeof(HoaDonNhap))]
        public IHttpActionResult PostHoaDonNhap(HoaDonNhap hoaDonNhap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HoaDonNhaps.Add(hoaDonNhap);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hoaDonNhap.HdnID }, hoaDonNhap);
        }

        // DELETE: api/HoaDonNhaps/5
        [ResponseType(typeof(HoaDonNhap))]
        public IHttpActionResult DeleteHoaDonNhap(int id)
        {
            HoaDonNhap hoaDonNhap = db.HoaDonNhaps.Find(id);
            if (hoaDonNhap == null)
            {
                return NotFound();
            }

            db.HoaDonNhaps.Remove(hoaDonNhap);
            db.SaveChanges();

            return Ok(hoaDonNhap);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HoaDonNhapExists(int id)
        {
            return db.HoaDonNhaps.Count(e => e.HdnID == id) > 0;
        }
    }
}