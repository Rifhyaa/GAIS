﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GAIS.Models;

namespace GAIS.Controllers
{
    public class PeminjamanController : Controller
    {
        // Entities
        GAISEntities entities = new GAISEntities();

        public ActionResult Index()
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.Peminjamen.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult Details(string ID)
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.DetailPeminjamen.Where(x => x.ID_Peminjaman == ID);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult Form()
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.BarangPerusahaans.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.Keranjangs.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult Add(int ID)
        {
            string npk = this.Session["NPK"].ToString();
            var data = entities.BarangPerusahaans.Where(x => x.ID == ID).First();

            var check = entities.Keranjangs.Where(x => x.ID_Barang == data.ID && x.ID_Karyawan == npk).FirstOrDefault();
            if (check == null)
            {
                Keranjang cart = new Keranjang();
                cart.ID_Barang = data.ID;
                cart.ID_Karyawan = npk; 
                cart.Qyt = 1;
                entities.Keranjangs.Add(cart);

                entities.SaveChanges();
            }
            else
            {
                
            }

            var item = entities.BarangPerusahaans.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.Keranjangs.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        public ActionResult Plus(int ID)
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.Keranjangs.Where(x => x.ID_Barang == ID && x.ID_Karyawan == npk).First();
            data.Qyt = data.Qyt + 1;
            entities.SaveChanges();

            var item = entities.BarangPerusahaans.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.Keranjangs.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        public ActionResult Minus(int ID)
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.Keranjangs.Where(x => x.ID_Barang == ID && x.ID_Karyawan == npk).First();

            if (data.Qyt - 1 < 1)
            {
                RemoveCart(data.ID_Barang, data.ID_Karyawan);
            }
            else
            {
                data.Qyt = data.Qyt - 1;
                entities.SaveChanges();
            }

            var item = entities.BarangPerusahaans.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.Keranjangs.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        public ActionResult Delete(int ID)
        {
            string npk = this.Session["NPK"].ToString();

            RemoveCart(ID, npk);
            
            var item = entities.BarangPerusahaans.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.Keranjangs.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        [HttpGet]
        public ActionResult Submit()
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.Keranjangs.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        [HttpPost]
        public ActionResult Submit(DateTime borrow, DateTime returned)
        {
            string npk = this.Session["NPK"].ToString();

            Peminjaman mdat = new Peminjaman();
            mdat.ID = DateTime.Now.ToString("yyyyMMddHHmmss") + RandomString(3);
            mdat.ID_Karyawan = npk;
            mdat.TglPeminjaman = borrow;
            mdat.TglPengembalian = returned;
            mdat.AcceptedBy = "-";
            mdat.Status = 0;
            
            entities.Peminjamen.Add(mdat);

            var detail = entities.Keranjangs.Where(x => x.ID_Karyawan == npk);
            foreach (var item in detail)
            {
                DetailPeminjaman dat = new DetailPeminjaman();
                dat.ID_Peminjaman = mdat.ID;
                dat.ID_Barang = item.ID_Barang;
                dat.Kuantitas = item.Qyt;

                var temp = entities.BarangPerusahaans.Where(x => x.ID == item.ID_Barang).First();
                dat.HargaBarang = temp.Harga;

                entities.DetailPeminjamen.Add(dat);
            }

            entities.Keranjangs.RemoveRange(entities.Keranjangs.Where(x => x.ID_Karyawan == npk));
            entities.SaveChanges();

            var data = entities.Peminjamen.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Index", data);
        }

        public ActionResult ViewAll()
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.Peminjamen;

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult ListPeminjaman()
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.Peminjamen.Where(x => x.Status == 0 || x.Status == 1);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult ConfirmationBorrowing(string id)
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.Peminjamen.Where(x => x.ID == id).FirstOrDefault();
            data.AcceptedBy = this.Session["NamaUser"].ToString();
            data.Status = 1;
            data.LastModifiedTime = DateTime.Now;
            entities.SaveChanges();

            var lists = entities.Peminjamen.Where(x => x.Status == 0 || x.Status == 1);
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("ListPeminjaman", lists);
        }

        public ActionResult ConfirmationReturning(string id)
        {
            string npk = this.Session["NPK"].ToString();

            // Changes Data
            var data = entities.Peminjamen.Where(x => x.ID == id).FirstOrDefault();
            data.ModifiedBy = this.Session["NamaUser"].ToString();
            data.Status = 2;
            data.LastModifiedTime = DateTime.Now;

            //TimeSpan ts = DateTime.Now.Subtract(data.TglPengembalian.Value);
            //int NumberOfDays = Convert.ToInt32(ts.TotalDays);
        
            // Check Denda
            if (data.TglPengembalian < DateTime.Today)
            {
                data.IsLate = 1;
                data.Denda = 50000;
            }
            entities.SaveChanges();
            
            var lists = entities.Peminjamen.Where(x => x.Status == 0 || x.Status == 1);
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("ListPeminjaman", lists);
        }

        public void RemoveCart(int? id_barang, string npk)
        {
            var data = entities.Keranjangs.Where(x => x.ID_Barang == id_barang && x.ID_Karyawan == npk).First();

            entities.Entry(data).State = EntityState.Deleted;

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            entities.SaveChanges();
        }

        public string RandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}