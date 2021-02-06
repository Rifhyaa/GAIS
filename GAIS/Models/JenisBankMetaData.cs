using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GAIS.Models
{
    public class JenisBankMetaData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Nama Bank")]
        [Required(ErrorMessage = "Nama Bank wajib diisi")]
        [MaxLength(50, ErrorMessage = "Nama Bank tidak boleh melebihi 50 karakter")]
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
        public int RowStatus { get; set; }
    }
}