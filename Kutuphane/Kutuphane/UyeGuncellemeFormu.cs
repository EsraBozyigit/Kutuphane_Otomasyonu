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
    public partial class UyeGuncellemeFormu : Form
    {
        public UyeGuncellemeFormu()
        {
            InitializeComponent();
            ComboBoxDoldur();
            comboBox1.SelectedIndex = -1;
        }
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        private void UyeGuncellemeFormu_Load(object sender, EventArgs e)
        {
            ComboBoxDoldur();
            comboBox1.SelectedIndex = -1;
        }
        void ComboBoxDoldur()
        {
            comboBox1.DataSource = new BindingSource(vi.YoneticiComboboxDataTable(),null);
            comboBox1.DisplayMember = "Uye Eposta Adresi";
            comboBox1.ValueMember = "Uye_No";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 )
            {
                string[] sonuc = vi.YoneticiUyeNoyaGoreUyeBilgisi(comboBox1.SelectedValue.ToString());
                if(sonuc[1] !="-1")
                {
                    try
                    {
                        ad.Text = sonuc[1]; 
                        soyad.Text = sonuc[2];
                        posta.Text = sonuc[3];
                        telefon.Text = sonuc[4]; 
                        dateTimePicker1.Value = DateTime.Parse(sonuc[5]);
                        dateTimePicker2.Value = DateTime.Parse(sonuc[6]);
                        richTextBox1.Text = sonuc[7];
                    }
                    catch
                    { }
                }
                else
                {
                    MessageBox.Show("Veritabanı Bağlantı Hatası Oluştu","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string uye_adi = ad.Text;
            string uye_soyadi = soyad.Text;
            string tel = telefon.Text;
            string adres = richTextBox1.Text;
            string eposta = posta.Text;
            string dogumtarihi = dateTimePicker1.Value.ToShortDateString();
            string uyeliktarihi = dateTimePicker2.Value.ToShortDateString();

            if(vi.YoneticiUyeGuncelleme(comboBox1.SelectedValue.ToString(),uye_adi,uye_soyadi,tel,adres,eposta,uyeliktarihi,dogumtarihi))
            {
                MessageBox.Show("Üye Bilgileri Başarılı Bir Şekilde Güncellendi", "Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Veritabanı Bağlantı Hatası Oluştu","HATA",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            

            
            MessageBox.Show("Kullanıcı Bilgileri Güncellendi", "BAŞARILI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ComboBoxDoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
