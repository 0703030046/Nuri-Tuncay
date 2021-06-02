using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace restoranRezervasyonSistemi.Data
{
    public class MasaSQL
    {
        SqlConnection sqlConnection = new SqlConnection("workstation id=rezervasyonSistemi.mssql.somee.com;packet size=4096;user id=rezervasyon_SQLLogin_1;pwd=sjke3wr46f;data source=rezervasyonSistemi.mssql.somee.com;persist security info=False;initial catalog=rezervasyonSistemi");
        SqlCommand cmd;
        SqlDataReader dr;

        public string ekle(Masa masa)
        {
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand("insert into Masa (masaAdi,masaDurumu) values(@masaAdi,@masaDurumu)", sqlConnection);
                cmd.Parameters.AddWithValue("@masaAdi", masa.masaAdi);
                cmd.Parameters.AddWithValue("@masaDurumu", masa.masaDurumu);
                sqlConnection.Close();
                return "Masa eklendi.";
            }
            catch (Exception e)
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();
                return "Bir hata oluştu.\nHata mesajı:" + e.Message;
            }
        }
        public Masa getir(int masaID)
        {
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand("SELECT * FROM Masa where masaID=" + masaID,sqlConnection);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Masa masa = new Masa();
                        masa.masaID = masaID;
                        masa.masaAdi = dr["masaAdi"].ToString();
                        masa.masaDurumu = dr["masaDurumu"].ToString(); 
                        dr.Close();
                        sqlConnection.Close();
                        return masa;
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
        public Masa[] liste()
        {
            List<Masa> masaListesi = new List<Masa>();
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand("SELECT * FROM Masa",sqlConnection);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Masa masa = new Masa();
                        masa.masaID = Convert.ToInt32(dr["masaID"]);
                        masa.masaAdi = dr["masaAdi"].ToString();
                        masa.masaDurumu = dr["masaDurumu"].ToString();
                        masaListesi.Add(masa);
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
            return masaListesi.ToArray();
        }
        public void secildi(string masaID)
        {
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand("update Masa set masaDurumu='Secildi' where masaID="+masaID, sqlConnection);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception )
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
    }
}
