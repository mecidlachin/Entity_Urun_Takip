using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityProjeUygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbEntityUrunEntities db = new DbEntityUrunEntities();
        private void btnListele_Click(object sender, EventArgs e)
        {
            var kategoriler = db.Tbl_Kategori.ToList();
            dataGridView1.DataSource = kategoriler;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKategoriAdi.Text))
            {
                MessageBox.Show("Kategori Adı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Tbl_Kategori t = new Tbl_Kategori();
            t.Ad = txtKategoriAdi.Text;
            db.Tbl_Kategori.Add(t);
            db.SaveChanges();
            MessageBox.Show("Kategori Eklendi");
            txtKategoriId.Text = "";
            txtKategoriAdi.Text = "";
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKategoriId.Text))
            {
                MessageBox.Show("Lütfen silinecek bir kategori seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int x = Convert.ToInt32(txtKategoriId.Text);
            var ktgr = db.Tbl_Kategori.Find(x);

            if (ktgr == null)
            {
                MessageBox.Show("Seçilen kategori bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.Tbl_Kategori.Remove(ktgr);
            db.SaveChanges();
            MessageBox.Show("Kategori Silindi");
            txtKategoriId.Text = "";
            txtKategoriAdi.Text = "";
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKategoriId.Text))
            {
                MessageBox.Show("Lütfen güncellenecek bir kategori seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int x = Convert.ToInt32(txtKategoriId.Text);
            var ktgr = db.Tbl_Kategori.Find(x);

            if (ktgr == null)
            {
                MessageBox.Show("Seçilen kategori bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKategoriAdi.Text))
            {
                MessageBox.Show("Kategori Adı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ktgr.Ad = txtKategoriAdi.Text;
            db.SaveChanges();
            MessageBox.Show("Güncelleme Yapıldı");
            txtKategoriId.Text = "";
            txtKategoriAdi.Text = "";
        }

    }
}
