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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //listeleme işlemi
        private void kitapligi_göster()
        {
            try
            {

                baglantim.Open();
                OleDbDataAdapter kitapligi_listele = new OleDbDataAdapter("select k_id AS[KİTAP ID],ad AS[KİTAP ADI],yad AS[YAZAR ADI],ysoyad AS[YAZAR SOYADI],y_e AS[YAYIN EVİ],sayfa AS[SAYFA SAYISI],tür AS[TÜR] from kitaplık ", baglantim);
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
        
        private void Form5_Load(object sender, EventArgs e)
        {
            kitapligi_göster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.DarkOrange;

            if (textBox2.Text == "")
                label4.ForeColor = Color.Red;
            else
                label4.ForeColor = Color.DarkOrange;

            if (textBox3.Text == "")
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.DarkOrange;

            if (textBox4.Text == "")
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.DarkOrange;

            if (textBox5.Text == "")
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.DarkOrange;

            if (comboBox1.Text == "")
                label8.ForeColor = Color.Red;
            else
                label8.ForeColor = Color.DarkOrange;

            if (textBox6.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.DarkOrange;

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "" && textBox6.Text != "")
            {
                try
                {
                    baglantim.Open();
                    OleDbCommand güncellekomutu = new OleDbCommand("update kitaplık set  ad ='" + textBox1.Text + "',yad='" + textBox2.Text + "',ysoyad='" + textBox3.Text + "',y_e='" + textBox4.Text + "',sayfa=" + textBox5.Text + ",tür='" + comboBox1.Text + "'where k_id =" + textBox6.Text + "", baglantim);
                    güncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    kitapligi_göster();
                    MessageBox.Show("Güncelleme işleminiz başarı ile gerçekleştirilmiştir", "kitap güncelleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception htmsj)
                {

                    MessageBox.Show(htmsj.Message, "kitap güncelleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }
            else
                MessageBox.Show("Lütfen kırmızı alanları doldurunuz", "kitap güncelleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
