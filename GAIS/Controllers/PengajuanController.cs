using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GAIS.Models;
using Microsoft.Reporting.WebForms;

namespace GAIS.Controllers
{
    public class PengajuanController : Controller
    {
        // Entities
        GAISEntities entities = new GAISEntities();

        // GET: Pengajuan
        public ActionResult Form()
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.BarangVendors.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk);
 
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        [HttpGet]
        public ActionResult Submit()
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        [HttpPost]
        public ActionResult Submit(int? id)
        {
            string npk = this.Session["NPK"].ToString();

            Pengajuan mdat = new Pengajuan();
            mdat.ID_Pengajuan = DateTime.Now.ToString("yyyyMMddHHmmss") + RandomString(3);
            mdat.ID_GA = npk;
            mdat.Tgl_Pengajuan = DateTime.Now;
            mdat.TotalHarga = 0;
            mdat.StatusPengajuan = 0;
            mdat.SudahDibayar = 0;
            mdat.StatusFinal = "-";

            entities.Pengajuans.Add(mdat);

            var detail = entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk);
            foreach (var item in detail)
            {
                DetailPengajuan dat = new DetailPengajuan();
                dat.ID_Pengajuan = mdat.ID_Pengajuan;
                dat.ID_Barang = item.ID_Barang;
                dat.Kuantitas = item.Qyt;
                dat.HargaBarang = item.BarangVendor.Harga;
                dat.StatusBarang = "-";
                dat.ID_Vendor = (int)item.BarangVendor.ID_Vendor;

                var temp = entities.BarangPerusahaans.Where(x => x.ID == item.ID_Barang).First();
                dat.HargaBarang = temp.Harga;

                entities.DetailPengajuans.Add(dat);
            }

            entities.KeranjangPengajuans.RemoveRange(entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk));
            entities.SaveChanges();

            var data = entities.Peminjamen.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", data);
        }

        public ActionResult Add(int ID)
        {
            string npk = this.Session["NPK"].ToString();
            var data = entities.BarangVendors.Where(x => x.ID == ID).First();

            var check = entities.KeranjangPengajuans.Where(x => x.ID_Barang == data.ID && x.ID_Karyawan == npk).FirstOrDefault();
            if (check == null)
            {
                KeranjangPengajuan cart = new KeranjangPengajuan();
                cart.ID_Barang = data.ID;
                cart.ID_Karyawan = npk;
                cart.Qyt = 1;
                entities.KeranjangPengajuans.Add(cart);

                entities.SaveChanges();
            }
            else
            {

            }

            var item = entities.BarangVendors.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        public ActionResult Plus(int ID)
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.KeranjangPengajuans.Where(x => x.ID_Barang == ID && x.ID_Karyawan == npk).First();
            data.Qyt = data.Qyt + 1;
            entities.SaveChanges();

            var item = entities.BarangVendors.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        public ActionResult Minus(int ID)
        {
            string npk = this.Session["NPK"].ToString();

            var data = entities.KeranjangPengajuans.Where(x => x.ID_Barang == ID && x.ID_Karyawan == npk).First();

            if (data.Qyt - 1 < 1)
            {
                RemoveCart(data.ID_Barang, data.ID_Karyawan);
            }
            else
            {
                data.Qyt = data.Qyt - 1;
                entities.SaveChanges();
            }

            var item = entities.BarangVendors.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        public ActionResult Delete(int ID)
        {
            string npk = this.Session["NPK"].ToString();

            RemoveCart(ID, npk);

            var item = entities.BarangVendors.Where(x => x.RowStatus == 0);
            ViewBag.Cart = entities.KeranjangPengajuans.Where(x => x.ID_Karyawan == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View("Form", item);
        }

        public ActionResult Riwayat()
        {
            string npk = this.Session["NPK"].ToString();
            var data = entities.Pengajuans.Where(x => x.ID_GA == npk);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult Details(string ID)
        {
            string npk = this.Session["NPK"].ToString();
            var data = entities.DetailPengajuans.Where(x => x.ID_Pengajuan == ID);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult LampiranPengajuan()
        {
            var data = entities.Pengajuans.Where(x => x.StatusPengajuan == 0);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult Disetujui(string ID)
        {
            var data = entities.Pengajuans.Where(x => x.ID_Pengajuan == ID).First();
            data.StatusPengajuan = 1;
            var detail = entities.DetailPengajuans.Where(x => x.ID_Pengajuan == ID);
            foreach (var item in detail)
            {
                
            }
            entities.SaveChanges();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return RedirectToAction("LampiranPengajuan");
        }

        public ActionResult TidakDisetujui(string ID)
        {
            var data = entities.Pengajuans.Where(x => x.ID_Pengajuan == ID).First();
            data.StatusPengajuan = 2;
            entities.SaveChanges();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return RedirectToAction("LampiranPengajuan");
        }

        public ActionResult LampiranPembayaran()
        {
            var data = entities.Pengajuans.Where(x => x.StatusPengajuan == 1 && x.SudahDibayar == 0);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult Pembayaran(string ID)
        {
            var data = entities.Pengajuans.Where(x => x.ID_Pengajuan == ID).First();
            data.SudahDibayar = 1;
            data.StatusFinal = "Menunggu Vendor Mengirimkan Barang";
            entities.SaveChanges();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return RedirectToAction("LampiranPembayaran");
        }

        public ActionResult Pemesanan()
        {
            int id = Convert.ToInt32(this.Session["ID_Vendor"]);
            var data = entities.DetailPengajuans.Where(x => x.ID_Vendor == id && x.StatusBarang != "Sudah diterima gudang");
            ViewBag.Transaksi = entities.Pengajuans.Where(x => x.StatusPengajuan == 1).ToList();

            // Session Username & Role
            ViewBag.NamaVendor = this.Session["Nama_Vendor"];
            return View(data);
        }

        public ActionResult KonfirmasiPengiriman(string ID, int item)
        {
            var data = entities.Pengajuans.Where(x => x.ID_Pengajuan == ID).First();
            data.StatusFinal = "Vendor Telah Mengirimkan Barang";

            var detail = entities.DetailPengajuans.Where(x => x.ID_Pengajuan == ID && x.ID_Barang == item).First();
            detail.StatusBarang = "Barang Telah Dikirim";
            entities.SaveChanges();

            // Session Username & Role
            ViewBag.NamaVendor = this.Session["Nama_Vendor"];
            return RedirectToAction("Pemesanan");
        }

        public ActionResult BarangMasuk()
        {
            var data = entities.Pengajuans.Where(x => x.StatusPengajuan == 1 && x.SudahDibayar == 1);

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult DetailBarangMasuk(string ID)
        {
            string npk = this.Session["NPK"].ToString();
            var data = entities.DetailPengajuans.Where(x => x.ID_Pengajuan == ID && x.StatusBarang == "Barang Telah Dikirim");

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult KonfirmasiBarangMasuk(string ID, int item)
        {
            var detail = entities.DetailPengajuans.Where(x => x.ID_Pengajuan == ID && x.ID_Barang == item).First();
            detail.StatusBarang = "Barang Telah Diterima";
            entities.SaveChanges();

            // Mengecek Detail
            var totalDetail = entities.DetailPengajuans.Where(x => x.ID_Pengajuan == ID).Count();
            var telahDikonfirmasi = entities.DetailPengajuans.Where(x => x.ID_Pengajuan == ID && x.StatusBarang == "Barang Telah Diterima Bagian Gudang").Count();

            if (totalDetail == telahDikonfirmasi)
            {
                var data = entities.Pengajuans.Where(x => x.ID_Pengajuan == ID).First();
                data.StatusFinal = "Vendor Telah Mengirimkan Barang";
                entities.SaveChanges();
            }

            var fill = entities.Pengajuans.Where(x => x.StatusPengajuan == 1 && x.SudahDibayar == 1);
            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return RedirectToAction("BarangMasuk", fill);
        }

        public ActionResult LaporanPengajuan()
        {
            var data = entities.Pengajuans.ToList();

            // Session Username & Role
            ViewBag.NamaUser = this.Session["NamaUser"];
            ViewBag.Role = this.Session["Role"];
            return View(data);
        }

        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "ReportPengajuan.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("LaporanPengajuan");
            }

            var data = entities.Pengajuans.ToList();

            ReportDataSource rd = new ReportDataSource("MyDataset", data);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "   <OutputFormat>" + id + "</OutputFormat>" +
                "   <PageWidth>8.5in</PageWidth>" +
                "   <PageHeight>11in</PageHeight>" +
                "   <MarginTop>0.5in</MarginTop>" +
                "   <MarginLeft>0.3in</MarginLeft>" +
                "   <MarginRight>0.3in</MarginRight>" +
                "   <MarginBottom>0.5</MarginBottom>" +
                "   <EmbedFonts>None</EmbedFonts>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings
                );
            return File(renderedBytes, mimeType);
        }


        public void RemoveCart(int? id_barang, string npk)
        {
            var data = entities.KeranjangPengajuans.Where(x => x.ID_Barang == id_barang && x.ID_Karyawan == npk).First();

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