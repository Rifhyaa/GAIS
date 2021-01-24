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
    public class KaryawanController : Controller
    {
        // Entities
        private GAISEntities entities = new GAISEntities();

        public ActionResult Index()
        {
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];

            var data = entities.Karyawans.Where(x => x.RowStatus == 0);
            return View(data.ToList());
        }

        public ActionResult Details(string ID)
        {
            if (ID == null)
            {
                // Error 400
                return RedirectToAction("BadRequest", "Error");
            }

            Karyawan data = entities.Karyawans.Find(ID);
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

            ViewBag.ID_Role = new SelectList(entities.Roles.Where(x => x.RowStatus == 0), "ID", "NamaRole");
            ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection");
            ViewBag.JenisKelamin = new SelectList(entities.References.Where(x => x.UsedFor == "Gender"), "Value", "Deskripsi");
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase File, Karyawan mdat)
        {
            // Check Validation Form
            if (ModelState.IsValid && File != null)
            {
                ViewBag.PesanValidasi = "";

                mdat.NPK = GenerateNPK(mdat.ID_Seksi);

                var ext = Path.GetExtension(File.FileName);
                var inputFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + mdat.NPK + ext;
                var serverPath = Path.Combine(Server.MapPath("~/App_Data/Upload/") + inputFileName);

                // Save File to Server Folder
                File.SaveAs(serverPath);

                // Set Attributes
                mdat.Foto = inputFileName;
                mdat.CreatedTime = DateTime.Now;
                mdat.RowStatus = 0;
                mdat.CreatedBy = this.Session["NamaUser"].ToString();
                mdat.Password = RandomString(10);
                entities.Karyawans.Add(mdat);
                entities.SaveChanges();

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("gais.staff@gmail.com");
                    mail.To.Add(mdat.Email);
                    mail.Subject = "Akun Perusahaan";
                    mail.Body = "<h2>Hello, " + mdat.NamaKaryawan +
                        "</h2>Berkaitan dengan website Sistem Informasi General Affair, Berikut Terlampir detail informasi akun anda<br>"
                        + "Username : <b>" + mdat.NPK + "</b><br>Password   : <b>" + mdat.Password +
                        "</b><br>Sekian info yang dapat kami sampaikan atas perhatiannya kami ucapkan terimakasih." +
                        "<br>Sekretaris";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("gais.staff@gmail.com", "Testing1322");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PesanValidasi = "Profile Picture is not selected";
                ViewBag.ID_Role = new SelectList(entities.Roles.Where(x => x.RowStatus == 0), "ID", "NamaRole", mdat.ID_Role);
                ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection", mdat.ID_Seksi);
                ViewBag.JenisKelamin = new SelectList(entities.References.Where(x => x.UsedFor == "Gender"), "Value", "Deskripsi", mdat.JenisKelamin);

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return View(mdat);
            }
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            if (ID == null)
            {
                // Error 400
                return RedirectToAction("BadRequest", "Error");
            }

            // Get Data By ID
            Karyawan myData = entities.Karyawans.Where(x => x.NPK == ID).FirstOrDefault();

            if (myData == null)
            {
                // Error 404
                return RedirectToAction("NotFound", "Error");
            }

            ViewBag.ID_Role = new SelectList(entities.Roles.Where(x => x.RowStatus == 0), "ID", "NamaRole", myData.ID_Role);
            ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection", myData.ID_Seksi);
            ViewBag.JenisKelamin = new SelectList(entities.References.Where(x => x.UsedFor == "Gender"), "Value", "Deskripsi", myData.JenisKelamin);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(myData);
        }

        [HttpPost]
        public ActionResult Edit(Karyawan mdat, string check_names)
        {
            // Get Data By ID
            Karyawan myData = entities.Karyawans.Where(x => x.NPK.Equals(mdat.NPK)).FirstOrDefault();

            // Check Validation Form
            if (ModelState.IsValid)
            {
                // Change Attributes
                myData.NamaKaryawan = mdat.NamaKaryawan;
                myData.JenisKelamin = mdat.JenisKelamin;
                myData.TanggalLahir = mdat.TanggalLahir;
                myData.Alamat = mdat.Alamat;
                myData.Email = mdat.Email;
                myData.NoTelp = mdat.NoTelp;
                myData.ID_Role = mdat.ID_Role;
                myData.ID_Seksi = mdat.ID_Seksi;
                myData.ModifiedBy = this.Session["NamaUser"].ToString();
                myData.LastModifiedTime = DateTime.Now;
                entities.SaveChanges();

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.ID_Role = new SelectList(entities.Roles.Where(x => x.RowStatus == 0), "ID", "NamaRole", mdat.ID_Role);
                ViewBag.ID_Seksi = new SelectList(entities.Sections.Where(x => x.RowStatus == 0), "ID", "NamaSection", mdat.ID_Seksi);
                ViewBag.JenisKelamin = new SelectList(entities.References.Where(x => x.UsedFor == "Gender"), "Value", "Deskripsi", myData.JenisKelamin);

                // Session Username & Role
                ViewBag.NamaUser = this.Session["NamaUser"];
                ViewBag.Role = this.Session["Role"];
                return View(mdat);
            }
        }

        public ActionResult Delete(string ID)
        {
            // Get Data By ID
            Karyawan data = entities.Karyawans.Where(x => x.NPK == ID).FirstOrDefault();

            // Changes Status to Inactive
            data.RowStatus = 1;
            entities.SaveChanges();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return RedirectToAction("Index");
        }

        public string GenerateNPK(int? section)
        {
            /* NPK 10 Digit
             * 2 digit pertama menunjukkan section
             * 4 digit selanjutnya menunjukkan kapan bergabung
             * 4 digit terakhir menunjukkan urutan
            */

            int year = DateTime.Now.Year;
            int row = entities.Karyawans.Where(x => x.CreatedTime.Value.Year == year).ToList().Count() + 1;

            int validation = 4 - row.ToString().Length;
            string result = "0" + section.ToString() + year.ToString();

            for (int i=0; i < validation; i++)
            {
                result += "0";
            }

            return result + row.ToString();
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