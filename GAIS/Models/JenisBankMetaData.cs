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

        [DisplayName("Bank")]
        [Required(ErrorMessage = "Bank field is required.")]
        [MaxLength(50, ErrorMessage = "Bank must be under 50 characters")]
        public string Nama { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description field is required.")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Description must be under 255 characters")]
        public string Keterangan { get; set; }

        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int RowStatus { get; set; }
    }
}