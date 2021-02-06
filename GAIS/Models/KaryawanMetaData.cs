using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GAIS.Models
{
    public class KaryawanMetaData
    {
        [Key]
        public string NPK { get; set; }

        [DisplayName("Nama Karyawan")]
        [Required(ErrorMessage = "Nama Karyawan wajib diisi")]
        [MaxLength(100, ErrorMessage = "Nama Karyawan tidak boleh melebihi 100 karakter")]
        public string NamaKaryawan { get; set; }

        [DisplayName("Jenis Kelamin")]
        [Required(ErrorMessage = "Jenis Kelamin belum dipilih")]
        public string JenisKelamin { get; set; }

        [DisplayName("Tanggal Lahir")]
        [Required(ErrorMessage = "Tanggal Lahir wajib diisi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TanggalLahir { get; set; }

        [DisplayName("Alamat Karyawan")]
        [Required(ErrorMessage = "Alamat Karyawan wajib diisi")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Alamat Karyawan tidak boleh melebihi 255 karakter")]
        public string Alamat { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email wajib diisi")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Format Email tidak benar")]
        [MaxLength(50, ErrorMessage = "Email tidak boleh melebihi 50 karakter")]
        public string Email { get; set; }

        [DisplayName("Telepon")]
        [Required(ErrorMessage = "Telepon wajib diisi")]
        [MinLength(10, ErrorMessage = "Telepon harus memiliki minimal 10 angka")]
        [MaxLength(13, ErrorMessage = "Telepon tidak boleh melebihi 13 angka")]
        public string NoTelp { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "Role belum dipilih")]
        public Nullable<int> ID_Role { get; set; }

        [DisplayName("Section")]
        [Required(ErrorMessage = "Section belum dipilih")]
        public Nullable<int> ID_Seksi { get; set; }

        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }

        [DisplayName("Foto Profil")]
        public string Foto { get; set; }
    }
}