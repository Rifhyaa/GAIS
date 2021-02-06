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

    [System.ComponentModel.DataAnnotations.MetadataType(typeof(JenisBarangMetaData))]
    public partial class JenisBarang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JenisBarang()
        {
            this.BarangPerusahaans = new HashSet<BarangPerusahaan>();
            this.BarangVendors = new HashSet<BarangVendor>();
        }
    
        public int ID { get; set; }
        public string Nama { get; set; }
        public string Keterangan { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> RowStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BarangPerusahaan> BarangPerusahaans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BarangVendor> BarangVendors { get; set; }
    }
}
