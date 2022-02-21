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
    public partial class KullaniciAlinanKitaplariListeleFormu : Form
    {
        public KullaniciAlinanKitaplariListeleFormu()
        {
            InitializeComponent();
        }
        public int uye_no = 0;
        private void KullaniciAlinanKitaplariListeleFormu_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=DESKTOP-8LDUEP8 ;Database=Kutuphane;Integrated Security=True;");
            SqlCommand AlinanKitaplariGetir = new SqlCommand("SELECT Uyeler.Uye_No,Kitaplar.Kitap_id,Kitaplar.KacTane, Uyeler.Adi AS[Üye Adı], Uyeler.Soyadi AS[Üye Soyadı], Uyeler.Eposta AS[Üye E - Posta Adresi], Kitaplar.Adi AS[Kitap Adı], Kitaplar.Yazari AS[Kitabın Yazarı], Kitaplar.BasimEvi AS [Kitabın Basım Evi], Kitaplar.BasimYili AS[Kitabın Basım Yılı], AlinanKitaplar.AlisTarihi AS[Ödünç Alma Tarihi], AlinanKitaplar.TeslimTarihi AS[Teslim Tarihi] FROM Uyeler, Kitaplar, AlinanKitaplar WHERE AlinanKitaplar.Kitap_id = Kitaplar.Kitap_id AND AlinanKitaplar.Uye_No = Uyeler.Uye_No AND AlinanKitaplar.TeslimTarihi IS NOT Null AND AlinanKitaplar.Uye_No =\'"+uye_no.ToString()+"\';", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(AlinanKitaplariGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }
    }
}
