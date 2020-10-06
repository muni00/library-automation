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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //kitapların listelenişi
        private void favorileri_göster()
        {
            try
            {

                baglantim.Open();
                OleDbDataAdapter favorileri_listele = new OleDbDataAdapter("select ad AS[KİTAP ADI],yad AS[YAZAR ADI],ysoyad AS[YAZAR SOYADI],y_e AS[YAYIN EVİ],sayfa AS[SAYFA SAYISI],tür AS[TÜR],kullanici_id AS[AİT OLDUĞU KULLANICININ ID'Sİ] from kitaplar where( fav = 'evet' AND kullanici_id =" + Form1.kullanicid + " )", baglantim);
                DataSet dshafıza = new DataSet();
                favorileri_listele.Fill(dshafıza);
                dataGridView3.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "favorileri listeleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }


        }
        private void Form11_Load(object sender, EventArgs e)
        {
            favorileri_göster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                OleDbCommand favorileri_güncelle = new OleDbCommand("update  kitaplar set fav = 'hayır' where ( kullanici_id =" + Form1.kullanicid + "AND ad = '" + textBox3.Text + "')", baglantim);
                favorileri_güncelle.ExecuteNonQuery();
                baglantim.Close();
                favorileri_göster();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "favorilerden kaldırma işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }
        }
    }
}
