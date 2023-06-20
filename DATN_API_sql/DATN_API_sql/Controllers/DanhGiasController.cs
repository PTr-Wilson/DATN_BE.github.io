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
    public class DanhGiasController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/DanhGias/admin
        [Route("api/DanhGias/GetAllDanhGias")]
        public IQueryable<object> GetallDanhGias()
        {
            return db.DanhGias.OrderByDescending(x => x.id).Join(db.SanPhams,
                                              a => a.MASANPHAM,
                                              b => b.SanPhamID,
                                              (a, b) => new
                                              {
                                                  id = a.id,
                                                  MASANPHAM = a.MASANPHAM,
                                                  SODIEM = a.SODIEM,
                                                  BINHLUAN = a.BINHLUAN,
                                                  TENSP = b.TENSP,
                                              });

        }
        //get all cho danh gia ng dung
        public IQueryable<DanhGia> GetDanhGias()
        {
            return db.DanhGias;
        }
       

        // GET: api/DanhGias/5
        [ResponseType(typeof(DanhGia))]
        public IHttpActionResult GetDanhGia(int id)
        {
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            return Ok(danhGia);
        }

        // PUT: api/DanhGias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDanhGia(int id, DanhGia danhGia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != danhGia.id)
            {
                return BadRequest();
            }

            db.Entry(danhGia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhGiaExists(id))
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

        // POST: api/DanhGias
        [ResponseType(typeof(DanhGia))]
        public IHttpActionResult PostDanhGia(DanhGia danhGia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DanhGias.Add(danhGia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = danhGia.id }, danhGia);
        }

        // DELETE: api/DanhGias/5
        [ResponseType(typeof(DanhGia))]
        public IHttpActionResult DeleteDanhGia(int id)
        {
            DanhGia danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            db.DanhGias.Remove(danhGia);
            db.SaveChanges();

            return Ok(danhGia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DanhGiaExists(int id)
        {
            return db.DanhGias.Count(e => e.id == id) > 0;
        }
    }
}