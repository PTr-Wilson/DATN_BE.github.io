using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DATN_API_sql.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietHDN> ChiTietHDNs { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public virtual DbSet<LoaiTin> LoaiTins { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonHang)
                .HasForeignKey(e => e.MAHDX)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDonNhap>()
                .HasMany(e => e.ChiTietHDNs)
                .WithRequired(e => e.HoaDonNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonHangs)
                .WithOptional(e => e.KhachHang1)
                .HasForeignKey(e => e.KHACHHANG);

            modelBuilder.Entity<LoaiSanPham>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.LoaiSanPham)
                .HasForeignKey(e => e.MALOAI);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.HoaDonNhaps)
                .WithOptional(e => e.NhaCungCap)
                .HasForeignKey(e => e.MANCC);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.DonHangs)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.NGUOITAO);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDonNhaps)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.NGUOITAO);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.MASP);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHDNs)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.MASP);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.DanhGias)
                .WithOptional(e => e.SanPham)
                .HasForeignKey(e => e.MASANPHAM);
        }
    }
}
