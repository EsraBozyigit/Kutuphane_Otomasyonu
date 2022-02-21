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
    public partial class UyeListelemeFormu : Form
    {
        public UyeListelemeFormu()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UyeListelemeFormu_Load(object sender, EventArgs e)
        {
            VeritabaniIslemleri vi = new VeritabaniIslemleri();
            dataGridView1.DataSource = vi.YoneticiUyeListesi();
            dataGridView1.Columns[0].Visible = false;
        }
    }
}
