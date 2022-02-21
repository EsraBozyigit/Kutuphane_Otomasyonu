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
    public partial class KitapEklemeFormu : Form
    {
        public KitapEklemeFormu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kitap_adi = kad.Text;
            string kitap_yazari = kyazar.Text;
            string kitap_bevi = kbev.Text;
            string kitap_byil = kbyil.Text;
            string kitap_adedi = numericUpDown1.Value.ToString();
            string Kitap_id = "-1";

            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");

            SqlCommand KitapEklemeKomut = new SqlCommand("INSERT INTO Kitaplar(Adi,Yazari,BasimEvi,BasimYili,KacTane) VALUES(\'" +kitap_adi + "\',\'" + kitap_yazari + "\',\'" + kitap_bevi + "\',\'" + kitap_byil + "\',\'" + kitap_adedi+ "\' );", baglanti);

            SqlCommand KitapGetir = new SqlCommand("SELECT Kitap_id FROM Kitaplar WHERE Adi=\'" + kitap_adi + "\' AND Yazari=\'" + kitap_yazari + "\' AND BasimEvi= \'" + kitap_bevi + "\' AND BasimYili=\'" + kitap_byil + "\' AND KacTane=\'" + kitap_adedi + "\';", baglanti);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(KitapGetir);
            baglanti.Open();

            KitapEklemeKomut.ExecuteNonQuery();
            da.Fill(dt);
            DataRow dr = dt.Rows[0];
            Kitap_id = dr[0].ToString();

            SqlCommand Kitapgetir = new SqlCommand("SELECT Kitap_id FROM Kitaplar WHERE Adi=\'" + kitap_adi + "\' AND Yazari=\'" + kitap_yazari + "\' AND BasimEvi= \'" + kitap_bevi + "\' AND BasimYili=\'" + kitap_byil + "\' AND KacTane=\'" + kitap_adedi + "\';", baglanti);

            Kitapgetir.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kitap Eklendi", "BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        
    }
}
