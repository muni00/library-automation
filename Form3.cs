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
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace _18010011035
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");

        //toplu ekleme için değişkenler
        string filelimiter = ",";
        string line = " ";
        Int32 counter = 0;
        //kitaplığı listelemek için bir fonksiyon
        private void kitapligi_göster()
        {
            try
            {

                baglantim.Open();
                OleDbDataAdapter kitapligi_listele = new OleDbDataAdapter("select ad AS[KİTAP ADI],yad AS[YAZAR ADI],ysoyad AS[YAZAR SOYADI],y_e AS[YAYIN EVİ],sayfa AS[SAYFA SAYISI],tür AS[TÜR] from kitaplık ", baglantim);
                DataSet dshafıza = new DataSet();
                kitapligi_listele.Fill(dshafıza);//geçici bi şekilde kayıtlar tutulur
                dataGridView1.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "kitaplığı listeleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }


        }
        private void Form3_Load(object sender, EventArgs e)
        {
            kitapligi_göster();
            string sConnectionString = ConfigurationManager.ConnectionStrings["DYNAMINDEX"].ConnectionString;
            SqlConnection scSqlConnection = new SqlConnection(sConnectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool kayıtkontrol = false;//kaydı bulunca true yapıp bulunduğunu anlamak için 

            if (textBox6.Text != "")//eğer bir dosya seçildiyse toplu ekleme yapılır
            {
                try
                {
                    System.IO.StreamReader kaynakdosya = new StreamReader(textBox6.Text.ToString());//okunacak dosya 
                    baglantim.Open();
                    while ((line = kaynakdosya.ReadLine()) != null)//dosyanın sonuna gelip gelmediğinin kontrolu
                    {
                        if (counter > 0)
                        {
                            OleDbCommand eklekomutu = new OleDbCommand("insert into kitaplık (ad,yad,ysoyad,y_e,sayfa,tür) values('" + line.Replace(filelimiter, "','") + "')", baglantim);
                            eklekomutu.ExecuteNonQuery();
                        }
                        counter++;
                    }
                    counter = 0;
                    kaynakdosya.Close();
                    baglantim.Close();
                    kitapligi_göster();
                    MessageBox.Show("Kayıt işleminiz başarı ile gerçekleştirilmiştir", "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception htmsj)
                {
                    MessageBox.Show(htmsj.Message, "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();

                }
            }
            else
            {
                //sıradan ekleme işlemi:şartlar kontrol edilir ve ekleme gerçekleşir ya da gerçekleşmez
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kitaplık where ad ='" + textBox3.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayıtkontrol = true;
                }
                baglantim.Close();

                if (kayıtkontrol == false)
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

                    if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "")
                    {
                        try
                        {
                            baglantim.Open();
                            OleDbCommand eklekomutu = new OleDbCommand("insert into kitaplık (ad,yad,ysoyad,y_e,sayfa,tür) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "')", baglantim);

                            eklekomutu.ExecuteNonQuery();
                            baglantim.Close();
                            kitapligi_göster();
                            MessageBox.Show("Kayıt işleminiz başarı ile gerçekleştirilmiştir", "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception htmsj)
                        {

                            MessageBox.Show(htmsj.Message, "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            baglantim.Close();
                        }
                    }
                    else
                        MessageBox.Show("Lütfen eklemeye devam etmek istiyorsanız kırmızı alanları doldurunuz", "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                    MessageBox.Show("Girilen ad ile daha önceden kayıt yapılmış", "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Filter = "txt files (*.txt)|*.txt|All files (*.*) |*.*";
                opendialog.Title = "Bir txt dosyası seçiniz.";
                opendialog.InitialDirectory = Application.StartupPath;
                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    textBox6.Text = opendialog.FileName.ToString();
                }
                opendialog.Dispose();

            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "dosya seçme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
