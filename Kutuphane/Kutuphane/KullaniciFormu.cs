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
    public partial class KullaniciFormu : Form
    {
        public KullaniciFormu()
        {
            InitializeComponent();
        }
        public string kullaniciAdi = "";
        public int uye_no = 0;

        private void teslimEttiğimKitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KullaniciAlinanKitaplariListeleFormu frm = new KullaniciAlinanKitaplariListeleFormu();
            frm.uye_no = uye_no;
            frm.MdiParent = this;//Ana Formun İçinde Açılmasını Sağlıyor
            frm.WindowState = FormWindowState.Maximized;// Tam Ekran Olarak Açılmasını Sağlıyor
            frm.Show();
        }

        private void henüzTeslimEtmedimKitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KullaniciVerilenKitaplariListeleFormu frm = new KullaniciVerilenKitaplariListeleFormu();
            frm.uye_no = uye_no;
            frm.MdiParent = this;//Ana Formun İçinde Açılmasını Sağlıyor
            frm.WindowState = FormWindowState.Maximized;// Tam Ekran Olarak Açılmasını Sağlıyor
            frm.Show();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HakkindaFormu frm = new HakkindaFormu();
            frm.ShowDialog();
        }

        private void KullaniciFormu_Load(object sender, EventArgs e)
        {
            this.Text += "Merhaba" + kullaniciAdi;
        }
    }
}
