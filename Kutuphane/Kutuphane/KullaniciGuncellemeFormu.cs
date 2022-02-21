using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kutuphane
{
    public partial class KullaniciGuncellemeFormu : Form
    {
        public KullaniciGuncellemeFormu()
        {
            InitializeComponent();
        }
        void ComboBoxDoldur()
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");

            SqlCommand kullaniciGetir = new SqlCommand("SELECT Uye_No, KullaniciAdi AS[Kullanıcı Adı], Sifre AS[Kullanıcı Şifre], Yonetici AS[Yönetici Durumu] FROM Kullanicilar;", baglanti);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(kullaniciGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            comboBox1.DataSource = new BindingSource(dt, null);
            comboBox1.DisplayMember = "Kullanıcı Adı";
            comboBox1.ValueMember = "Uye_No";
        }
        private void KullaniciGuncellemeFormu_Load(object sender, EventArgs e)
        {
            ComboBoxDoldur();
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                try
                {
                    SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
                    SqlCommand kullaniciGetir = new SqlCommand("SELECT Uye_No,KullaniciAdi AS [Kullanıcı Adı],Sifre AS [Kullanıcı Şifre] , Yonetici AS [Yönetici Durumu] FROM Kullanicilar WHERE Uye_No=\' " + comboBox1.SelectedValue.ToString() + "\';", baglanti);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(kullaniciGetir);
                    baglanti.Open();
                    da.Fill(dt);
                    baglanti.Close();
                    DataRow dr = dt.Rows[0];
                    string kadi = dr[1].ToString();
                    string ksifre = dr[2].ToString();

                    kad.Text = kadi;
                    ksif.Text = ksifre;

                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullanici_adi = kad.Text;
            string kullanici_sif = ksif.Text;
            string y = "0";
            if (checkBox1.Checked)
            {
                y = "1";
            }
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
            SqlCommand kullaniciGuncellemeKomutu = new SqlCommand("UPDATE Kullanicilar SET KullaniciAdi=\'" + kullanici_adi + "\' ,Sifre=\'" + kullanici_sif + "\' ,Yonetici=\'" + y + "WHERE Uye_No=\'" + comboBox1.SelectedValue.ToString() + "\';", baglanti);
            baglanti.Open();
            kullaniciGuncellemeKomutu.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kullanıcı Bilgileri Güncellendi", "BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ComboBoxDoldur();
        }
    }
}
