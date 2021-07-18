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
    public partial class SekreterPaneli : Form
    {
        public SekreterPaneli()
        {
            InitializeComponent();
        }

        public string TCnumara;

        private void SekreterPaneli_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCnumara;


            //Branş çekme
            SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();


            // Ad Soyad
            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblAd.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //branş
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Bransid as 'Branş No',BransAd as 'Branş Adı' from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktor
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd+' '+ DoktorSoyad) as Doktorlar,DoktorBrans as Branş from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource=dt2;

        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void SekreterPaneli_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevu (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,HastaTC) values (@r1,@r2,@r3,@r4,@r5)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mskTrh.Text);
            komutkaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            komutkaydet.Parameters.AddWithValue("@r5", mskTCkaydet.Text);

            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Kaydedildi.","Randevu",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Doktor Çekme
            SqlCommand komut3 = new SqlCommand("Select (DoktorAd+' '+DoktorSoyad) from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            cmbDoktor.SelectedIndex = -1;
            cmbDoktor.Items.Clear();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rchTxtBox.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.","Duyuru",MessageBoxButtons.OK,MessageBoxIcon.Information);
            rchTxtBox.Text = "";
        }

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            DoktorPaneliSekreter frm = new DoktorPaneliSekreter();
            frm.Show();
        }

        private void btnBrans_Click(object sender, EventArgs e)
        {
            Brans frm = new Brans();
            frm.Show();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            RandevuListesi rndv = new RandevuListesi();
            rndv.Show();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            Duyurular dyr = new Duyurular();
            dyr.Show();
        }
    }
}
