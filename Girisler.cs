using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneProjesi
{
    public partial class Girisler : Form
    {
        public Girisler()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            HastaGiris hasta = new HastaGiris();
            hasta.Show();
            this.Hide();
        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            DoktorGiris doktor = new DoktorGiris();
            doktor.Show();
            this.Hide();
        }

        private void btnSekreter_Click(object sender, EventArgs e)
        {
            SekreterGiris sekreter = new SekreterGiris();
            sekreter.Show();
            this.Hide();
        }

        private void Girisler_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Girisler_Load(object sender, EventArgs e)
        {

        }
    }
}
