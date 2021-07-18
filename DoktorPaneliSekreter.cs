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
    public partial class DoktorPaneliSekreter : Form
    {
        public DoktorPaneliSekreter()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void DoktorPaneliSekreter_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd+' '+ DoktorSoyad) as Doktorlar,DoktorBrans as Branş,DoktorTC,DoktorSifre from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;


            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        public void listele()
        {
            string sql = "Select (DoktorAd+' '+ DoktorSoyad) as Doktorlar,DoktorBrans,DoktorTC,DoktorSifre from Tbl_Doktorlar";
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(sql, bgl.baglanti());
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
            bgl.baglanti().Close();
        }



        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            int index = txtAd.Text.IndexOf(" ");
            if (index != -1)
            {
                string soyad = txtAd.Text.Substring(index, txtAd.Text.Length - index);
                string ad = txtAd.Text.Substring(0, index);
                SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", ad);
                komut.Parameters.AddWithValue("@d2", soyad);
                komut.Parameters.AddWithValue("@d3", cmbBrans.Text);
                komut.Parameters.AddWithValue("@d4", mskTC.Text);
                komut.Parameters.AddWithValue("@d5", txtSifre.Text);
                if (txtSifre.Text.Length < 6 || mskTC.Text.Length < 11||cmbBrans.SelectedIndex==-1)
                {
                    MessageBox.Show("Şifreniz 6 hane, TC'niz 11 hane veya branş girişi yapılmış olmak zorundadır.");
                }
                else
                {
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Doktor eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                listele();
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Ad&Soyad arasına boşluk bırakınız.","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            change = true;

            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            change = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroSetButton2_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("Delete from Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            sil.Parameters.AddWithValue("@p1", mskTC.Text);
            sil.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt silindi.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }

        private void metroSetButton3_Click(object sender, EventArgs e)
        {
           
            int index = txtAd.Text.IndexOf(" ");
            string soyad = txtAd.Text.Substring(index, txtAd.Text.Length - index);
            string ad = txtAd.Text.Substring(0, index);
            SqlCommand guncelle = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTC=@d4", bgl.baglanti());
            guncelle.Parameters.AddWithValue("@d1", ad);
            guncelle.Parameters.AddWithValue("@d2", soyad);
            guncelle.Parameters.AddWithValue("@d3", cmbBrans.Text);
            guncelle.Parameters.AddWithValue("@d5", txtSifre.Text);
            guncelle.Parameters.AddWithValue("@d4", mskTC.Text);
            guncelle.ExecuteNonQuery();
            if (txtSifre.Text.Length < 6)
            {
                MessageBox.Show("Şifreniz 6 hane olmak zorunda.");
            }
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            listele();

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            change = false;
        }
        bool change = false;

        private void cmbBrans_TextChanged(object sender, EventArgs e)
        {
            if (change == true) {

                 }
            if (change == false) { cmbBrans.Text = ""; }
            
        }
    }
}
