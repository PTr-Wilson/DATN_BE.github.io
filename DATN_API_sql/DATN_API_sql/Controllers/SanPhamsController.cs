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

namespace Project5h.Controllers
{
    public class SanPhamsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/SanPhams
        // GET: api/TinTucs

       /* public IQueryable<Object> Getsanphams()
        {
            return db.sanphams.Join(db.loaisps,
                                    a => a.maloai,
                                    b => b.maloai,
                                    (a, b) => new {
                                        masp = a.masp,
                                        tensp = a.tensp,
                                        maloai = a.maloai,
                                        soluong = a.soluong,
                                        anh = a.anh,
                                        mota = a.mota,
                                        dongia = a.dongia,
                                        tenloai = b.tenloai
                                    });
        }
*/


        public IQueryable<object> GetSanPhams()
        {
            return db.SanPhams.OrderByDescending(x => x.SanPhamID).Join(db.LoaiSanPhams,
                                    a => a.MALOAI,
                                    b => b.IDLOAI,
                                    (a, b) => new {
                                        SanPhamID = a.SanPhamID,
                                        TENSP = a.TENSP,
                                        MALOAI = a.MALOAI,
                                        SOLUONG = a.SOLUONG,
                                        IMAGE = a.IMAGE,
                                        IMAGE1=a.IMAGE1,
                                        IMAGE2=a.IMAGE2,
                                        TENTG = a.TENTG,
                                        XUATXU=a.XUATXU,
                                        GIABAN=a.GIABAN,
                                        SOLUONGTON=a.SOLUONGTON,
                                        MOTA = a.MOTA,
                                        TRANGTHAI = a.TRANGTHAI,
                                        TENLOAI=b.TENLOAI,
                                    }).Where(e => e.SOLUONGTON > 0);

        }
        //danh sách sản phẩm đã bán hết 
        [Route("api/Sachs/GetsachTon")]
        public IQueryable<SanPham> Getsachton()
        {

            return db.SanPhams.Where(e => e.SOLUONGTON <= 0); ;
        }


        [Route("api/Sachs/Newest-Products")]
        public IQueryable<SanPham> GetNewest()
        {
            return db.SanPhams.OrderByDescending(x => x.SanPhamID).Take(4).Where(e => e.SOLUONGTON > 0);
        }

      
      
        [Route("api/Sachs/get1")]
        public IQueryable<SanPham> get1()
        {

            return db.SanPhams.OrderByDescending(x => x.SanPhamID);
        }
        [HttpGet]
        [Route("api/Sachs/CountSanpham")]
        public int CountSanpham()
        {
          
             var count = db.SanPhams.ToList().Count(e => e.SOLUONGTON > 0);

            /*            var count = db.SanPhams.ToList().Count();
            */
            return count;
        }

        [Route("api/Sachs/Hotest-Products")]
        public IQueryable<SanPham> GetHotest()
        {
            return db.SanPhams.OrderByDescending(x => x.SanPhamID).Where(e => e.SOLUONGTON > 0).Take(8);
        }
        [Route("api/Sachs/GetByPrice")]
        public IQueryable<SanPham> GetByPrice()
        {
            return db.SanPhams.OrderByDescending(x => x.GIABAN).Where(e => e.SOLUONGTON > 0).Take(1);
        }

        [Route("api/Sachs/GetByPriceSmall")]
        public IQueryable<SanPham> GetByPriceSmall()
        {
            return db.SanPhams.OrderBy(x => x.GIABAN).Where(e => e.SOLUONGTON > 0).Take(6);
        }
        [HttpGet]
        [Route("api/Sachs/Search/{TENSP}")]
        public HttpResponseMessage GetByName(string TENSP)
        {
            using (Model1 db = new Model1())
            {
                var rs = db.SanPhams.Where(x => x.TENSP.Contains(TENSP) || x.MOTA.Contains(TENSP)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, rs);
            }
        }
        //danh sách sản phẩm theo loại có số lượng tồn >0
        [Route("api/Sachs/LocTheoLoai")]
        public HttpResponseMessage GetSanPhamTheoLoai(int MALOAI)
        {
            /* using (Model1 db = new Model1())
             {
                 var list = db.SanPhams.Where(sp => sp.MALOAI == MALOAI, e => e.SOLUONGTON > 0).OrderByDescending(sp => sp.SanPhamID).ToList();
                 return Request.CreateResponse(HttpStatusCode.OK, list);
             }*/

            using (Model1 db = new Model1())
            {
                var list = db.SanPhams.Where(sp => sp.MALOAI == MALOAI && sp.SOLUONGTON > 0)
                                      .OrderByDescending(sp => sp.SanPhamID)
                                      .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
        }
        //danh sách sản phẩm theo loại có số lượng tồn <=0
        [Route("api/Sachs/LocTheoLoaiBanHet")]
        public HttpResponseMessage GetSanPhamTheoLoaiBanHet(int MALOAI)
        {
            using (Model1 db = new Model1())
            {
                var list = db.SanPhams.Where(sp => sp.MALOAI == MALOAI && sp.SOLUONGTON <= 0)
                                      .OrderByDescending(sp => sp.SanPhamID)
                                      .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
        }
        //lay theo id gia dươis 10000
        [Route("api/Sachs/Giathap")]
        public int GetDataProduct()
        {
            using (Model1 db = new Model1())
            {
                var count = db.SanPhams.Where(x => x.GIABAN < 50000).ToList().Count();
                return count;
               /* var 
                return _res.GetProductByCategory(id).Where(id => id.Price < 100000);*/
            }
        }

        // GET: api/SanPhams/5
        [ResponseType(typeof(SanPham))]
        public IHttpActionResult GetSanPham(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(sanPham);
        }

        // PUT: api/SanPhams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSanPham(int id, SanPham sanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanPham.SanPhamID)
            {
                return BadRequest();
            }

            db.Entry(sanPham).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
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

        // POST: api/SanPhams
        [ResponseType(typeof(SanPham))]
        public IHttpActionResult PostSanPham(SanPham sanPham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SanPhams.Add(sanPham);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sanPham.SanPhamID }, sanPham);
        }

        // DELETE: api/SanPhams/5// xóa luôn sản phẩm trong hóa đơn
        public IHttpActionResult DeleteSanPham(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            // xoa lienkeets sữa sp và chi tiet don
            foreach (var chiTietDonHang in sanPham.ChiTietDonHangs.ToList())
            {
                chiTietDonHang.MASP = null;
            }

            db.SanPhams.Remove(sanPham);
            db.SaveChanges();

            return Ok(sanPham);
        }
        /*  [ResponseType(typeof(SanPham))]
          public IHttpActionResult DeleteSanPham(int id)
          {
              SanPham sanPham = db.SanPhams.Find(id);
              if (sanPham == null)
              {
                  return NotFound();
              }

              db.SanPhams.Remove(sanPham);
              db.SaveChanges();

              return Ok(sanPham);
          }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SanPhamExists(int id)
        {
            return db.SanPhams.Count(e => e.SanPhamID == id) > 0;
        }
        [Route("api/SanPhams/savefile")]
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