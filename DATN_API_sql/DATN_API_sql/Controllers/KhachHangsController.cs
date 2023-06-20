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
    public class KhachHangsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/KhachHangs
        public IQueryable<KhachHang> GetKhachHangs()
        {
            return db.KhachHangs;
        }

        // GET: api/KhachHangs/5
        [ResponseType(typeof(KhachHang))]
        public IHttpActionResult GetKhachHang(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return Ok(khachHang);
        }
        [HttpGet]
        [Route("api/KhachHangs/CountKhachHang")]
        public int CountKhachHang()
        {
            var count = db.KhachHangs.ToList().Count();
            return count;
        }
        //đánh giá sp đã mua
        [HttpGet]
        [Route("api/KhachHangs/myOrders/{id}")]
        public IHttpActionResult GetMyOrders(int id)
        {

            var orders = from kh in db.KhachHangs
                         join dh in db.DonHangs on kh.KhachHangID equals dh.KHACHHANG
                         join ctdh in db.ChiTietDonHangs on dh.DonHangID equals ctdh.MAHDX
                         join p in db.SanPhams on ctdh.MASP equals p.SanPhamID

                         where kh.KhachHangID == id && dh.TRANGTHAI == 4
                         select new CTDonHangModel()
                         {
                             NguoiNhan = dh.TENKH,
                             GiaBan = ctdh.GIABAN,
                             TenSanPham = p.TENSP,
                             IdSanPham=p.SanPhamID,
                             Trangthaimua=dh.TRANGTHAI,
                             HinhAnh = p.IMAGE,
                             NgayDat = (DateTime)dh.NGAYDAT,
                             SoLuong = ctdh.SOLUONG,
                             /*HinhAnh = .IMAGE,
                             GiaBan = p.GIABAN,
                             TenSanPham = p.TENSP,
                             NgayDat = (DateTime)o.NGAYDAT,*/
                             //hinhf anhr ngyaf vs teen 
                             //image
                             ThoiGian = "Giao giờ hành chính",
                         };
            ;
            return Ok(orders);


        }
        // danh sach đon hang
        [Route("api/KhachHangs/LocTheoKhachhang/{makh}")]
        public IHttpActionResult Getdhtheokhachhang(int makh)
        {
            using (Model1 db = new Model1())
            {
                var data =
                    from u in db.DonHangs
                    join o in db.KhachHangs on u.KHACHHANG equals o.KhachHangID

                    where u.KHACHHANG == makh
                    orderby u.NGAYDAT descending

                    select new CTDonHangModel()
                    {
                        NguoiNhan = o.TENKH,
                        Ghichu = u.GHICHU,

                        NgayDat = (DateTime)u.NGAYDAT,
                        DiaChi = o.DIACHI,
                        ThoiGian = "Giao giờ hành chính",
                        Tongtienmua = u.TONGTIEN,
                        Trangthaimua = u.TRANGTHAI,
                    };

                return Ok(data.ToList());
            }

        }

        // PUT: api/KhachHangs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKhachHang(int id, KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != khachHang.KhachHangID)
            {
                return BadRequest();
            }

            db.Entry(khachHang).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhachHangExists(id))
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

        // POST: api/KhachHangs
        // POST: api/KhachHangs
        [ResponseType(typeof(KhachHang))]
        public IHttpActionResult PostKhachHang(KhachHang khachHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KhachHangs.Add(khachHang);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = khachHang.KhachHangID }, khachHang);
        }

        // DELETE: api/KhachHangs/5
        /*     [ResponseType(typeof(KhachHang))]
             public IHttpActionResult DeleteKhachHang(int id)
             {
                 KhachHang khachHang = db.KhachHangs.Find(id);
                 if (khachHang == null)
                 {
                     return NotFound();
                 }

                 db.KhachHangs.Remove(khachHang);
                 db.SaveChanges();

                 return Ok(khachHang);
             }*/
        [ResponseType(typeof(KhachHang))]
        public IHttpActionResult DeleteKhachHang(int id)
        {
            KhachHang khachHang = db.KhachHangs.Include(kh => kh.DonHangs.Select(dh => dh.ChiTietDonHangs))
                                               .SingleOrDefault(kh => kh.KhachHangID == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            // Remove associated order details
          /*  foreach (var donHang in khachHang.DonHangs.ToList())
            {
                foreach (var chiTietDonHang in donHang.ChiTietDonHangs.ToList())
                {
                    db.ChiTietDonHangs.Remove(chiTietDonHang);
                }
            }

            // Remove associated orders
            foreach (var donHang in khachHang.DonHangs.ToList())
            {
                db.DonHangs.Remove(donHang);
            }
*/
            foreach (var donHang in khachHang.DonHangs.ToList())
            {
                foreach (var chiTietDonHang in donHang.ChiTietDonHangs.ToList())
                {

                    var sanPham = db.SanPhams.FirstOrDefault(sp => sp.SanPhamID == chiTietDonHang.MASP);
                    if (sanPham != null)
                    {
                        sanPham.SOLUONGTON += chiTietDonHang.SOLUONG;
                    }
                    db.ChiTietDonHangs.Remove(chiTietDonHang);

                }
                db.DonHangs.Remove(donHang);
            }
            // Remove the customer
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();

            return Ok(khachHang);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KhachHangExists(int id)
        {
            return db.KhachHangs.Count(e => e.KhachHangID == id) > 0;
        }
    }
}