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
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        DbEntityUrunEntities db = new DbEntityUrunEntities();
        private void btnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (from x in db.Tbl_Urun
                                        select new
                                        {
                                            x.UrunId,
                                            x.UurnAd,
                                            x.Marka,
                                            x.Stok,
                                            x.Fiyat,
                                            x.Tbl_Kategori.Ad,
                                            x.Durum
                                        }).ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrunAd.Text) ||
                string.IsNullOrWhiteSpace(txtMarka.Text) ||
                string.IsNullOrWhiteSpace(txtStok.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(txtFiyat.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Boş değer varsa işlemi sonlandır
            }

            Tbl_Urun t = new Tbl_Urun();

            t.UurnAd = txtUrunAd.Text;
            t.Marka = txtMarka.Text;

            // Stok alanını kontrol etmek için TryParse kullanıldı
            if (short.TryParse(txtStok.Text, out short stok))
            {
                t.Stok = stok;
            }
            else
            {
                MessageBox.Show("Geçerli bir stok değeri girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stok değeri geçerli değilse işlemi sonlandır
            }

            // Kategori alanını kontrol etmek için TryParse kullanıldı
            if (int.TryParse(comboBox1.SelectedValue.ToString(), out int kategori))
            {
                t.Kategori = kategori;
            }
            else
            {
                MessageBox.Show("Geçerli bir kategori değeri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Kategori değeri geçerli değilse işlemi sonlandır
            }

            // Fiyat alanını kontrol etmek için TryParse kullanıldı
            if (decimal.TryParse(txtFiyat.Text, out decimal fiyat))
            {
                t.Fiyat = fiyat;
            }
            else
            {
                MessageBox.Show("Geçerli bir fiyat değeri girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Fiyat değeri geçerli değilse işlemi sonlandır
            }

            t.Durum = true;
            db.Tbl_Urun.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ürün Sisteme Kaydedildi");

            // Ekledikten sonra TextBox'ları temizle
            txtUrunAd.Clear();
            txtMarka.Clear();
            txtStok.Clear();
            comboBox1.SelectedIndex = -1; // ComboBox'ı temizle
            txtFiyat.Clear();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtId.Text);
            var urun = db.Tbl_Urun.Find(x);
            db.Tbl_Urun.Remove(urun);
            db.SaveChanges();
            MessageBox.Show("Ürün Silindi");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtId.Text);
            var urun = db.Tbl_Urun.Find(x);
            urun.UurnAd = txtUrunAd.Text;
            urun.Stok = short.Parse(txtStok.Text);
            urun.Marka = txtMarka.Text;
            db.SaveChanges();
            MessageBox.Show("Ürün Güncellendi");
        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            var kategoriler = (from x in db.Tbl_Kategori
                               select new
                               {
                                   x.Id,
                                   x.Ad
                               }
                               ).ToList();
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "AD";
            comboBox1.DataSource = kategoriler;
        }
    }
}
