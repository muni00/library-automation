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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //listeleme işlemi
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                OleDbCommand kitapsil = new OleDbCommand("delete from  kitaplar  where ( kitap_id =" + textBox1.Text + "AND  kullanici_id = " + Form1.kullanicid + ")", baglantim);
                kitapsil.ExecuteNonQuery();
                baglantim.Close();
                kitaplari_göster();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "kitap silme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            kitaplari_göster();
        }
    }
}
