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
    public partial class Brans : Form
    {
        public Brans()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void Brans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void listele()
        {
            string sql = "Select * From Tbl_Branslar";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (BransAd) values (@b1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@b1", txtBransAd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Eklendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("delete from Tbl_Branslar where Bransid=@b1", bgl.baglanti());
            sil.Parameters.AddWithValue("@b1", txtID.Text);
            sil.ExecuteNonQuery();
            MessageBox.Show("Branş silindi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            listele();
            bgl.baglanti().Close();

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand guncelle = new SqlCommand("update Tbl_Branslar set Bransad=@g2 where bransid=@g1", bgl.baglanti());
            guncelle.Parameters.AddWithValue("@g1", txtID.Text);
            guncelle.Parameters.AddWithValue("@g2", txtBransAd.Text);
            guncelle.ExecuteNonQuery();
            MessageBox.Show("Branş güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            listele();
            bgl.baglanti().Close();
        }
    }
}
