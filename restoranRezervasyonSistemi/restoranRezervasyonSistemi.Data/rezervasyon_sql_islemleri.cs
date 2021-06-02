using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace restoranRezervasyonSistemi.Data
{
    public  class RezervasyonSQL
    {
         SqlConnection sqlConnection = new SqlConnection("workstation id=rezervasyonSistemi.mssql.somee.com;packet size=4096;user id=rezervasyon_SQLLogin_1;pwd=sjke3wr46f;data source=rezervasyonSistemi.mssql.somee.com;persist security info=False;initial catalog=rezervasyonSistemi");
         SqlCommand cmd;
         SqlDataReader dr;

        public void ekle(Rezervasyon rezervasyon)
        {
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand("insert into Rezervasyon (masaID,ad,soyAd,telefon,tarih) values(@masaID,@ad,@soyAd,@telefon,@tarih)", sqlConnection);
                cmd.Parameters.AddWithValue("@ad", rezervasyon.ad);
                cmd.Parameters.AddWithValue("@soyAd", rezervasyon.soyAd);
                cmd.Parameters.AddWithValue("@telefon", rezervasyon.telefon);
                cmd.Parameters.AddWithValue("@masaID", rezervasyon.masaID);
                cmd.Parameters.AddWithValue("@tarih", DateTime.Now.ToUniversalTime().AddHours(3));
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception )
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
        public  Rezervasyon getir(int rezervasyonID)
        {
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand("SELECT * FROM Rezervasyon where rezervasyonID=" + rezervasyonID, sqlConnection);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Rezervasyon rezervasyon = new Rezervasyon();
                        rezervasyon.ad = dr["ad"].ToString();
                        rezervasyon.soyAd = dr["soyAd"].ToString();
                        rezervasyon.telefon = dr["telefon"].ToString();
                        rezervasyon.masaID = Convert.ToInt32(dr["masaID"]);
                        rezervasyon.tarih = Convert.ToDateTime(dr["tarih"].ToString());
                        dr.Close();
                        sqlConnection.Close();
                        return rezervasyon;
                    }
                }
            }
            catch (Exception)
            {
                dr.Close();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();
            }

            return null;
        }
        public  Rezervasyon[] guncelListe()
        {
            List<Rezervasyon> rezervasyonListesi = new List<Rezervasyon>();
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand("SELECT * FROM Rezervasyon where tarih>'" + DateTime.Now.ToUniversalTime().AddHours(3).AddMinutes(-1).ToString("MM.dd.yyyy HH:mm:ss")+"'", sqlConnection);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Rezervasyon rezervasyon = new Rezervasyon();
                        rezervasyon.ad = dr["ad"].ToString();
                        rezervasyon.soyAd = dr["soyAd"].ToString();
                        rezervasyon.telefon = dr["telefon"].ToString();
                        rezervasyon.masaID = Convert.ToInt32(dr["masaID"]);
                        rezervasyon.tarih = Convert.ToDateTime(dr["tarih"].ToString());
                        rezervasyonListesi.Add(rezervasyon);
                    }
                }
            }
            catch (Exception)
            {
                dr.Close();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();
            }
            dr.Close();
            sqlConnection.Close();
            return rezervasyonListesi.ToArray();
        }
    }
}
