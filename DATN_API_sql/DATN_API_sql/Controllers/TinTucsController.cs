using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DATN_API_sql.Models;

namespace DATN_API_sql.Controllers
{
    public class TinTucsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/TinTucs
        public IQueryable<TinTuc> GetTinTucs()
        {
            return db.TinTucs;
        }
        [Route("api/TinTucs/getall")]

        public IQueryable<object> GetallTintucs()
        {
            return db.TinTucs.OrderByDescending(x => x.TinTucID).Join(db.LoaiTins,
                                    a => a.LoaiTinID,
                                    b => b.LoaiTinID,
                                    (a, b) => new {
                                        TinTucID = a.TinTucID,
                                        NhanVienID = a.NhanVienID,
                                        TIEUDE = a.TIEUDE,
                                        NGAYDANG = a.NGAYDANG,
                                        NOIDUNG = a.NOIDUNG,
                                        IMAGE = a.IMAGE,
                                        TRICHDAN = a.TRICHDAN,
                                        LoaiTinID = a.LoaiTinID,

                                        TENLOAITIN = b.TENLOAITIN,
                                    }); ;

        }

        [Route("api/Tintucs/LocTheoLoai1")]
        public HttpResponseMessage LocTheoLoai1(int LoaiTinID)
        {
            using (Model1 db = new Model1())
            {
                var list = db.TinTucs.Where(sp => sp.LoaiTinID == LoaiTinID).OrderByDescending(sp => sp.TinTucID).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
        }
        [Route("api/Tintucs/Tinganday")]
        public IQueryable<TinTuc> Gettinganday()
        {
            return db.TinTucs.OrderByDescending(x => x.NGAYDANG);
        }
        [HttpGet]
        [Route("api/Tintucs/GetNewBytypeName")]
        public IQueryable<TinTuc> LocTheoLoai(int typeID)
        {

            return db.TinTucs.OrderByDescending(d => d.NGAYDANG).Where(d => d.LoaiTinID == typeID);
        }

        // GET: api/TinTucs/5
        [ResponseType(typeof(TinTuc))]
        public IHttpActionResult GetTinTuc(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            return Ok(tinTuc);
        }

        // PUT: api/TinTucs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTinTuc(int id, TinTuc tinTuc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tinTuc.TinTucID)
            {
                return BadRequest();
            }

            db.Entry(tinTuc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TinTucExists(id))
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

        // POST: api/TinTucs
        [ResponseType(typeof(TinTuc))]
        public IHttpActionResult PostTinTuc(TinTuc tinTuc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TinTucs.Add(tinTuc);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tinTuc.TinTucID }, tinTuc);
        }

        // DELETE: api/TinTucs/5
        [ResponseType(typeof(TinTuc))]
        public IHttpActionResult DeleteTinTuc(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return NotFound();
            }

            db.TinTucs.Remove(tinTuc);
            db.SaveChanges();

            return Ok(tinTuc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TinTucExists(int id)
        {
            return db.TinTucs.Count(e => e.TinTucID == id) > 0;
        }
        [Route("api/Tintucs/SaveFile")]
        public string UploadFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);

                postedFile.SaveAs(physicalPath);
                return fileName;
            }
            catch (Exception)
            {
                return "empty.jpg";
            }
        }
    }
}