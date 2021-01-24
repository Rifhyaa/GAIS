using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GAIS.Models
{
    public class BarangPerusahaanMetaData
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "Item Name field is required")]
        [MaxLength(75, ErrorMessage = "Item Name must be under 75 characters")]
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

        [DisplayName("Section")]
        [Required(ErrorMessage = "Section is not selected")]
        public int ID_Seksi { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price field is required")]
        public int Harga { get; set; }

        [DisplayName("Stock")]
        [Required(ErrorMessage = "Stock is required")]
        [Range(1, 1000, ErrorMessage = "Range should be between 1 & 1000") ]
        public int Stok { get; set; }

        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    }
}