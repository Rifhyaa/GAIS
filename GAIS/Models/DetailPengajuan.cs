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
    
    public partial class DetailPengajuan
    {
        public string ID_Pengajuan { get; set; }
        public int ID_Barang { get; set; }
        public Nullable<int> Kuantitas { get; set; }
        public Nullable<int> HargaBarang { get; set; }
        public int ID_Vendor { get; set; }
        public string StatusBarang { get; set; }
    
        public virtual BarangVendor BarangVendor { get; set; }
        public virtual Pengajuan Pengajuan { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
