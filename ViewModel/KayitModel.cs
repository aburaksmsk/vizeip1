using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vıze001.ViewModel
{
    public class KayitModel
    {
        internal object kayitUrunId;
        internal object kayitKategoriId;

        public string kayitId { get; set; }
        public string kayiturunId { get; set; }
        public string kayitkategoriId { get; set; }
        public object KategoriBilgi { get; internal set; }
        public object UrunBilgi { get; internal set; }
    }
}