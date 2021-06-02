using restoranRezervasyonSistemi.Business;
using System;
using System.Web.UI.WebControls;
using System.Linq;
namespace restoranRezervasyonSistemi
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Masa.secilenMasa != null)
                masaNo.Text = Masa.secilenMasa.masaAdi;

            foreach (var item in Masa.listeGetir())
            {
                if (item.masaTipi == MasaTipi.Bos)
                    item.masaButton.Click += masaSec;
                else
                    item.masaButton.OnClientClick = "return false;";
                masalar.Controls.Add(item.masaButton);

                if (item.masaID <= 9)
                    kucukMasalar.InnerHtml += item.kucukMasa;
                else
                    buyukMasalar.InnerHtml += item.kucukMasa;
            }
        }

        private void masaSec(object sender, EventArgs e)
        {
            Masa.masaSecildi(Convert.ToInt32((sender as LinkButton).ID));
            Response.Redirect(Request.RawUrl);
        }
        protected void rezervasyonEkle(object sender, EventArgs e)
        {
            Masa.rezervasyonEkle(masaNo.Text,musteriAdi.Text,musteriSoyAdi.Text,musteriTelefon.Text);
            Response.Redirect(Request.RawUrl);
        }
    }
}