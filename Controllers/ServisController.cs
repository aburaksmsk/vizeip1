using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vıze001.Models;
using vıze001.ViewModel;
namespace vıze001.Controllers
{
    public class ServisController : ApiController
    {
        DB003Entities db = new DB003Entities();
        SepetModel sonuc = new SepetModel();

        #region Kurslar

        [HttpGet]
        [Route("api/kategoriliste")]

        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAd = x.kategoriAd,
                kategoriKodu = x.kategoriKodu,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{urunId}")]

        public KategoriModel kategoribyid(string kategoriId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAd = x.kategoriAd,
                kategoriKodu = x.kategoriKodu,
            }).SingleOrDefault();
            return kayit;

        }

        [HttpPost]
        [Route("api/kategoriekle")]

        public SepetModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.kategoriKodu == model.kategoriKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = " Girilen Kategori Kodu Kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.kategoriId = Guid.NewGuid().ToString();
            yeni.kategoriAd = model.kategoriAd;
            yeni.kategoriKodu = model.kategoriKodu;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kurs Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/kurslarduzenle")]

        public SepetModel kategoriduzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == model.kategoriId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı";
                return sonuc;
            }

            kayit.kategoriAd = model.kategoriAd;
            kayit.kategoriKodu = model.kategoriKodu;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{kategoriId}")]
        public SepetModel KategoriSil(string kategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitKategoriId == kategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategoriye Kayıtlı Ürün Olduğu İçin Silinemez";
                return sonuc;
            }

            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }
        #endregion

        #region Urun

        [HttpGet]
        [Route("api/urunliste")]

        public List<UrunModel> UrunListe()
        {
            List<UrunModel> liste = db.Urun.Select(x => new UrunModel()
            {
                urunId = x.urunId,
                urunAd = x.urunAd,
                urunFiyat = x.urunFiyat,
                urunAdet = x.urunAdet,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/urunbyid/{ogrId}")]

        public UrunModel UrunById(string urunId)
        {
            UrunModel kayit = db.Urun.Where(s => s.urunId == urunId).Select(x => new UrunModel()
            {
                urunId = x.urunId,
                urunAd = x.urunAd,
                urunFiyat = x.urunFiyat,
                urunAdet = x.urunAdet,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]
        public SepetModel OgrenciEkle(UrunModel model)
        {
            if (db.Urun.Count(s => s.urunId == model.urunId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır";
            }
            Urun yeni = new Urun();
            yeni.urunId = Guid.NewGuid().ToString();
            yeni.urunAd = model.urunAd;
            yeni.urunFiyat = model.urunFiyat;
            yeni.urunAdet = model.urunFiyat;
            db.Urun.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/urunduzenle")]

        public SepetModel UrunDuzenle(UrunModel model)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == model.urunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı";
                return sonuc;
            }
            kayit.urunAd = model.urunAd;
            kayit.urunFiyat = model.urunFiyat;
            kayit.urunAdet = model.urunAdet;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Bilgileri Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/urunsil/{urunId}")]
        public SepetModel UrunSil(string urunId)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == urunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitUrunId == urunId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Kategoriye Kayıtlı";
                return sonuc;
            }

            db.Urun.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Bilgileri Silindi";
            return sonuc;
        }

        #endregion

        #region Kayıt
        [HttpGet]
        [Route("api/ogrecikurslarliste/{ogrId}")]

        public List<KayitModel> UrunKategoriListe(string urunId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayiturunId == urunId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitkategoriId = x.kayitkategoriId,
                kayiturunId = x.kayiturunId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.UrunBilgi = UrunById(kayit.kayiturunId);
                kayit.KategoriBilgi = kategoribyid(kayit.kayitkategoriId);
            }
            return liste;
        }

        [HttpGet]
        [Route("api/kategoriurunliste/{kategoriId}")]

        public List<KayitModel> kategoriurunliste(string urunId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitkategoriId == urunId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitkategoriId = x.kayitkategoriId,
                kayiturunId = x.kayiturunId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.UrunBilgi = UrunById(kayit.kayiturunId);
                kayit.KategoriBilgi = kategoribyid(kayit.kayitkategoriId);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SepetModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitkategoriId == model.kayitkategoriId && s.kayiturunId == model.kayiturunId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Ürün Bu Kategoriye Kayıtlıdır";
                return sonuc;

            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayiturunId = model.kayiturunId;
            yeni.kayitkategoriId = model.kayitkategoriId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Kategoriye Kayıt Edildi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SepetModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kursa Kydı Silindi";

            return sonuc;
        }

        #endregion

    }
}


