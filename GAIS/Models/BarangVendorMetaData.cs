using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GAIS.Models
{
    public class BarangVendorMetaData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Nama Barang")]
        [Required(ErrorMessage = "Nama Barang wajib diisi")]
        [MaxLength(100, ErrorMessage = "Nama Barang tidak boleh melebihi 100 karakter")]
        public string NamaBarang { get; set; }

        [DisplayName("Deskripsi")]
        [Required(ErrorMessage = "Deskripsi wajib diisi")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Deskripsi tidak boleh melebihi 255 karakter")]
        public string Keterangan { get; set; }

        [DisplayName("Jenis Barang")]
        [Required(ErrorMessage = "Jenis Barang belum dipilih")]
        public int ID_JenisBarang { get; set; }

        [DisplayName("Vendor")]
        [Required(ErrorMessage = "Vendor belum dipilih")]
        public int ID_Vendor { get; set; }

        [DisplayName("Harga")]
        [Required(ErrorMessage = "Harga wajib diisi")]
        public int Harga { get; set; }

        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    }
}