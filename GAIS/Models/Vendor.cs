//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAIS.Models
{
    using System;
    using System.Collections.Generic;

    [System.ComponentModel.DataAnnotations.MetadataType(typeof(VendorMetaData))]
    public partial class Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendor()
        {
            this.BarangPerusahaans = new HashSet<BarangPerusahaan>();
            this.BarangVendors = new HashSet<BarangVendor>();
            this.DetailPengajuans = new HashSet<DetailPengajuan>();
        }
    
        public int ID { get; set; }
        public string NamaVendor { get; set; }
        public string Alamat { get; set; }
        public string Email { get; set; }
        public string NoTelp { get; set; }
        public string NoRek { get; set; }
        public int ID_JenisBank { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BarangPerusahaan> BarangPerusahaans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BarangVendor> BarangVendors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailPengajuan> DetailPengajuans { get; set; }
        public virtual JenisBank JenisBank { get; set; }
    }
}
