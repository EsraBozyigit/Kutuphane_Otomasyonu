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
    public partial class KullaniciListelemeFormu : Form
    {
        public KullaniciListelemeFormu()
        {
            InitializeComponent();
        }

        private void KullaniciListelemeFormu_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
            SqlCommand kullaniciGetir = new SqlCommand("SELECT Uye_No ,KullaniciAdi AS[Kullanıcı Adı], Sifre AS[Kullanıcı Şifre], Yonetici AS[Kullanıcı Yöneticilik Durumu] FROM Kullanicilar; ", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(kullaniciGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }
    }
}
