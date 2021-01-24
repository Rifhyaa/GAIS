using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using GAIS.Models;

namespace GAIS.Controllers
{
    public class BarangPerusahaanController : Controller
    {
        // Entities
        GAISEntities entities = new GAISEntities();

        public ActionResult Index()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            var lists = entities.BarangPerusahaans.Where(x => x.RowStatus == 0);
            return View(lists.ToList());
        }

        public ActionResult Details(int ID)
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            BarangPerusahaan lists = entities.BarangPerusahaans.Where(x => x.ID == ID).FirstOrDefault();
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

            ViewBag.ID_JenisBarang = new SelectList(entities.JenisBarangs.Where(x => x.RowStatus == 0), "ID", "Nama");
            ViewBag.ID_Vendor = new SelectList(entities.Vendors.Where(x => x.RowStatus == 0), "ID", "NamaVendor");
            ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection");
            return View();
        }


        [HttpPost]
        public ActionResult Create(BarangPerusahaan mdat)
        {
            if (ModelState.IsValid)
            {
                // Set Data
                mdat.CreatedTime = DateTime.Now;
                mdat.RowStatus = 0;
                mdat.CreatedBy = this.Session["NamaUser"].ToString();
                entities.BarangPerusahaans.Add(mdat);
                entities.SaveChanges();

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ID_JenisBarang = new SelectList(entities.JenisBarangs.Where(x => x.RowStatus == 0), "ID", "Nama", mdat.ID_JenisBarang);
                ViewBag.ID_Vendor = new SelectList(entities.Vendors.Where(x => x.RowStatus == 0), "ID", "NamaVendor", mdat.ID_Vendor);
                ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection", mdat.ID_Seksi);

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
            BarangPerusahaan myData = entities.BarangPerusahaans.Where(x => x.ID.Equals(ID)).FirstOrDefault();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            ViewBag.ID_JenisBarang = new SelectList(entities.JenisBarangs.Where(x => x.RowStatus == 0), "ID", "Nama", myData.ID_JenisBarang);
            ViewBag.ID_Vendor = new SelectList(entities.Vendors.Where(x => x.RowStatus == 0), "ID", "NamaVendor", myData.ID_Vendor);
            ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection", myData.ID_Seksi);
            return View(myData);
        }

        [HttpPost]
        public ActionResult Edit(BarangPerusahaan mdat)
        {
            // Get Data By ID
            BarangPerusahaan myData = entities.BarangPerusahaans.Where(x => x.ID.Equals(mdat.ID)).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                // Change Attributes
                myData.NamaBarang = mdat.NamaBarang;
                myData.Keterangan = mdat.Keterangan;
                myData.ID_JenisBarang = mdat.ID_JenisBarang;
                myData.ID_Seksi = mdat.ID_Seksi;
                myData.ID_Vendor = mdat.ID_Vendor;
                myData.Stok = mdat.Stok;
                myData.Harga = mdat.Harga;
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
                ViewBag.ID_JenisBarang = new SelectList(entities.JenisBarangs.Where(x => x.RowStatus == 0), "ID", "Nama", mdat.ID_JenisBarang);
                ViewBag.ID_Vendor = new SelectList(entities.Vendors.Where(x => x.RowStatus == 0), "ID", "NamaVendor", mdat.ID_Vendor);
                ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection", mdat.ID_Seksi);

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return View(mdat);
            }
        }

        public ActionResult Delete(int ID)
        {
            // Get Data By ID
            BarangPerusahaan data = entities.BarangPerusahaans.Where(x => x.ID == ID).FirstOrDefault();

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