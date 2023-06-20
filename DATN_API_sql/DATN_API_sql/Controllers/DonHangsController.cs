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
using DAL.Helper;

namespace DATN_API_sql.Controllers
{
    public class DonHangsController : ApiController
    {
        private Model1 db = new Model1();
        private IDatabaseHelper _dbHelper;

        // GET: api/DonHangs không có đơn giao thành cong
        public IQueryable<DonHang> GetDonHangs()
        {
            return db.DonHangs.Where(x => x.TRANGTHAI != 4)
                             .OrderByDescending(x => x.DonHangID);
        }


        [Route("api/DonHangs/Hoanthanh")]
        public IQueryable<DonHang> GetDonHT()
        {
            return db.DonHangs.Where(x => x.TRANGTHAI == 4)
                                         .OrderByDescending(x => x.DonHangID);
        }
        /*
                [Route("api/DonHangs/DoanhThuTheoThang")]
        *//*        public IQueryable<DoanhThu> GetDoanhThuTheoThang(int Nam)
        *//*
                public IQueryable<DoanhThu> GetDoanhThuTheoThang()
                {
                    var Nam = 2023;
                    var doanhthuhangThang = db.DonHangs.Where(x => x.NGAYDAT.Value.Year == Nam && x.TRANGTHAI == 4)
                        .GroupBy(x => x.NGAYDAT.Value.Month).Select(g => new DoanhThu
                        {
                            Thang = g.Key,
                            TongDoanhThuTheoThang = g.Sum(x => x.TONGTIEN)
                        }).OrderBy(g => g.Thang);
                    return doanhthuhangThang;
                }*/
        [Route("api/DonHangs/DoanhThuTheoThang")]
        public IHttpActionResult GetDoanhThuTheoThang()
        {
            var Nam = 2023;
            var allMonths = Enumerable.Range(1, 12);//tạo 1 chuỗi số nguyên cho 12 tháng
            // nhung thang co don
            var monthsWithOrders = db.DonHangs
                .Where(x => x.NGAYDAT.HasValue && x.NGAYDAT.Value.Year == Nam && x.TRANGTHAI == 4)//lọc đon không null và khop năm chi dinh và  có gia tri ==4
                .Select(x => x.NGAYDAT.Value.Month)
                .Distinct()
                .ToList();

            //tính toán các tháng không có đơn đặt hàng
            //trả về một chuỗi các tháng ko don
            var monthsNoOrders = allMonths.Except(monthsWithOrders);

            //Mỗi đối tượng đại diện cho một tháng 
            //thành giá trị tháng và thuộc tính(tổng doanh thu trong tháng) thành 0.
            var doanhThuList = monthsNoOrders.Select(month => new DoanhThu
            {
                Thang = month,
                TongDoanhThuTheoThang = 0
            }).ToList(); // thuc thi ơ phia nguoi dung

            // Dòng này truy xuất các tháng có đơn đặt hàng
            // và tính tổng doanh thu của chúng. 
            var doanhThuWithOrders = db.DonHangs
                .Where(x => x.NGAYDAT.HasValue && x.NGAYDAT.Value.Year == Nam && x.TRANGTHAI == 4)
                .GroupBy(x => x.NGAYDAT.Value.Month)
                .Select(g => new DoanhThu
                {
                    Thang = g.Key,
                    TongDoanhThuTheoThang = g.Sum(x => x.TONGTIEN)
                })
                .ToList(); // Execute this part on the database server
           //  kết hợp danh sách(tháng có đơn đặt hàng và doanh thu của chúng)
           // với danh sách(tháng không có đơn đặt hàng và doanh thu được đặt thành 0)
            var mergedDoanhThu = doanhThuWithOrders.Concat(doanhThuList);
           // Doog nay sắp xếp danh sách theo thuộc tính(tháng) theo thứ tự tăng dần.
          var orderedDoanhThu = mergedDoanhThu.OrderBy(g => g.Thang);

            return Ok(orderedDoanhThu);
        }


        [Route("api/DonHangs/DoanhThuTheoQuy")]
        //ique  kết quả trả về sẽ là một tập hợp có thể truy vấn tiếp
        public IQueryable<DoanhThuquy> GetDoanhThuTheoQuy()
        {
            var Nam = 2023;

            // Tạo chuỗi các quý trong năm
            var quarters = Enumerable.Range(1, 4);

            var doanhthuhangQuy = db.DonHangs.Where(x => x.NGAYDAT.Value.Year == Nam && x.TRANGTHAI == 4)
                .GroupBy(x => (x.NGAYDAT.Value.Month - 1) / 3 + 1)//-1 vì đếm tháng từ [0,11], /3 ra quý [0,1,2,3]
                //+1 để về [1,2,3,4]
                //xác định quý tương ứng với mỗi đơn hàng.
                .Select(g => new DoanhThuquy
                {
                    Quy = g.Key,
                    TongDoanhThuTheoquy = g.Sum(x => x.TONGTIEN)
                })
                .OrderBy(g => g.Quy)//tăng
                .ToList(); 
            // Thực hiện nối trái với các quý đã tạo để bao gồm các quý chẵn không có doanh thu
            var result = from mot_phan_tu in quarters//1 phaanf tuw cuar 1 quys
                         //kết hợp dữ liệu từ tập hợp dttt vs  quarters(quý)
                         join doanhThu in doanhthuhangQuy on mot_phan_tu equals doanhThu.Quy into gj
                         //DefaultIfEmpty  được gọi tren gj để cho các quý ko có doanh thu vẫn có kết quảNếu không có phần tử tương ứng trong gj, mộtDoanhThuquy mới sẽ được tạo
                         //gj (chứa các đơn hàng đã được nhóm theo quý) và tập hợp quarters (chứa các quý trong năm).
                         from subDoanhThu in gj.DefaultIfEmpty(new DoanhThuquy { Quy = mot_phan_tu, TongDoanhThuTheoquy = 0 })
                        //chọn các DoanhThuquy từ kết quả left join trước đó.
                         select subDoanhThu;

            return result.AsQueryable();
            // kết quả được chuyển đổi thành IQueryable
        }


        //tk_ 5 đơn hàng đặt gàn nhất 
        [HttpGet]
        [Route("api/DonHangs/donmoidat")]
        public IHttpActionResult GetLatestOrders()
        {
            var latestOrders = db.DonHangs
                .OrderByDescending(dh => dh.NGAYDAT)
                .Take(5)
                .Select(dh => new
                {
                    Ngaydat= dh.NGAYDAT,
                    CustomerName = dh.TENKH,
                    TotalAmount = dh.TONGTIEN
                })
                .ToList();

            return Ok(latestOrders);
        }

        // GET: api/DonHangs/5
        [ResponseType(typeof(DonHang))]
        public IHttpActionResult GetDonHang(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return NotFound();
            }

            return Ok(donHang);
        }
        //đặt đơn hang
        [Route("api/DonHangs/GetNewestOrderID")]
        public HttpResponseMessage GetNewestOrderID()
        {
            var rs = db.DonHangs.OrderByDescending(x => x.DonHangID).Select(x => x.DonHangID).Take(1);
            return Request.CreateResponse(HttpStatusCode.OK, rs);
        }
        //TK_3 khách hàng có nhiều đon nhát 
        [HttpGet]
        [Route("api/DonHangs/Topkhachhang_nhieudon")]
        public IHttpActionResult GetTopCustomers()
        {
            var customerOrders = db.KhachHangs.Select(kh => new
            {
                CustomerName = kh.TENKH,
                OrderCount = kh.DonHangs.Count(dh => dh.TRANGTHAI == 4),
                TotalAmountPerOrder = kh.DonHangs.Where(dh => dh.TRANGTHAI == 4).Sum(dh => dh.TONGTIEN)
            })
            .OrderByDescending(c => c.OrderCount)
            .Take(3)
            .ToList();

            return Ok(customerOrders);
        }

        //api khach hang va donhannng
        [HttpGet]
        [Route("api/DonHangs/KH_DH")]
        public IHttpActionResult GetTopKHDH()
        {
            var customerOrders = db.KhachHangs.Select(kh => new
            {
                CustomerName = kh.TENKH,
                OrderCount = kh.DonHangs.Count(),

                /*                OrderCount = kh.DonHangs.Count(),
                */
                TotalAmountPerOrder = kh.DonHangs.Sum(dh => dh.TONGTIEN)

            })
/*            .OrderByDescending(c => c.OrderCount)
*/          /*  .Take(3)*/
            .ToList();

            return Ok(customerOrders);
        }
        //Tk_5 sản phẩm có số lượng án nhiều nhất

        /*    [Route("api/DonHangs/TopSpbannhieu")]
            public IHttpActionResult GetTopSellingProducts()
            {
                var topProducts = db.ChiTietDonHangs
                    .Join(db.DonHangs, ctdh => ctdh.CTDonHangID, dh => dh.DonHangID, (ctdh, dh) => new { ctdh, dh })
                    .Where(join => join.dh.TRANGTHAI == 4)
                    .GroupBy(join => join.ctdh.MASP)
                    .Select(group => new
                    {
                        ProductId = group.Key,
                        soluongdamua = group.Sum(ctdh => join.ctdh.SOLUONG)
                    })
                    .OrderByDescending(p => p.soluongdamua)
                    .Take(5)
                    .Join(db.SanPhams, p => p.ProductId, sp => sp.SanPhamID, (p, sp) => new
                    {
                        ProductId = p.ProductId,
                        ProductName = sp.TENSP,
                        soluongdamua = p.soluongdamua
                    })
                    .ToList();

                return Ok(topProducts);
            }*/

        [HttpGet]
        [Route("api/DonHangs/TopSpbannhieu")]
        public IHttpActionResult GetTopSellingProducts()
        {
            var topProducts = db.ChiTietDonHangs
                .Join(db.DonHangs, ctdh => ctdh.MAHDX, dh => dh.DonHangID, (ctdh, dh) => new { ctdh, dh })
                .Where(joinResult => joinResult.dh.TRANGTHAI == 4) // Filter by TRANGTHAI == 4
                .GroupBy(joinResult => joinResult.ctdh.MASP)
                .Select(group => new
                {
                    ProductId = group.Key,
                    soluongdamua = group.Sum(ctdh => ctdh.ctdh.SOLUONG)
                })
                .OrderByDescending(p => p.soluongdamua)
                .Take(5)
                .Join(db.SanPhams, p => p.ProductId, sp => sp.SanPhamID, (p, sp) => new
                {
                    ProductId = p.ProductId,
                    ProductName = sp.TENSP,
                    soluongdamua = p.soluongdamua
                })
                .ToList();

            return Ok(topProducts);
        }






        /* [HttpGet]
         [Route("api/DonHangs/TopSpbannhieu")]
         public IHttpActionResult GetTopSellingProducts()
         {
             //iến lưu trữ kết quả
             var topProducts = db.ChiTietDonHangs//nhóm ảng chitiet theo masp

                 .GroupBy(ctdh => ctdh.MASP)
                 .Select(group => new
                 {//tạo đối tượng mói cho mỗi nhóm , trích xuất so luong sp
                     ProductId = group.Key,
                     soluongdamua = group.Sum(ctdh => ctdh.SOLUONG)
                 })
                 .OrderByDescending(p => p.soluongdamua)// lấy số lượng giảm gần theo số lươn mua
                 .Take(5)//lkay 5 cai đau
                 .Join(db.SanPhams, p => p.ProductId, sp => sp.SanPhamID, (p, sp) => new// thao tá ảng sp và chitietdon
                 {
                     ProductId = p.ProductId,
                     ProductName = sp.TENSP,
                     soluongdamua = p.soluongdamua
                 })
                 .ToList();

             return Ok(topProducts);
         }
 */

        [HttpGet]

        [Route("api/DonHangs/CountDonHang")]
        public int CountDonHang()
        {
            var count = db.DonHangs.ToList().Count();
            return count;
        }
        //tk_số đơn hàng theo trangjt hái
        [HttpGet]

        [Route("api/DonHangs/OrderStatistics")]
        public IHttpActionResult GetOrderStatistics()
        {
            var statistics = new[]
            {
        new { Status = 0, Count = db.DonHangs.Count(x => x.TRANGTHAI == 0) },
        new { Status = 1, Count = db.DonHangs.Count(x => x.TRANGTHAI == 1) },
        new { Status = 2, Count = db.DonHangs.Count(x => x.TRANGTHAI == 2) },
        new { Status = 3, Count = db.DonHangs.Count(x => x.TRANGTHAI == 3) },
        new { Status = 4, Count = db.DonHangs.Count(x => x.TRANGTHAI == 4) }
    };

            return Ok(statistics);//trả về kết quả dưới dạng một mảng các đối tượng chứa trạng thái và thông tin đếm.
        }




        // PUT: api/DonHangs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDonHang(int id, DonHang donHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != donHang.DonHangID)
            {
                return BadRequest();
            }

            db.Entry(donHang).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(id))
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

        // POST: api/DonHangs
        [ResponseType(typeof(DonHang))]
        public IHttpActionResult PostDonHang(DonHang donHang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            donHang.TRANGTHAI = 0;
            db.DonHangs.Add(donHang);
            var idSanPhams = donHang.ChiTietDonHangs.Where(e => e.MASP.HasValue).Select(e => e.MASP);
            var dsSanPham = db.SanPhams.Where(e => idSanPhams.Contains(e.SanPhamID)).ToList();
            foreach (var item in dsSanPham)
            {
                var chiTietDonHang = donHang.ChiTietDonHangs.First(e => e.MASP == item.SanPhamID);
                item.SOLUONGTON -= chiTietDonHang.SOLUONG;
                if (item.SOLUONGTON < 0)
                {
                    return BadRequest("Số lượng tồn không đủ");
                }
            }
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = donHang.DonHangID }, donHang);
        }


        /*      [Route("api/Sachs/LocTheoLoai")]
              public HttpResponseMessage GetSanPhamTheoLoai(int MALOAI)
              {
                  using (Model1 db = new Model1())
                  {
                      var list = db.SanPhams.Where(sp => sp.MALOAI == MALOAI).OrderByDescending(sp => sp.SanPhamID).ToList();
                      return Request.CreateResponse(HttpStatusCode.OK, list);
                  }
              }*/
        // POST: api/DonHangs

        [Route("UpdateStatus")]
        [HttpPut]
        public IHttpActionResult UpdateStatus(DonHang d)
        {
            DonHang donHang = db.DonHangs.Find(d.DonHangID);
            donHang.TRANGTHAI = d.TRANGTHAI;  
            db.SaveChanges();
            return Ok(donHang);
        }

        /* [Route("UpdateStatus")]
         [HttpPut]
         public IHttpActionResult UpdateStatus(DonHang d)
         {
             DonHang donHang = db.DonHangs.Find(d.DonHangID);
             if (donHang.TRANGTHAI == 0)
             {
                 donHang.TRANGTHAI = 1;
                 db.SaveChanges();

                 return Ok(donHang);
             }

             else
             {
                 donHang.TRANGTHAI = 0;
                 db.SaveChanges();

                 return Ok(donHang);
             }

         }
 */
        // DELETE: api/DonHangs/5
        // DELETE: api/DonHangs/5
        [ResponseType(typeof(DonHang))]
        public IHttpActionResult DeleteDonHang(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return NotFound();
            }

            if (donHang.TRANGTHAI == 2 || donHang.TRANGTHAI == 4)
            {
                return BadRequest("Trạng thái đang giao hàng không thể xóa");
            }
            else if (donHang.TRANGTHAI == 4)
            {
                return BadRequest("Trạng thái Giao hàng thành công không thể xóa");
            }

            var chiTietDonHangs = db.ChiTietDonHangs.Where(c => c.MAHDX == id).ToList();

            // Xóa các bản ghi ChiTietDonHang được liên kết
            var idSanPhams = donHang.ChiTietDonHangs.Where(e => e.MASP.HasValue).Select(e => e.MASP);
            var dsSanPham = db.SanPhams.Where(e => idSanPhams.Contains(e.SanPhamID)).ToList();
            foreach (var item in dsSanPham)
            {
                var chiTietDonHang = donHang.ChiTietDonHangs.First(e => e.MASP == item.SanPhamID);
                item.SOLUONGTON += chiTietDonHang.SOLUONG;
                if (item.SOLUONGTON < 0)
                {
                    return BadRequest("Số lượng tồn đã tồn tại");
                }
            }
            db.ChiTietDonHangs.RemoveRange(chiTietDonHangs);

            db.DonHangs.Remove(donHang);
            db.SaveChanges();

            return Ok(donHang);
        }



        /*  [ResponseType(typeof(DonHang))]
          public IHttpActionResult DeleteDonHang(int id)
          {
              DonHang donHang = db.DonHangs.Find(id);
              if (donHang == null)
              {
                  return NotFound();
              }

              db.DonHangs.Remove(donHang);
              db.SaveChanges();

              return Ok(donHang);
          }
  */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DonHangExists(int id)
        {
            return db.DonHangs.Count(e => e.DonHangID == id) > 0;
        }
        #region Export to excel
        private string GenerateOrfer(int orderID)
        {
            /*      var folderReport=configHelper.Get b
            */
            return string.Empty;
        }
        #endregion
    }
}