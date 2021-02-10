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
            return View();
        }

        public ActionResult User()
        {
            ViewBag.StatusPengajuan = entities.View_StatusPengajuan.ToList();
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }
    }
}