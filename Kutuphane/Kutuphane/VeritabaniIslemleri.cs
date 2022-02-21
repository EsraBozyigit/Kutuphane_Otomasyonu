using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kutuphane
{
    class VeritabaniIslemleri
    {
        ///<summary>
        ///Bağlantı Cümleciği
        /// </summary>
        public SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KutuphaneOtomasyonu.mdf;Integrated Security=True;");

        //KULLANICII
        public string[] kullaniciGirisKontrolu(string KullaniciAdi, string Sifre)
        {
            string[] bilgiler = new string[4];
            SqlCommand komut = new SqlCommand("SELECT KullaniciAdi,Yonetici,Uye_No FROM Kullanicilar WHERE KullaniciAdi=\'" + KullaniciAdi + "\' AND Sifre = \'" + Sifre + "\';", baglanti);
            baglanti.Open();
            SqlDataReader okuyucu = komut.ExecuteReader();
            if (okuyucu.HasRows)
            {
                okuyucu.Read();
                bilgiler[0] = "1";
                bilgiler[1] = okuyucu.GetString(0);
                bilgiler[2] = okuyucu.GetString(1);
                bilgiler[3] = okuyucu.GetInt32(2).ToString();

            }
            else
            {
                bilgiler[0] = "0";
                bilgiler[1] = "0"; 
                bilgiler[2] = "0";
                bilgiler[3] = "0";
            }
            baglanti.Close();
            return bilgiler;
        }
        ///<summary>
        ///
        /// </summary>
        /// <param name="KullaniciAdi"></param>
        /// <param name="Sifre"></param>
        /// <returns></returns

        //YÖNETİCİİ
        public bool YoneticiUyeEkleme(string uye_adi, string uye_soyadi, string tel, string adres, string posta, string uyeliktarihi, string dogumtarihi, string kullaniciAdi, string sifre, string yonetici)
        {
            try
            {
                SqlCommand uyeEklemeKomut = new SqlCommand("INSERT INTO Uyeler(Adi,Soyadi,Telefon,Adres,Eposta,UyelikTarihi,DogumTarihi) VALUES(\'" + uye_adi + "\',\'" + uye_soyadi + "\',\'" + tel + "\',\'" + adres + "\',\'" + posta + "\',\'" + uyeliktarihi + "\',\'" + dogumtarihi + "\' );", baglanti);
                SqlCommand uyeNoGetir = new SqlCommand("SELECT Uye_No FROM Uyeler WHERE Adi=\'" + uye_adi + "\' AND Soyadi=\'" + uye_soyadi + "\' AND Telefon= \'" + tel + "\';", baglanti);

                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(uyeNoGetir);
                baglanti.Open();

                uyeEklemeKomut.ExecuteNonQuery();
                da.Fill(dt);
                DataRow dr = dt.Rows[0];
                string Uye_No = dr[0].ToString();

                SqlCommand kullaniciEkleKomutu = new SqlCommand("INSERT INTO Kullanicilar(KullaniciAdi,Sifre,Uye_No,Yonetici) VALUES(\'" + kullaniciAdi + "\',\'" + sifre + "\',\'" + Uye_No + "\',\'" + yonetici + "\');", baglanti);

                kullaniciEkleKomutu.ExecuteNonQuery();

                baglanti.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///<summary>
        ///Üye EKleme İşlemi Aynı Zamanda da Kullanıcı Ekleme İşlemidir.
        /// </summary>
        /// <param name="uye_adi">Üyenin Adı</param>
        /// <param name="uye_soyadi">Üyenin Soyadı</param>
        ///<param name="tel">Üyenin Telefonu</param>
        /// <param name="adres">Üyenin Adresi</param>
        /// <param name="posta">Üyenin Eposta Adresi</param>
        /// <param name="uyeliktarihi">Üyenin Üyelik Tarihi</param>
        /// <param name="dogumtarihi">Üyenin Doğum Tarihi</param>
        /// <param name="kullaniciAdi">Üyenin Kullanici Adi</param>
        /// <param name="sifre">Üyenin Kullanıcı Şifresi</param>
        /// <param name="yonetici">Üyenin Yönetici Olup Olmadığı</param>
        /// <returns>Üye ekleme işlemi başarılı olursa true, başarısız olursa false değeri geri döndüürür</returns>


        ///<summary>
        ///Uye Güüncelleme  Ve Silme Formlarındaki ComboBox İçin Veritablosu Oluşturur.
        /// </summary>
        /// <returns>DataTable Türünde Veri Geri Döndürür</returns>

        //YÖNETİCİİ
        public DataTable YoneticiComboboxDataTable()
        {
            SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No,Adi AS [Uye Adi],Soyadi AS [Üye Soyadı,Eposta AS [Üye E-Posta Adresi]," + "Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü] ,UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler;", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            return dt;
        }

        ///<summary>
        ///
        /// </summary>
        /// <param name="uye_no">Bilgileri Geri Döndürülecek Üyenin Üye Nosu
        /// </param>
        /// <returns></returns>

        //YÖNETİCİİ
        public string[] YoneticiUyeNoyaGoreUyeBilgisi(string uye_no)
        {
            string[] bilgiler = new string[8];
            try
            {
                SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No,Adi AS [Üye Adı],Soyadi AS [Üye Soyadi] , Eposta AS [Üye E-Posta Adresi],Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü], UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler WHERE Uye_No=\' " + uye_no + "\';", baglanti);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
                baglanti.Open();
                da.Fill(dt);
                baglanti.Close();
                DataRow dr = dt.Rows[0];
                bilgiler[0] = dr[0].ToString();
                bilgiler[1] = dr[1].ToString();
                bilgiler[2] = dr[2].ToString();
                bilgiler[3] = dr[3].ToString();
                bilgiler[4] = dr[4].ToString();
                bilgiler[5] = dr[5].ToString();
                bilgiler[6] = dr[6].ToString();
                bilgiler[7] = dr[7].ToString();

            }
            catch(Exception e)
            {
                bilgiler[0] = "-1";
                if(baglanti.State==ConnectionState.Open)
                {
                    baglanti.Close();
                }
               
            } 
            return bilgiler;
        }
        ///<summary>
        ///Verilen uye_nosuna ait üyenin bilgilerini günceller
        /// </summary>
        /// <param name="uye_no">Üyenin Nosu</param>
        /// <param name="uye_adi">Üyenin Adı</param>
        /// <param name="uye_soyadi">Üyenin Soyadı</param>
        ///<param name="tel">Üyenin Telefonu</param>
        /// <param name="adres">Üyenin Adresi</param>
        /// <param name="posta">Üyenin Eposta Adresi</param>
        /// <param name="uyeliktarihi">Üyenin Üyelik Tarihi</param>
        /// <param name="dogumtarihi">Üyenin Doğum Tarihi</param>
        /// <returns>Üye güncelleme işlemi başarılı olursa true, başarısız olursa false değeri geri döndüürür</returns>

        //YÖNETİCİİ
        public bool YoneticiUyeGuncelleme(string  uye_no ,string uye_adi, string uye_soyadi, string tel, string adres, string posta, string uyeliktarihi, string dogumtarihi)
        {
            try
            {
                SqlCommand uyeGuncellemeKomutu = new SqlCommand("UPDATE Uyeler SET Adi=\'" + uye_adi + "\' ,Soyadi=\'" + uye_soyadi + "\' ,Telefon=\'" + tel + "\' ,Adres=\'" + adres + "\' ,Eposta=\'" + posta
                + "\' ,UyelikTarihi=\'" + uyeliktarihi + "\' ,DogumTarihi=\'" + dogumtarihi + "\' WHERE Uye_No=\'" + uye_no + "\';", baglanti);
                baglanti.Open();
                uyeGuncellemeKomutu.ExecuteNonQuery();
                baglanti.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///<summary>
        ///Verilen uye_no suna ait üyeyi siler. Üyenin daha önceden aldığı kitap kayıtları ve kullanıcıbilgileri de silinir.
        /// </summary>
        /// <param name="uye_no">Üye No</param>
        /// <returns>Üye silme işlemi başarılı olursa true, başarısız olursa false değeri geri döndürür</returns>

        //YÖNETİCİİ
        public bool YoneticiUyeSilme(string uye_no)
        {
            try
            {
                SqlCommand alinankitapSil = new SqlCommand("DELETE FROM AlinanKitaplar WHERE Uye_No=\' " + uye_no+"\';",baglanti);
                baglanti.Open();

                SqlCommand kullaniciSil = new SqlCommand("DELETE FROM Kullanicilar WHERE Uye_No=\' " + uye_no + "\';", baglanti);

                SqlCommand uyeyiSil = new SqlCommand("DELETE FROM Uyeler WHERE Uye_No=\' " + uye_no + "\';", baglanti);
                
                kullaniciSil.ExecuteNonQuery();
                alinankitapSil.ExecuteNonQuery(); 
                uyeyiSil.ExecuteNonQuery();
                baglanti.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        ///<summary>
        ///Üye Listesini DataTable türünde geri döndürür
        /// </summary>
        /// <returns>Üye tablosunun içinde bilgiler vardır</returns>
        
        //YÖNETİCİİ
        public DataTable YoneticiUyeListesi()
        {
            SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No,Adi AS [Uye Adi],Soyadi AS [Üye Soyadı,Eposta AS [Üye E-Posta Adresi]," + "Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü] ,UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler;", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            return dt;
        }
    }
}
