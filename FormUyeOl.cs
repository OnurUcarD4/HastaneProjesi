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

namespace HastaneProjesi
{
    public partial class FormUyeOl : Form
    {
        public FormUyeOl()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void btnUyeOl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAd.Text)||string.IsNullOrWhiteSpace(txtSoyad.Text)||string.IsNullOrWhiteSpace(mskTC.Text)||string.IsNullOrWhiteSpace(mskTelefon.Text)||string.IsNullOrWhiteSpace(txtSifre.Text)||string.IsNullOrWhiteSpace(cmbCinsiyet.Text)||mskTC.Text.Length!=11||txtSifre.Text.Length!=6||mskTelefon.Text.Length!=11)
            {
                MessageBox.Show("Boş alanları doldurunuz.");
            }
            else
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", mskTC.Text);
                komut.Parameters.AddWithValue("@p4", mskTelefon.Text);
                komut.Parameters.AddWithValue("@p5", txtSifre.Text);
                komut.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kaydınız Gerçekleşmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            
        }

        private void FormUyeOl_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
