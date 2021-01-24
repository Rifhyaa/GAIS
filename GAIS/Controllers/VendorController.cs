using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GAIS.Models;

namespace GAIS.Controllers
{
    public class VendorController : Controller
    {
        // Entities
        GAISEntities entities = new GAISEntities();

        public ActionResult Index()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            var lists = entities.Vendors.Where(x => x.RowStatus == 0).ToList();
            return View(lists.ToList());
        }

        public ActionResult Details(int ID)
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            Vendor lists = entities.Vendors.Where(x => x.ID == ID).FirstOrDefault();
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

            ViewBag.ID_JenisBank = new SelectList(entities.JenisBanks.Where(x => x.RowStatus == 0), "ID", "Nama");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Vendor mdat)
        {
            

            if (ModelState.IsValid)
            {
                // Set Data
                mdat.CreatedTime = DateTime.Now;
                mdat.RowStatus = 0;
                mdat.CreatedBy = this.Session["NamaUser"].ToString();
                entities.Vendors.Add(mdat);
                entities.SaveChanges();

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ID_JenisBank = new SelectList(entities.JenisBanks.Where(x => x.RowStatus == 0), "ID", "Nama", mdat.ID_JenisBank);

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return View(mdat);
            }
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            // Get Data By ID
            Vendor myData = entities.Vendors.Where(x => x.ID.Equals(ID)).FirstOrDefault();
            
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            ViewBag.ID_JenisBank = new SelectList(entities.JenisBanks.Where(x => x.RowStatus == 0), "ID", "Nama", myData.ID_JenisBank);
            return View(myData);
        }

        [HttpPost]
        public ActionResult Edit(Vendor mdat)
        {
            // Get Data By ID
            Vendor myData = entities.Vendors.Where(x => x.ID.Equals(mdat.ID)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                // Change Data
                myData.NamaVendor = mdat.NamaVendor;
                myData.Alamat = mdat.Alamat;
                myData.Email = mdat.Email;
                myData.NoTelp = mdat.NoTelp;
                myData.NoRek = mdat.NoRek;
                myData.ID_JenisBank = mdat.ID_JenisBank;
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
                ViewBag.ID_JenisBank = new SelectList(entities.JenisBanks.Where(x => x.RowStatus == 0), "ID", "Nama", mdat.ID_JenisBank);
                return View(mdat);
            }

        }

        public ActionResult Delete(int ID)
        {
            // Get Data By ID
            Vendor data = entities.Vendors.Where(x => x.ID == ID).FirstOrDefault();

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