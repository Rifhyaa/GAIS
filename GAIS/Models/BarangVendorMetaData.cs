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

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "Item Name field is required")]
        [MaxLength(100, ErrorMessage = "Item Name must be under 100 characters")]
        public string NamaBarang { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description field is required")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Description must be under 255 characters")]
        public string Keterangan { get; set; }

        [DisplayName("Category Item")]
        [Required(ErrorMessage = "Category Item is not selected")]
        public int ID_JenisBarang { get; set; }

        [DisplayName("Vendor")]
        [Required(ErrorMessage = "Vendor is not selected")]
        public int ID_Vendor { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price field is required")]
        public int Harga { get; set; }

        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    }
}