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
    public partial class KitapGuncellemeFormu : Form
    {
        public KitapGuncellemeFormu()
        {
            InitializeComponent();
        }

       
        void ComboBoxDoldur()
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");

            SqlCommand kitapgünGetir = new SqlCommand("SELECT Kitap_id, Adi AS[Kitap Adı], Yazari AS[Yazar Adı], BasimEvi AS[Basım Evi'nin Adı], BasimYili AS[Basım Yılı], KacTane AS[Kaç Adet Kitap] FROM Kitaplar; ", baglanti);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(kitapgünGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            comboBox1.DataSource = new BindingSource(dt, null);
            comboBox1.DisplayMember = "Kitap Adı";
            comboBox1.ValueMember = "Kitap_id";
        }
        private void KitapGuncellemeFormu_Load(object sender, EventArgs e)
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
                    SqlCommand kitapGetir = new SqlCommand("SELECT Kitap_id, Adi AS[Kitap Adı], Yazari AS[Yazar Adı], BasimEvi AS[Basım Evi'nin Adı], BasimYili AS[Basım Yılı], KacTane AS[Kaç Adet Kitap] FROM Kitaplar WHERE Kitap_id=\' " + comboBox1.SelectedValue.ToString() + "\';", baglanti);
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(kitapGetir);
                    baglanti.Open();
                    da.Fill(dt);
                    baglanti.Close();
                    DataRow dr = dt.Rows[0];
                    string Kitap_id = dr[1].ToString();
                    string kitap_adi = dr[2].ToString();
                    string kitap_yazari = dr[3].ToString();
                    string kitap_bevi = dr[4].ToString();
                    string kitap_byil = dr[5].ToString();
                    

                    kad.Text = kitap_adi;
                    kyazar.Text = kitap_yazari;
                    kbev.Text = kitap_bevi;
                    kbyil.Text = kitap_byil;
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kitap_adi = kad.Text;
            string kitap_yazari = kyazar.Text;
            string kitap_bevi = kbev.Text;
            string kitap_byil = kbyil.Text;
            string kitap_adedi = numericUpDown1.Value.ToString();

            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");

            SqlCommand KitapGuncellemeKomutu = new SqlCommand("UPDATE Kitaplar SET Adi=\'" + kitap_adi + "\' ,Yazari=\'" + kitap_yazari + "\' ,BasimEvi= \'" + kitap_bevi + "\' ,BasimYili=\'" + kitap_byil + "\' ,KacTane=\'" + kitap_adedi + "\' WHERE Kitap_id=\'" + comboBox1.SelectedValue.ToString() + "\';", baglanti);
            baglanti.Open();
            KitapGuncellemeKomutu.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kullanıcı Bilgileri Güncellendi", "BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ComboBoxDoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
