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
    public partial class KitapListelemeFormu : Form
    {
        public KitapListelemeFormu()
        {
            InitializeComponent();
        }

        private void KitapListelemeFormu_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
            SqlCommand kitapGetir = new SqlCommand("SELECT Kitap_id, Adi AS[Kitap Adı], Yazari AS[Yazar Adı], BasimEvi AS[Basım Evi'nin Adı], BasimYili AS[Basım Yılı], KacTane AS[Kaç Adet Kitap] FROM Kitaplar; ", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(kitapGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }
    }
}
