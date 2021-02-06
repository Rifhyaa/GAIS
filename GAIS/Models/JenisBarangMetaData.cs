using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GAIS.Models
{
    public class JenisBarangMetaData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Jenis Barang")]
        [Required(ErrorMessage = "Jenis Barang wajib diisi")]
        [MaxLength(50, ErrorMessage = "Jenis Barang tidak boleh melebihi 50 karakter")]
        public string Nama { get; set; }

        [DisplayName("Deskripsi")]
        [Required(ErrorMessage = "Deskripsi wajib diisi")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Deskripsi tidak boleh melebihi 255 karakter")]
        public string Keterangan { get; set; }

        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    }
}