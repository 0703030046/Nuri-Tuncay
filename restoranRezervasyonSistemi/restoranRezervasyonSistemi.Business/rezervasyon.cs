using restoranRezervasyonSistemi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restoranRezervasyonSistemi.Business
{
   public class rezervasyon
    {
        RezervasyonSQL rezervasyonSQL = new RezervasyonSQL();

        public void ekle(Rezervasyon rezervasyon)
        {
            rezervasyonSQL.ekle(rezervasyon);
        }
        public Rezervasyon getir(int rezervasyonID)
        {
            return rezervasyonSQL.getir(rezervasyonID);
        }

        public Rezervasyon[] guncelListe()
        {
            return rezervasyonSQL.guncelListe();
        }
    }
}
