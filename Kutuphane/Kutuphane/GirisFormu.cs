using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class GirisFormu : Form
    {
        public GirisFormu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            VeritabaniIslemleri vi = new VeritabaniIslemleri();
            string[] sonuc = vi.kullaniciGirisKontrolu(adi.Text, sifre.Text);
                if (sonuc[0]=="1")
                {
                string kullaniciAdi = sonuc[1];
                    int yetki = Convert.ToInt32(sonuc[2]);
                    int uye_no = Convert.ToInt32(sonuc[3]);
                    if (yetki == 1)
                    {
                        yfrm frm = new yfrm();
                        this.Hide();
                        this.adi.Text = "";
                        this.sifre.Text = "";
                        frm.ShowDialog();
                    }
                    else
                    {
                        KullaniciFormu kFrm = new KullaniciFormu();
                        this.Hide();
                        adi.Text = "";
                        sifre.Text = "";
                        kFrm.kullaniciAdi = kullaniciAdi;
                        kFrm.uye_no = uye_no;
                        kFrm.ShowDialog();
                        adi.Focus();
                    }

                }
            
                else
                {
                  
                    MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
        }
    }
}
