//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace vıze001.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kayit
    {
        public string kayitId { get; set; }
        public string kayiturunId { get; set; }
        public string kayitkategoriId { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        public virtual Urun Urun { get; set; }
    }
}
