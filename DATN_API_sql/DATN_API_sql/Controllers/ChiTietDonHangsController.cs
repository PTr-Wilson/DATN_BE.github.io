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
    public class ChiTietDonHangsController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/ChiTietDonHangs
        public IQueryable<ChiTietDonHang> GetChiTietDonHangs()
        {
            return db.ChiTietDonHangs;
        }

        // GET: api/ChiTietDonHangs/5
        [ResponseType(typeof(ChiTietDonHang))]
        public IHttpActionResult GetChiTietDonHang(int id)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

            return Ok(chiTietDonHang);
        }
        [Route("api/OrderDtails/{orderID}")]
        public IHttpActionResult GetMyOrders(int orderID)
        {
            using (var db = new Model1())
            {
                var data =
                    from u in db.KhachHangs
                    join o in db.DonHangs on u.KhachHangID equals o.KHACHHANG
                    join d in db.ChiTietDonHangs on o.DonHangID equals d.MAHDX
                    join p in db.SanPhams on d.MASP equals p.SanPhamID

                    where o.DonHangID == orderID
                    orderby o.NGAYDAT descending

                    select new CTDonHangModel()
                    {
                        NguoiNhan = o.TENKH,
                        HinhAnh = p.IMAGE,
                        GiaBan = p.GIABAN,
                        TenSanPham = p.TENSP,
                        NgayDat = (DateTime)o.NGAYDAT,
                        SoLuong = d.SOLUONG,
                        sodtkh=u.SODIENTHOAI,
                        DiaChi = o.DIACHI,
                        Ghichu=o.GHICHU,
                        Tongtienmua = o.TONGTIEN,
                        ThoiGian = "Giao giờ hành chính",
                    };

                return Ok(data.ToList());
            }
        }
        // PUT: api/ChiTietDonHangs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutChiTietDonHang(int id, ChiTietDonHang chiTietDonHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chiTietDonHang.CTDonHangID)
            {
                return BadRequest();
            }

            db.Entry(chiTietDonHang).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChiTietDonHangExists(id))
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

        // POST: api/ChiTietDonHangs
        [ResponseType(typeof(ChiTietDonHang))]
        public IHttpActionResult PostChiTietDonHang(ChiTietDonHang chiTietDonHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          /*  db.DonHangs.Add(donHang);
            var idSanPhams = donHang.ChiTietDonHangs.Where(e => e.MASP.HasValue).Select(e => e.MASP);
            var dsSanPham = db.SanPhams.Where(e => idSanPhams.Contains(e.SanPhamID)).ToList();
*/
            db.ChiTietDonHangs.Add(chiTietDonHang);
            var sanPham = db.SanPhams.First(e => e.SanPhamID == chiTietDonHang.MASP.Value);
            sanPham.SOLUONGTON -= chiTietDonHang.SOLUONG;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = chiTietDonHang.CTDonHangID }, chiTietDonHang);
        }

        // DELETE: api/ChiTietDonHangs/5
        [ResponseType(typeof(ChiTietDonHang))]
        public IHttpActionResult DeleteChiTietDonHang(int id)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }

           /* var sanPham = db.SanPhams.First(e => e.SanPhamID == chiTietDonHang.MASP.Value);
            sanPham.SOLUONGTON += chiTietDonHang.SOLUONG;*/

            db.ChiTietDonHangs.Remove(chiTietDonHang);
            db.SaveChanges();

            return Ok(chiTietDonHang);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChiTietDonHangExists(int id)
        {
            return db.ChiTietDonHangs.Count(e => e.CTDonHangID == id) > 0;
        }
    }
}