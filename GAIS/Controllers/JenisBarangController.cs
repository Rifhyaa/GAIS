using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GAIS.Models;
using GAIS;

namespace GAIS.Controllers
{
    public class JenisBarangController : Controller
    {
        // Entities
        GAISEntities entities = new GAISEntities();

        public ActionResult Index()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            var lists = entities.JenisBarangs.Where(x => x.RowStatus == 0).ToList();
            return View(lists);
        }

        public ActionResult Details(int ID)
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            JenisBarang lists = entities.JenisBarangs.Where(x => x.ID == ID).FirstOrDefault();
            if (lists == null)
            {
                return RedirectToAction("Index");
            }
            return View(lists);
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
        public ActionResult Create(JenisBarang mdat)
        {
            if (ModelState.IsValid)
            {
                // Change Attributes
                mdat.CreatedTime = DateTime.Now;
                mdat.RowStatus = 0;
                mdat.CreatedBy = this.Session["NamaUser"].ToString();
                entities.JenisBarangs.Add(mdat);
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
                return View("Create");
            }
        }

        
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            // Get Data By ID
            JenisBarang myData = entities.JenisBarangs.Where(x => x.ID.Equals(ID)).FirstOrDefault();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(myData);
        }

        [HttpPost]
        public ActionResult Edit(JenisBarang mdat)
        {
            // Get Data By ID
            JenisBarang myData = entities.JenisBarangs.Where(x => x.ID.Equals(mdat.ID)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                // Change Data
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
            else
            {
                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return View("Edit");
            }
        }

        public ActionResult Delete(int ID)
        {
            // Get Data By ID
            JenisBarang data = entities.JenisBarangs.Where(x => x.ID == ID).FirstOrDefault();

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