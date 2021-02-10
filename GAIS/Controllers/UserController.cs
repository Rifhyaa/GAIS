using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using GAIS.Models;

namespace GAIS.Controllers
{
    public class UserController : Controller
    {
        // Entities
        private GAISEntities entities = new GAISEntities();

        // GET: User
        public ActionResult MyProfile()
        {
            // Get Data By ID
            string ID = this.Session["NPK"].ToString();
            Karyawan myData = entities.Karyawans.Where(x => x.NPK == ID).FirstOrDefault();

            if (myData == null)
            {
                // Error 404
                return RedirectToAction("NotFound", "Error");
            }

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(myData);
        }

        public ActionResult Security()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View();
        }

        public ActionResult EditProfile()
        {
            // Get Data By ID
            string ID = this.Session["NPK"].ToString();
            Karyawan myData = entities.Karyawans.Where(x => x.NPK == ID).FirstOrDefault();

            if (myData == null)
            {
                // Error 404
                return RedirectToAction("NotFound", "Error");
            }

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(myData);
        }
    }
}