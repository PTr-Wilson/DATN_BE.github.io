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
    public class LoaiTinsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/LoaiTins
        public IQueryable<LoaiTin> GetLoaiTins()
        {
            return db.LoaiTins;
        }

        // GET: api/LoaiTins/5
        [ResponseType(typeof(LoaiTin))]
        public IHttpActionResult GetLoaiTin(int id)
        {
            LoaiTin loaiTin = db.LoaiTins.Find(id);
            if (loaiTin == null)
            {
                return NotFound();
            }

            return Ok(loaiTin);
        }

        // PUT: api/LoaiTins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLoaiTin(int id, LoaiTin loaiTin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loaiTin.LoaiTinID)
            {
                return BadRequest();
            }

            db.Entry(loaiTin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiTinExists(id))
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

        // POST: api/LoaiTins
        [ResponseType(typeof(LoaiTin))]
        public IHttpActionResult PostLoaiTin(LoaiTin loaiTin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoaiTins.Add(loaiTin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = loaiTin.LoaiTinID }, loaiTin);
        }

        // DELETE: api/LoaiTins/5
        [ResponseType(typeof(LoaiTin))]
        public IHttpActionResult DeleteLoaiTin(int id)
        {
            LoaiTin loaiTin = db.LoaiTins.Find(id);
            if (loaiTin == null)
            {
                return NotFound();
            }

            db.LoaiTins.Remove(loaiTin);
            db.SaveChanges();

            return Ok(loaiTin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoaiTinExists(int id)
        {
            return db.LoaiTins.Count(e => e.LoaiTinID == id) > 0;
        }
    }
}