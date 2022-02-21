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
    public partial class UyeSilmeFormu : Form
    {
        public UyeSilmeFormu()
        {
            InitializeComponent();
            ComboBoxDoldur();
            comboBox1.SelectedIndex = -1;
        }
        VeritabaniIslemleri vi = new VeritabaniIslemleri();
        private void UyeSilmeFormu_Load(object sender, EventArgs e)
        {

            ComboBoxDoldur();
            comboBox1.SelectedIndex = -1;
        }
        void ComboBoxDoldur()
        {
            comboBox1.DataSource = new BindingSource(vi.YoneticiComboboxDataTable(),null);
            comboBox1.DisplayMember = "Üye E-Posta Adresi";
            comboBox1.ValueMember = "Uye_No";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string[] sonuc = vi.YoneticiUyeNoyaGoreUyeBilgisi(comboBox1.SelectedValue.ToString());
                if (sonuc[1] != "-1")
                {
                    try
                    {
                        ad.Text = sonuc[1];
                        soyad.Text = sonuc[2];
                        posta.Text = sonuc[3];
                        tel.Text = sonuc[4];
                        dateTimePicker1.Value = DateTime.Parse(sonuc[5]);
                        dateTimePicker2.Value = DateTime.Parse(sonuc[6]);
                        richTextBox1.Text = sonuc[7];
                    }
                    catch
                    { }
                }
                else
                {
                    MessageBox.Show("Veritabanı Bağlantı Hatası Oluştu", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Üye Adı: "+ad.Text+ "\n Üye Soyadı: " + soyad.Text+"\n Olan Kullanıcıyı Silmek İstediğinizden Emin Misiniz ?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if(dr == DialogResult.Yes)
            {
                    if(vi.YoneticiUyeSilme(comboBox1.SelectedValue.ToString()))
                    {
                        MessageBox.Show("Üye Başarlı Bir Şekilde Silindi", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ComboBoxDoldur();
                    }
                    else
                    {
                        MessageBox.Show("Veritabanında balantı hatası oluştu", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
