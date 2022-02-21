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
    public partial class KitapVerFormu : Form
    {
        public KitapVerFormu()
        {
            InitializeComponent();
        }

        private void KitapVerFormu_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
            SqlCommand kitaplariGetir = new SqlCommand("SELECT Kitap_id, Adi AS[Kitap Adı], Yazari AS[Yazar Adı], BasimEvi AS[Basım Evi'nin Adı], BasimYili AS[Basım Yılı], KacTane AS[Kaç Adet Kitap] FROM Kitaplar; ", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(kitaplariGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
        }
        private void dataGridView1_DoubleClick(object sender,EventArgs e)
        {
            KitapVerUyeSecFormu frm = new KitapVerUyeSecFormu();
            frm.kitapId = dataGridView1.SelectedCells[0].Value.ToString();
            frm.kitapAdi = dataGridView1.SelectedCells[1].Value.ToString();
            frm.kitapYazari = dataGridView1.SelectedCells[2].Value.ToString();
            frm.kitapYayinEvi = dataGridView1.SelectedCells[3].Value.ToString();
            frm.kitapBasimYili = dataGridView1.SelectedCells[4].Value.ToString();
            frm.kitapSayisi = Convert.ToInt32(dataGridView1.SelectedCells[5].Value);
            frm.ShowDialog();


        }
    }
}
