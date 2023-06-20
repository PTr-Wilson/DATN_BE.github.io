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
    public class BannersController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Banners
        public IQueryable<Banner> GetBanners()
        {
            return db.Banners;
        }

        // GET: api/Banners/5
        [ResponseType(typeof(Banner))]
        public IHttpActionResult GetBanner(int id)
        {
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            return Ok(banner);
        }

        // PUT: api/Banners/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBanner(int id, Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != banner.BANNER_ID)
            {
                return BadRequest();
            }

            db.Entry(banner).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(id))
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

        // POST: api/Banners
        [ResponseType(typeof(Banner))]
        public IHttpActionResult PostBanner(Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Banners.Add(banner);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = banner.BANNER_ID }, banner);
        }

        // DELETE: api/Banners/5
        [ResponseType(typeof(Banner))]
        public IHttpActionResult DeleteBanner(int id)
        {
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            db.Banners.Remove(banner);
            db.SaveChanges();

            return Ok(banner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BannerExists(int id)
        {
            return db.Banners.Count(e => e.BANNER_ID == id) > 0;
        }
        [Route("api/Banners/savefile1")]
        public string SaveFile()
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
            catch
            {
                return "empty photo";
            }
        }
    }
}