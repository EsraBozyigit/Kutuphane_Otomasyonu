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
    public partial class KitapVerUyeSecFormu : Form
    {
        public KitapVerUyeSecFormu()
        {
            InitializeComponent();
        }
        void ComboBoxDoldur()
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");

            SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No,Adi AS [Üye Adı],Soyadi AS [Üye Soyadi] , Eposta AS [Üye E-Posta Adresi],Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü], UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler;", baglanti);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            comboBox1.DataSource = new BindingSource(dt, null);
            comboBox1.DisplayMember = "Üye E-Posta Adresi";
            comboBox1.ValueMember = "Uye_No";
        }
        public string kitapId = "";
        public string kitapAdi = "";
        public string kitapYazari = "";
        public string kitapYayinEvi = "";
        public string kitapBasimYili = "";
        public int kitapSayisi = 0;
        public string uye_no = "";
        public string adi = "";
        public string soyadi = "";
        public string eposta = "";
        string alistarihi= "";
        string teslimtarihi = "";
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (comboBox1.SelectedIndex != -1)
                {
                    try
                    {
                        SqlConnection baglanti = new SqlConnection("Server=DESKTOP-TK8P36J ;Database=Kutuphane;Integrated Security=True;");
                        SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No,Adi AS [Üye Adı],Soyadi AS [Üye Soyadi] , Eposta AS [Üye E-Posta Adresi],Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü], UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler WHERE Uye_No=\' " + comboBox1.SelectedValue.ToString() + "\';", baglanti);
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
                        baglanti.Open();
                        da.Fill(dt);
                        baglanti.Close();
                        DataRow dr = dt.Rows[0];
                        uye_no = dr[0].ToString(); //sınıf içinde tanımlı değişleni kullanıyor artık
                        adi = dr[1].ToString(); //sınıf içinde tanımlı değişleni kullanıyor artık
                        soyadi = dr[2].ToString(); //sınıf içinde tanımlı değişleni kullanıyor artık


                        Adi.Text = adi;
                        Soyadi.Text = soyadi;

                    }
                    catch
                    {

                    }
                }
            
        }
            private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Üye Adı: " + Adi.Text + "\n Üye Soyadı: " + Soyadi.Text + "\n E-Posta Adresi: "+ comboBox1.Text + "\n Olan Üyeye \n Kitap Adı:"+ kitapAdi +"\n Kitap Yazarı: "+  kitapYazari + "\n Kitap Basım Evi: " +kitapYayinEvi+ "\n Kitap Basım Yılı: " + kitapBasimYili +"Olan Kitabı Ödünç Vermek İstiyor Musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
               SqlCommand KitapVer = new SqlCommand("UPDATE Kitaplar SET Adi=\'" + kitapAdi + "\' ,Yazari=\'" + kitapYazari + "\' ,BasimEvi= \'" + kitapYayinEvi + "\' ,BasimYili=\'" + kitapBasimYili + "\' ,KacTane=\'" + kitapSayisi + "\' WHERE Kitap_id=\'" + comboBox1.SelectedValue.ToString() + "\';", baglanti);
                SqlCommand Kitapal = new SqlCommand("INSERT INTO AlinanKitaplar(Uye_No,Kitap_id,AlisTarihi,TeslimTarihi) VALUES(\'" + uye_no + "\',\'" + kitapId + "\',\'" + alistarihi+ "\',\'"+teslimtarihi+"\' );", baglanti);
                baglanti.Open();
                KitapVer.ExecuteNonQuery();
                Kitapal.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kitap Ödünç Verildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SqlCommand KitapsayGuncl = new SqlCommand("UPDATE Kitaplar SET  KacTane=\'" + (--kitapSayisi).ToString() + "\' WHERE Kitap_id=\'" + kitapId + "\';", baglanti);

                baglanti.Open();
                KitapsayGuncl.ExecuteNonQuery();
                baglanti.Close();
                ComboBoxDoldur();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KitapVerUyeSecFormu_Load(object sender, EventArgs e)
        {
            ComboBoxDoldur();
            comboBox1.SelectedIndex = -1;
        }
    }
}
