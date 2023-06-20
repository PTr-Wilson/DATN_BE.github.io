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
using System.Web.Mvc;
using DATN_API_sql.Models;

namespace DATN_API_sql.Controllers
{
    public class HomeController : Controller
    {

        /*        private Model1 db = new Model1();

                [Microsoft.AspNetCore.Mvc.HttpGet]
        *//*        [Route("api/KhachHangs/CountKhachHang")]
        *//*        [Microsoft.AspNetCore.Mvc.Route("api/history/{id}/{first}/{rows}")]

                [Microsoft.AspNetCore.Mvc.HttpGet("history/{id}/{first}/{rows}")]*/
        /* public IActionResult  getOrder(int id, int first, int rows)
         {

             var listOrder = db.DonHangs.Select(o => new
             {

                 o.DonHangID,
                 o.KHACHHANG,
                 o.NGAYDAT,
                 o.TONGTIEN,
                 o.GHICHU,
                 o.NGUOITAO,
                 o.TENKH,

                 KhachHang1 = db.KhachHangs.First(s => s.KhachHangID == o.KHACHHANG),

                 ChiTietDonHangs = db.ChiTietDonHangs.Where(t => t.MAHDX == o.DonHangID).ToList(),
             }).OrderByDescending(d => d.NGUOITAO).Where(e => e.KHACHHANG == id).Skip(first).Take(rows).ToList();
             *//*            return Ok(new { list = listOrder, total = listOrder.Count() });
             */
        /*            return OkResult(new { list = listOrder, total = listOrder.Count() });
        */
        /*return Ok(listOrder());*//*
        return Ok;
    }
*/
        /* private IActionResult Ok(List<object> listOrder)
         {
             throw new NotImplementedException();
         }
 */

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
