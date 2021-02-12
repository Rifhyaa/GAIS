using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GAIS.Models;
using System.IO;
using FastMember;

namespace GAIS.Report
{
    public partial class Report_Peminjaman : System.Web.UI.Page
    {
        private GAISEntities entities = new GAISEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string id_cabang = Request.QueryString["id_cabang"].ToString();
                var data = entities.View_LaporanPeminjaman.ToList();
                DataTable table = new DataTable();
                using (var reader = ObjectReader.Create(data))
                {
                    table.Load(reader);
                }
                string path = Path.Combine(Server.MapPath("~/Report"), "ReportPeminjaman.rdlc");
                //ReportParameter[] param = new ReportParameter[1];
                //param[0] = new ReportParameter("id_cabang", "id_cabang");

                ReportDataSource rdc = new ReportDataSource("DataSet1", table);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report"), "ReportPeminjaman.rdlc");
                //ReportViewer1.LocalReport.SetParameters(param);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.Visible = true;
            }
        }

        public DataSet vw_data(string tanggal1 = null, string tanggal2 = null)
        {
            DataSet dt = new DataSet();
            /*
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TruefalseClothingConnectionString"].ToString());
            con.Open();
            SqlDataAdapter ad = new SqlDataAdapter(
               "  select CONCAT(DATEPART(YEAR, p.date_created), '-', DATEPART(month, p.date_created), '-', DATEPART(day, p.date_created)) as 'Tanggal', c.nama_cabang as 'Cabang' ,i.nama as 'NamaProduk', SUM(pd.kuantitas) as 'Kuantitas', id.harga as 'HargaSatuan' ,SUM(id.harga * pd.kuantitas) as 'Total'"
             + "  from Penjualan p"
             + "  join PenjualanDetail pd on p.id_penjualan = pd.id_penjualan"
             + "  join ItemCabang ic on pd.id_itemcabang = ic.id_itemgudang"
             + "  join ItemCabangDetail icd on pd.id_itemcabangdetail = icd.id_detailitemcabang"
             + "  join Item i on ic.id_itemgudang = i.id_item"
             + "  join ItemDetail id on icd.id_itemgudangdetail = id.id_detail"
             + "  join Cabang c on p.id_cabang = c.id_cabang"
             + "  where c.nama_cabang = '" + roll + "'" //+ "and"
                                                        //+ "  p.date_created BETWEEN @tglawal and @tglakhir"
             + "  group by i.nama,id.harga, p.date_created, c.nama_cabang", con
            );
            DataSet dt = new DataSet();
            try
            {
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
            */
            return dt;
        }
    }
}