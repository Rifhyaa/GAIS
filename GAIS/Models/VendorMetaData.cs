using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GAIS.Models
{
    public class VendorMetaData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Nama Vendor")]
        [Required(ErrorMessage = "Nama Vendor wajib diisi")]
        [MaxLength(50, ErrorMessage = "Nama Vendor tidak boleh melebihi 100 karakter")]
        public string NamaVendor { get; set; }

        [DisplayName("Alamat Vendor")]
        [Required(ErrorMessage = "Vendor Alamat wajib diisi")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Alamat Vendor tidak boleh melebihi 255 karakter")]
        public string Alamat { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email wajib diisi")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Format Email tidak benar")]
        [MaxLength(50, ErrorMessage = "Email must be under 50 characters")]
        public string Email { get; set; }

        [DisplayName("Telepon")]
        [Required(ErrorMessage = "Telepon wajib diisi")]
        [MinLength(10, ErrorMessage = "Telepon harus memiliki minimal 10 angka")]
        [MaxLength(13, ErrorMessage = "Telepon tidak boleh melebihi 13 angka")]
        public string NoTelp { get; set; }

        [DisplayName("Nomor Rekening")]
        [Required(ErrorMessage = "Nomor Rekening wajib diisi")]
        [MinLength(10, ErrorMessage = "Nomor Rekening harus memiliki minimal 10 angka")]
        [MaxLength(25, ErrorMessage = "Nomor Rekening tidak boleh melebihi 25 angka")]
        public string NoRek { get; set; }

        [DisplayName("Bank")]
        [Required(ErrorMessage = "Bank belum dipilih")]
        public int ID_JenisBank { get; set; }

        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    }
}