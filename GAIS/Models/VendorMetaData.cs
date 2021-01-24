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

        [DisplayName("Vendor Name")]
        [Required(ErrorMessage = "Vendor Name field is required")]
        [MaxLength(50, ErrorMessage = "Vendor Name must be under 50 characters")]
        public string NamaVendor { get; set; }

        [DisplayName("Vendor Address")]
        [Required(ErrorMessage = "Vendor Address field is required")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Vendor Address must be under 50 characters")]
        public string Alamat { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Email field is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        [MaxLength(50, ErrorMessage = "Email must be under 50 characters")]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Phone Number field is required")]
        [MinLength(10, ErrorMessage = "Phone Number must be min 10 digit")]
        [MaxLength(13, ErrorMessage = "Phone Number must be max 13 digit")]
        public string NoTelp { get; set; }

        [DisplayName("Account Number")]
        [Required(ErrorMessage = "Account Number field is required")]
        [MinLength(10, ErrorMessage = "Account Number must be min 10 characters long")]
        [MaxLength(25, ErrorMessage = "Account Number must be max 25 characters long")]
        public string NoRek { get; set; }

        [DisplayName("Bank")]
        [Required(ErrorMessage = "Bank is not selected")]
        public int ID_JenisBank { get; set; }

        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    }
}