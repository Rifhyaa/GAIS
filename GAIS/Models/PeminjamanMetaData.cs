using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GAIS.Models
{
    public class PeminjamanMetaData
    {
        public string ID { get; set; }
        public string ID_Karyawan { get; set; }
        public string ID_GA { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TglPeminjaman { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> TglPengembalian { get; set; }

        public Nullable<System.DateTime> LastModifiedTime { get; set; }
        public string AcceptedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> IsLate { get; set; }
        public Nullable<int> Denda { get; set; }
    }
}