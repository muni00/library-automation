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

namespace _18010011035
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //kullanıcıları listelemek için
        private void kullanicileri_göster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicileri_listele = new OleDbDataAdapter("select id AS[KULLANICI ID],tc AS[TC NO],ad AS[AD],soyad AS[SOYAD],yetki AS[YETKİ],dg AS[DOĞUM GÜNÜ],kullanici_ad AS[KULLANICI ADI],parola AS[PAROLA],engel AS[ENGEL] from kullanici Order By ad ASC", baglantim);
                DataSet dshafıza = new DataSet();
                kullanicileri_listele.Fill(dshafıza);
                dataGridView2.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "yönetici kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }


        }
        private void Form4_Load(object sender, EventArgs e)
        {
            kullanicileri_göster();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "kullanıcı resmi seçiniz.";
            resimsec.Filter = "JPG Dosyalar (*.jpg) | *jpg";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());


                if (pictureBox2.Image.Width > pictureBox2.Width && pictureBox2.Image.Height > pictureBox2.Height)//dosyayı sığdırmak için
                {
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            bool kayıtkontrol = false;
            string yetki = "yonetici";
            string engel = "hayır";

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanici where tc ='" + textBox6.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayıtkontrol = true;
            }
            baglantim.Close();

            if (kayıtkontrol == false)
            {
                if (pictureBox2.Image == null)
                    button1.ForeColor = Color.Red;
                else
                    button1.ForeColor = Color.DarkOrange;

                if (textBox6.Text.Length != 11)
                    label9.ForeColor = Color.Red;
                else
                    label9.ForeColor = Color.DarkOrange;

                if (textBox7.Text == "")
                    label10.ForeColor = Color.Red;
                else
                    label10.ForeColor = Color.DarkOrange;

                if (textBox8.Text == "")
                    label11.ForeColor = Color.Red;
                else
                    label11.ForeColor = Color.DarkOrange;

                if (dateTimePicker1.Text == Form1.tarih)
                    label12.ForeColor = Color.Red;
                else
                    label12.ForeColor = Color.DarkOrange;

                if (textBox9.Text == "")
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.DarkOrange;

                if (textBox10.Text == "")
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.DarkOrange;

                if (dateTimePicker1.Text != Form1.tarih && pictureBox2.Image != null && textBox6.Text.Length == 11 && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "")
                {
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanici(tc,ad,soyad,yetki,dg,kullanici_ad,parola,engel) values ('" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + yetki + "','" + dateTimePicker1.Text + "','" + textBox9.Text + "','" + textBox10.Text + "','" + engel + "')", baglantim);

                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        kullanicileri_göster();
                        //kullanıcı resimlerini kayıt işlemleri
                        if (!Directory.Exists(Application.StartupPath + "\\kullaniciresimler"))
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\kullaniciresimler");
                            pictureBox2.Image.Save(Application.StartupPath + "\\kullaniciresimler\\" + textBox6.Text + ".jpg");
                            MessageBox.Show("Yönetici kayıt işleminiz başarıyla sonuçlandı..", "yönetici kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        else
                        {
                            pictureBox2.Image.Save(Application.StartupPath + "\\kullaniciresimler\\" + textBox6.Text + ".jpg");
                            MessageBox.Show("kaydolma işleminiz başarıyla sonuçlandı..", "yönetici kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                    }
                    catch (Exception htmsj)
                    {

                        MessageBox.Show(htmsj.Message, "yönetici kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }
                }
                else
                    MessageBox.Show("Lütfen kırmızı alanları doldurunuz", "yönetici kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("Girilen tc kimlik numarası ile daha önceden kayıt yapılmış", "yönetici kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
