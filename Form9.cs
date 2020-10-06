using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace _18010011035
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //kitapların listelenmesi
        private void kitaplari_göster()
        {
            try
            {

                baglantim.Open();
                OleDbDataAdapter kitaplari_listele = new OleDbDataAdapter("select kitap_id AS [KİTAP ID],ad AS[KİTAP ADI],yad AS[YAZAR ADI],ysoyad AS[YAZAR SOYADI],y_e AS[YAYIN EVİ],sayfa AS[SAYFA SAYISI],tür AS[TÜR],kullanici_id AS[AİT OLDUĞU KULLANICININ ID'Sİ] from kitaplar where kullanici_id =" + Form1.kullanicid + "", baglantim);
                DataSet dshafıza = new DataSet();
                kitaplari_listele.Fill(dshafıza);
                dataGridView1.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "kitapları listeleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }


        }
        private void Form9_Load(object sender, EventArgs e)
        {
            kitaplari_göster();
        }
        //sırasıyla butonların secilmesinin kontrolü sağlanıyor ve fav ile durum kolon değerleri o kitap için değişince
        // listelendiği yerler de değişiyor
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox2.Checked == false)
            {
                try
                {
                    baglantim.Open();
                    OleDbCommand favorileri_güncelle = new OleDbCommand("update  kitaplar set fav = 'evet' where ( kullanici_id =" + Form1.kullanicid + "AND kitap_id = " + textBox1.Text + ")", baglantim);
                    favorileri_güncelle.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Seçili kitap başarı ile favorilere eklendi ", "favorilere ekleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    kitaplari_göster();
                }
                catch (Exception htmsj)
                {
                    MessageBox.Show(htmsj.Message, "favorilere ekleme  işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();

                }
            }
            if (checkBox1.Checked == false && checkBox2.Checked == true)
            {
                try
                {
                    baglantim.Open();
                    OleDbCommand okuduklarımı_güncelle = new OleDbCommand("update  kitaplar set durum = 'evet' where ( kullanici_id =" + Form1.kullanicid + "AND kitap_id = " + textBox1.Text + ")", baglantim);
                    okuduklarımı_güncelle.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Seçili kitap başarı ile okuduklarına eklendi ", "okuduklarıma ekleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    kitaplari_göster();
                }
                catch (Exception htmsj)
                {
                    MessageBox.Show(htmsj.Message, "okuduklarıma ekleme  işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();

                }
            }
            if (checkBox1.Checked == true && checkBox2.Checked == true)
            {
                try
                {
                    baglantim.Open();
                    OleDbCommand kitaplarını_güncelle = new OleDbCommand("update  kitaplar set fav = 'evet' , durum = 'evet' where ( kullanici_id =" + Form1.kullanicid + "AND kitap_id = " + textBox1.Text + ")", baglantim);
                    kitaplarını_güncelle.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Seçili kitap başarı ile favorilerine ve okuduklarına  eklendi ", "kullanıcı engelleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    kitaplari_göster();
                }
                catch (Exception htmsj)
                {
                    MessageBox.Show(htmsj.Message, "favorilerime ve okuduklarıma ekleme  işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();

                }
            }

        }
       
    }
}
