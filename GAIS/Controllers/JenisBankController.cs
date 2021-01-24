using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using GAIS.Models;

namespace GAIS.Controllers
{
    public class JenisBankController : Controller
    {
        // Entities
        private GAISEntities entities = new GAISEntities();

        public ActionResult Index()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            var data = entities.JenisBanks.Where(x => x.RowStatus == 0);
            return View(data.ToList());
        }

        public ActionResult Details(int? ID)
        {
            if (ID == null)
            {
                // Error 400
                return RedirectToAction("BadRequest", "Error");
            }

            JenisBank data = entities.JenisBanks.Find(ID);

            if (data == null)
            {
                return RedirectToAction("Index");
            }

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            return View();
        }

        [HttpPost]
        public ActionResult Create(JenisBank mdat)
        {
            // Is Exists
            var check = entities.JenisBanks.FirstOrDefault(x => x.Nama == mdat.Nama);

            // Check Validation Form
            if (ModelState.IsValid)
            {
                if (check != null)
                {
                    ViewBag.Validasi = "Bank telah terdaftar.";
                    return View(mdat);
                }

                // Set Attributes
                mdat.CreatedTime = DateTime.Now;
                mdat.RowStatus = 0;
                mdat.CreatedBy = this.Session["NamaUser"].ToString();
                entities.JenisBanks.Add(mdat);
                entities.SaveChanges();

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return RedirectToAction("Index");
            }
            else
            {
                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return View(mdat);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? ID)
        {
            if (ID == null)
            {
                // Error 400
                return RedirectToAction("BadRequest", "Error");
            }

            // Get Data By ID
            JenisBank myData = entities.JenisBanks.Where(x => x.ID == ID).FirstOrDefault();

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

        [HttpPost]
        public ActionResult Edit(JenisBank mdat, string check_names)
        {
            // Get Data By ID
            JenisBank myData = entities.JenisBanks.Where(x => x.ID.Equals(mdat.ID)).FirstOrDefault();
            var check = entities.JenisBanks.FirstOrDefault(x => x.ID == mdat.ID);
            
            // Check Validation Form
            if (ModelState.IsValid)
            {
                if (mdat.Nama == check_names)
                {
                    ViewBag.Validasi = null;
                }
                else if (check.Nama == mdat.Nama)
                {
                    ViewBag.Validasi = "Bank telah terdaftar.";
                    ViewBag.NamaUser = this.Session["NamaUser"];
                    ViewBag.Role = this.Session["Role"];
                    return View(mdat);
                }

                // Change Attributes
                myData.Nama = mdat.Nama;
                myData.Keterangan = mdat.Keterangan;
                myData.LastModifiedTime = DateTime.Now;
                myData.ModifiedBy = this.Session["NamaUser"].ToString();
                entities.SaveChanges();

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return RedirectToAction("Index");
            }

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(mdat);
        }

        public ActionResult Delete(int ID)
        {
            // Get Data By ID
            JenisBank data = entities.JenisBanks.Find(ID);

            // Changes Status to Inactive
            data.RowStatus = 1;
            entities.SaveChanges();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return RedirectToAction("Index");
        }
    }
}