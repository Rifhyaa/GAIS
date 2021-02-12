using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GAIS.Models;

namespace GAIS.Controllers
{
    public class DashboardController : Controller
    {
        // Entities
        GAISEntities entities = new GAISEntities();

        // GET: Admin
        public ActionResult Admin()
        {
            // Data
            ViewBag.TotalUser = entities.Karyawans.Where(x => x.RowStatus == 0).Count();
            ViewBag.TotalVendor = entities.Vendors.Where(x => x.RowStatus == 0).Count();
            ViewBag.TotalJenisBarang = entities.JenisBarangs.Where(x => x.RowStatus == 0).Count();
            ViewBag.TotalBarangPerusahaan = entities.BarangPerusahaans.Where(x => x.RowStatus == 0).Count();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }

        public ActionResult GA()
        {
            ViewBag.StatusPeminjaman = entities.View_StatusPeminjaman.ToList();

            // Data
            ViewBag.TotalVendor = entities.Vendors.Where(x => x.RowStatus == 0).Count();
            ViewBag.TotalBarangVendor = entities.Karyawans.Where(x => x.RowStatus == 0).Count();
            ViewBag.TotalBarangPerusahaan = entities.BarangPerusahaans.Where(x => x.RowStatus == 0).Count();
            ViewBag.TotalJenisBarang = entities.JenisBarangs.Where(x => x.RowStatus == 0).Count();
            ViewBag.TotalPeminjaman = entities.Peminjamen.Count();
            ViewBag.TotalPengajuan = entities.Pengajuans.Count();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }

        public ActionResult Gudang()
        {
            ViewBag.StatusBarangMasuk = entities.View_BarangMasuk.ToList();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }

        public ActionResult Finance()
        {
            ViewBag.StatusPembayaran = entities.View_StatusPembayaran.ToList();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }

        public ActionResult Karyawan()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }

        public ActionResult KepalaDivisi()
        {
            ViewBag.StatusPengajuan = entities.View_StatusPengajuan.ToList();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }
    }
}