using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GAIS.Models;

namespace GAIS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string npk, string password)
        {
            if (ModelState.IsValid)
            {
                using (GAISEntities entities = new GAISEntities())
                {
                    var obj = entities.Karyawans.Where(x => x.Password == password && x.NPK == npk).FirstOrDefault();
                    if (obj == null)
                    {
                        ViewBag.Validasi = "1";
                        return View();
                    }
                    else
                    {
                        this.Session["NPK"] = obj.NPK;
                        this.Session["NamaUser"] = obj.NamaKaryawan;
                        this.Session["Role"] = obj.Role.NamaRole;
                        this.Session["isLogged"] = true;

                        return RedirectToAction("Index", "JenisBank");

                        // disini di cek rolenya buat pindah2 halaman
                    }
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            this.Session["NPK"] = null;
            this.Session["NamaUser"] = null;
            this.Session["Role"] = null;
            this.Session["isLogged"] = null;

            return View("Index");
        }
    }
}