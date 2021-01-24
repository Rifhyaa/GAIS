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

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(100, ErrorMessage = "Name must be under 100 characters")]
        public string NamaKaryawan { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is not selected")]
        public string JenisKelamin { get; set; }

        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "Birth Date field is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TanggalLahir { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Address field is required")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255, ErrorMessage = "Address must be under 255 characters")]
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
        [DataType(DataType.PhoneNumber)]
        public string NoTelp { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "Role is not selected")]
        public Nullable<int> ID_Role { get; set; }

        [DisplayName("Section")]
        [Required(ErrorMessage = "Section is not selected")]
        public Nullable<int> ID_Seksi { get; set; }

        public string Password { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }

        [DisplayName("Profile Picture")]
        public string Foto { get; set; }
    }
}