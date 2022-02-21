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
    public partial class KitapAlFormu : Form
    {
        public KitapAlFormu()
        {
            InitializeComponent();
        }
        
        private void KitapAlFormu_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
            SqlCommand kitaplariGetir = new SqlCommand("SELECT Uyeler.Uye_No,Kitaplar.Kitap_id, Kitaplar.KacTane, Uyeler.Adi AS [Üye Adı], Uyeler.Soyadi AS[Üye Soyadı], Uyeler.Eposta AS[Üye E - Posta Adresi], Kitaplar.Adi AS[Kitap Adı], Kitaplar.Yazari AS[Kitabın Yazarı], Kitaplar.BasimEvi AS[Kitabın Basım Evi], Kitaplar.BasimYili AS[Kitabın Basım Yılı], AlinanKitaplar.AlisTarihi AS[Ödünç Alma Tarihi] FROM Uyeler, Kitaplar, AlinanKitaplar WHERE AlinanKitaplar.Kitap_id = Kitaplar.Kitap_id AND AlinanKitaplar.Uye_No = Uyeler.Uye_No AND AlinanKitaplar.TeslimTarihi is NOT Null",baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(kitaplariGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string uye_no = dataGridView1.SelectedCells[0].Value.ToString();
            string kitapId = dataGridView1.SelectedCells[1].Value.ToString();
            int kitapSayisi = Convert.ToInt32(dataGridView1.SelectedCells[2].Value);
            string  adi = dataGridView1.SelectedCells[3].Value.ToString();
            string  soyadi = dataGridView1.SelectedCells[4].Value.ToString();
            string eposta = dataGridView1.SelectedCells[5].Value.ToString();
            string kitapAdi = dataGridView1.SelectedCells[6].Value.ToString();
            string kitapYazari = dataGridView1.SelectedCells[7].Value.ToString();
            string kitapYayinEvi = dataGridView1.SelectedCells[8].Value.ToString();
            string kitapBasimYili = dataGridView1.SelectedCells[9].Value.ToString();
            string alisTarihi = dataGridView1.SelectedCells[10].Value.ToString();

            string mesaj = "Üye Adı: " + adi + "\n Üye Soyadı: " + soyadi + "\n E-Posta Adresi: " + eposta + "\n Olan Üyenin"+alisTarihi+" Tarihinde Almış Olduğu \n Kitap Adı:" + kitapAdi + "\n Kitap Yazarı: " + kitapYazari + "\n Kitap Basım Evi: " + kitapYayinEvi + "\n Kitap Basım Yılı: " + kitapBasimYili + "Olan Kitabı Geri Almak İstiyor Musunuz?";
            DialogResult dr = MessageBox.Show(mesaj,"Kitap Al Onay",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
                SqlCommand KitapAlGuncl = new SqlCommand("UPDATE AlinanKitaplar SET  TeslimTarihi=\'" + DateTime.Now.ToShortDateString() + "\' WHERE Kitap_id=\'"+kitapId+"\' AND Uye_No=\'" +uye_no+"\' AND AlisTarihi=\'"+alisTarihi + "\' AND TeslimTarihi=\'" + DateTime.Now.ToShortDateString() + "\';", baglanti) ;
               
                baglanti.Open();
                KitapAlGuncl.ExecuteNonQuery();
                baglanti.Close();

                SqlCommand KitapsayGuncl = new SqlCommand("UPDATE Kitaplar SET  KacTane=\'" + (++kitapSayisi).ToString() + "\' WHERE Kitap_id=\'" + kitapId + "\';", baglanti);

                baglanti.Open();
                KitapsayGuncl.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Kitap Alındı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand alinankitaplariGetir = new SqlCommand("SELECT Uyeler.Uye_No,Kitaplar.Kitap_id, Kitaplar.KacTane, Uyeler.Adi AS [Üye Adı], Uyeler.Soyadi AS[Üye Soyadı], Uyeler.Eposta AS[Üye E - Posta Adresi], Kitaplar.Adi AS[Kitap Adı], Kitaplar.Yazari AS[Kitabın Yazarı], Kitaplar.BasimEvi AS[Kitabın Basım Evi], Kitaplar.BasimYili AS[Kitabın Basım Yılı], AlinanKitaplar.AlisTarihi AS[Ödünç Alma Tarihi] FROM Uyeler, Kitaplar, AlinanKitaplar WHERE AlinanKitaplar.Kitap_id = Kitaplar.Kitap_id AND AlinanKitaplar.Uye_No = Uyeler.Uye_No AND AlinanKitaplar.TeslimTarihi is Null", baglanti);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(alinankitaplariGetir);
                baglanti.Open();
                da.Fill(dt);
                baglanti.Close();
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
            }
        }
    }
}
