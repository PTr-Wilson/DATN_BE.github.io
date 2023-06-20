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
    public class NhanViensController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/NhanViens
        public IQueryable<NhanVien> GetNhanViens()
        {
            return db.NhanViens;
        }

        // GET: api/NhanViens/5
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult GetNhanVien(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return Ok(nhanVien);
        }

        // PUT: api/NhanViens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNhanVien(int id, NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhanVien.NhanVienID)
            {
                return BadRequest();
            }

            db.Entry(nhanVien).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(id))
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

        // POST: api/NhanViens
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult PostNhanVien(NhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NhanViens.Add(nhanVien);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nhanVien.NhanVienID }, nhanVien);
        }

        // DELETE: api/NhanViens/5
        [ResponseType(typeof(NhanVien))]
        public IHttpActionResult DeleteNhanVien(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();

            return Ok(nhanVien);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NhanVienExists(int id)
        {
            return db.NhanViens.Count(e => e.NhanVienID == id) > 0;
        }
    }
}