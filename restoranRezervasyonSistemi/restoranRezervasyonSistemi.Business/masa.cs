using restoranRezervasyonSistemi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace restoranRezervasyonSistemi.Business
{
    public enum MasaTipi { Bos, Dolu, Secildi, Kapali }
    public class Masa
    {

        public static Masa masa = new Masa();
        public static Masa secilenMasa;
        private static MasaSQL masaSQL;
        private static RezervasyonSQL rezervasyonSQL;
        public static List<Masa> masaListesi;

        public MasaTipi masaTipi;
        public string masaAdi;
        public string kucukMasa;
        public LinkButton masaButton = new LinkButton();
        public int masaID;

        public Masa()
        {
            masaSQL = new MasaSQL();
            rezervasyonSQL = new RezervasyonSQL();
            masaListesi = listeGetir();
        }
        public Masa(Data.Masa masaGelen, Rezervasyon rezervasyon)
        {
            masaOlustur(masaGelen, rezervasyon);
        }
        public Masa(string masaTipi,int masaID,  string masaAdi)
        {
            Data.Masa dataMasa = new Data.Masa();

            dataMasa.masaAdi = masaAdi;
            dataMasa.masaDurumu = masaTipi;
            dataMasa.masaID = masaID;

            masaOlustur(dataMasa, null);
        }


        public static List<Masa> listeGetir()
        {
            Rezervasyon[] rezervasyonListesi = rezervasyonSQL.guncelListe();
            Data.Masa[] geciciListe = masaSQL.liste();
            if (masaListesi == null)
                masaListesi = new List<Masa>();
            else
                masaListesi.Clear();



            for (int i = 0; i < geciciListe.Length; i++)
            {
                if (rezervasyonListesi.FirstOrDefault(x => x.masaID == geciciListe[i].masaID) != null)
                {
                    if (i != 9)
                        if (i > 0)
                            if (geciciListe[i - 1].masaDurumu == "Bos")
                                geciciListe[i - 1].masaDurumu = "Kapali";

                    if (i != 8)
                        if (i < geciciListe.Length - 1)
                            if (geciciListe[i + 1].masaDurumu == "Bos")
                                geciciListe[i + 1].masaDurumu = "Kapali";
                }
            }


            foreach (var item in geciciListe)
            {
                if (rezervasyonListesi.FirstOrDefault(x => x.masaID == item.masaID) != null)
                {
                    item.masaDurumu = "Dolu";
                    masaListesi.Add(new Masa(item, rezervasyonListesi.FirstOrDefault(x => x.masaID == item.masaID)));
                    continue;
                }
                else
                    if (secilenMasa != null)
                {
                    if (secilenMasa.masaID == item.masaID)
                        item.masaDurumu = "Secildi";
                }
                masaListesi.Add(new Masa(item,null));
            }
            return masaListesi;
        }

        private void masaOlustur(Data.Masa masaGelen, Rezervasyon rezervasyon)
        {
            string arkaPlanRengi, yaziRengi, ikon, yazi;


            switch (masaGelen.masaDurumu)
            {
                case "Bos":
                    {
                        masaTipi = MasaTipi.Bos;
                        arkaPlanRengi = "cyan lighten-3";
                        yaziRengi = "white-text";
                        yazi = "Boş";
                        ikon = "fas fa-calendar-check";
                        break;
                    }
                case "Dolu":
                    {
                        masaTipi = MasaTipi.Dolu;
                        arkaPlanRengi = "pink lighten-3";
                        yaziRengi = "white-text";
                        yazi = "Dolu";
                        ikon = "fas fa-calendar-times";
                        break;
                    }
                case "Secildi":
                    {
                        masaTipi = MasaTipi.Secildi;
                        arkaPlanRengi = "deep-purple lighten-3";
                        yaziRengi = "white-text";
                        yazi = "Seçildi";
                        ikon = "fas fa-mouse-pointer";
                        break;
                    }
                case "Kapali":
                    {
                        masaTipi = MasaTipi.Kapali;
                        arkaPlanRengi = "light-green darken-1";
                        yaziRengi = "white-text";
                        yazi = "Kapalı";
                        ikon = "fas fa-shield-virus";
                        break;
                    }
                default:
                    return;
            }

            masaID = masaGelen.masaID;
            masaAdi = masaGelen.masaAdi;
            masaButton.ID = masaID.ToString();
            masaButton.CssClass = "col-md-4 col-sm-6 col-lg-3 mb-3";
            kucukMasa =
                "<div class=' mb-2 text-center card " + arkaPlanRengi + " " + yaziRengi + "'>" + masaAdi + "</div>";
            masaButton.Text =
                        "<div class='card " + arkaPlanRengi + " " + yaziRengi + "'>" +
                            "<div class='card-body d-flex justify-content-between align-items-center'>" +
                                "<div>" +
                                    "<p class='h2-responsive font-weight-bold mt-n2 mb-0'>" + masaAdi + "</p>" +
                                    "<p class='mb-0'>" + (masaTipi == MasaTipi.Dolu ? rezervasyon.ad + " " + rezervasyon.soyAd : yazi) + "</p>" +
                                "</div>" +
                                "<div>" +
                                    "<i class='" + ikon + " fa-4x text-black-40'></i>" +
                                "</div>" +
                            "</div>" + (masaTipi==MasaTipi.Dolu? "<i class='text-center'>"+(rezervasyon.tarih.AddMinutes(1)-DateTime.UtcNow.AddHours(3)).TotalSeconds.ToString("F0") +" saniye kaldı.</i>":"")+
                        "</div>";
        }

        public static void rezervasyonEkle(string masaAdi, string musteriAdi, string musteriSoyAdi, string musteriTelefon)
        {
            Masa masa = masaListesi.FirstOrDefault(x => x.masaAdi == masaAdi);
            if (masa == null)
                return;

            if (musteriAdi == null || musteriSoyAdi == null || musteriTelefon == null)
                return;

            rezervasyonSQL.ekle(new Rezervasyon(masa.masaID, musteriAdi, musteriSoyAdi, musteriTelefon));
        }

        public static void masaSecildi(int masaID)
        {
            Masa temp;
            Masa masa = masaListesi.FirstOrDefault(x => x.masaID == masaID);
            if (masa != null)
            {
                if (secilenMasa != null)
                {
                    temp = new Masa("Bos", secilenMasa.masaID, secilenMasa.masaAdi);
                    masaListesi.FirstOrDefault(x => x.masaID == temp.masaID).masaButton = temp.masaButton;
                }
                secilenMasa = masaListesi.FirstOrDefault(x => x.masaID == masaID);
            }
        }

    }
}
