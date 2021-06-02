using System;

namespace restoranRezervasyonSistemi.Data
{
    public class Rezervasyon
    {
        public Rezervasyon()
        {

        }
        public Rezervasyon(int masaNo, string musteriAdi, string musteriSoyAdi, string musteriTelefon)
        {
            masaID = masaNo;
            ad = musteriAdi;
            soyAd = musteriSoyAdi;
            telefon = musteriTelefon;
        }

        public int rezervasyonID { get; set; }
        public int masaID { get; set; }
        public string ad { get; set; }
        public string soyAd { get; set; }
        public string telefon { get; set; }
        public DateTime tarih { get; set; }
    }
}
