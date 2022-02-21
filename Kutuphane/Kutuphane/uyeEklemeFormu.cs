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
    public partial class uyeEklemeFormu : Form
    {
        public uyeEklemeFormu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uye_adi = ad.Text;
            string uye_soyadi = soyad.Text;
            string tel = telefon.Text;
            string posta = eposta.Text;
            string dogumtarihi = dateTimePicker1.Value.ToShortDateString();
            string uyeliktarihi = dateTimePicker2.Value.ToShortDateString();
            string kullaniciAdi = kullaniciadi.Text;
            string sifre = sfr.Text;
            string adres = richTextBox1.Text;
            string yonetici = "0";
            if(checkBox1.Checked==true)
            {
                yonetici = "1";
            }

            VeritabaniIslemleri vi = new VeritabaniIslemleri();
            if(vi.YoneticiUyeEkleme(uye_adi,uye_soyadi,tel,adres,posta,uyeliktarihi,dogumtarihi,kullaniciAdi,sifre,yonetici))
            {
                 MessageBox.Show("Kullanıcı Eklendi","BAŞARILI",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kullanıcı Eklenemedi","HATA",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
