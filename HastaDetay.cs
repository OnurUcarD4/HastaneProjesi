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
    public partial class HastaDetay : Form
    {
        public HastaDetay()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public string tc;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void HastaDetay_Load(object sender, EventArgs e)
        {
            // ad soyad çekme
            lblTC.Text = tc;
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] +" "+ dr[1];
            }
            bgl.baglanti().Close();
            //randevu geçmiş
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select RandevuTarih as Tarih,RandevuSaat as Saat,RandevuBrans as Branş,RandevuDoktor as Doktor from Tbl_Randevu where HastaTC='"+tc+"'", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //branş çekme
            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void HastaDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Clear();
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select id,RandevuTarih as Tarih,RandevuSaat as Saat,RandevuBrans as Branş,RandevuDoktor as Doktor,RandevuDurum as Durum from Tbl_Randevu where RandevuBrans='" + cmbBrans.Text + "'" + " and RandevuDoktor='" + cmbDoktor.Text + "'"+ "and Randevudurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BilgiDuzenle fr = new BilgiDuzenle();
            fr.tcnm = lblTC.Text;
            fr.Show();
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();

        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Randevu set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where id=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", rchSikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu alındı.","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
