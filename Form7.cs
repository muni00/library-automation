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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }


        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //kitapların lstelenmesi
        private void kitapligi_göster()
        {
            try
            {

                baglantim.Open();
                OleDbDataAdapter kitapligi_listele = new OleDbDataAdapter("select ad AS[KİTAP ADI],yad AS[YAZAR ADI],ysoyad AS[YAZAR SOYADI],y_e AS[YAYIN EVİ],sayfa AS[SAYFA SAYISI],tür AS[TÜR] from kitaplık ", baglantim);
                DataSet dshafıza = new DataSet();
                kitapligi_listele.Fill(dshafıza);
                dataGridView1.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "kitaplığı listeleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }


        }
        private void Form7_Load(object sender, EventArgs e)
        {
            kitapligi_göster();
        }
    }
}
