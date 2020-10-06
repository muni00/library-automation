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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //veritabanı bağlantısı
        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");

        //formlararası değişken tanımlama
        public static string tc, yetki, parola, kullanicid, ad, soyad, kullanici_ad, engel, dg, tarih;

        private void timer2_Tick(object sender, EventArgs e)
        {
            panel2.Left += 4;//buton kaydırıldı ve texti değiştirildi
            panel3.Left += 4;
            if (panel2.Left > 12)
            {
                timer2.Stop();
                button1.Text = "ÜYELİK";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Left -= 6;//kayma sağlandı
            panel3.Left -= 6;
            if (panel3.Left <= 12)
            {
                timer1.Stop();
                button1.Text = "OTURUM AÇ";//butonun texti değiştirildi
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;//buton bir enter tuşuyla çalışır halde
            radioButton4.Checked = true;//kolaylık açısında üye radiobuttonu seçili geliyor
            tarih = DateTime.Now.ToLongDateString();//tarih kontrolü için atama
        }

        //kaydı bulduğunu kontrol etmek için bir değişken atadık
        bool varyok = false;
        private void button2_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanici", baglantim);//kullanıcılar select sorgusu
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();//kayıt okuma da tutuluyorlar
            while (kayitokuma.Read())//okunan değerler bitene kadar bir while döngüsü
            {
                if (radioButton4.Checked == true)//üye girişi için
                {
                    if (kayitokuma["tc"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["engel"].ToString() == "hayır" && kayitokuma["yetki"].ToString() == "uye")
                    { //şartlar sağlandığında kayıttaki bilgiler değişkenlere aktarılıyor ve yönlendirme yapılıyor 
                        varyok = true;
                        kullanicid = kayitokuma.GetValue(0).ToString();
                        tc = kayitokuma.GetValue(1).ToString();
                        ad = kayitokuma.GetValue(2).ToString();
                        soyad = kayitokuma.GetValue(3).ToString();
                        yetki = kayitokuma.GetValue(4).ToString();
                        dg = kayitokuma.GetValue(5).ToString();
                        kullanici_ad = kayitokuma.GetValue(6).ToString();
                        parola = kayitokuma.GetValue(7).ToString();
                        engel = kayitokuma.GetValue(8).ToString();
                        this.Hide();
                        Form2 frm2 = new Form2();
                        frm2.Show();
                        break;
                    }

                }
                if (radioButton3.Checked == true)
                {
                    if (kayitokuma["tc"].ToString() == textBox1.Text && kayitokuma["parola"].ToString() == textBox2.Text && kayitokuma["engel"].ToString() == "hayır" && kayitokuma["yetki"].ToString() == "yonetici")
                    {
                        //şartlar sağlandığında kayıttaki bilgiler değişkenlere aktarılıyor ve yönlendirme yapılıyor 
                        varyok = true;
                        kullanicid = kayitokuma.GetValue(0).ToString();
                        tc = kayitokuma.GetValue(1).ToString();
                        ad = kayitokuma.GetValue(2).ToString();
                        soyad = kayitokuma.GetValue(3).ToString();
                        yetki = kayitokuma.GetValue(4).ToString();
                        dg = kayitokuma.GetValue(5).ToString();
                        kullanici_ad = kayitokuma.GetValue(6).ToString();
                        parola = kayitokuma.GetValue(7).ToString();
                        engel = kayitokuma.GetValue(8).ToString();
                        this.Hide();
                        Form2 frm2 = new Form2();
                        frm2.Show();
                        break;
                    }
                }
            }
            if (varyok == false)
            {
                MessageBox.Show("ARANAN KULLANICI BULUNAMADI", "üye giriş işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "ÜYELİK")//buton isimleri sayfa kaydığında değişiyor bu farkı kullanarak timerler başlatıldı
            {
                timer1.Start();
            }
            else if (button1.Text == "OTURUM AÇ")
            {
                timer2.Start();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool kayıtkontrol = false;
            string yetki = "uye";
            string engel = "hayır";

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanici where tc ='" + textBox3.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();//kayıt okuma
            while (kayitokuma.Read())
            {
                kayıtkontrol = true;
            }
            baglantim.Close();

            if (kayıtkontrol == false)
            {
                //girilen veri kontrolleri

                if (textBox3.Text.Length != 11)
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.DarkOrange;

                if (textBox4.Text == "")
                    label4.ForeColor = Color.Red;
                else
                    label4.ForeColor = Color.DarkOrange;

                if (textBox5.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.DarkOrange;

                if (dateTimePicker1.Text == tarih)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.DarkOrange;

                if (textBox6.Text == "")
                    label8.ForeColor = Color.Red;
                else
                    label8.ForeColor = Color.DarkOrange;

                if (textBox7.Text == "")
                    label9.ForeColor = Color.Red;
                else
                    label9.ForeColor = Color.DarkOrange;

                if (dateTimePicker1.Text != tarih && textBox3.Text.Length == 11 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
                {
                    try
                    {//şartlar sağlanınca ekleme yapılır
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanici(tc,ad,soyad,yetki,dg,kullanici_ad,parola,engel) values ('" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + yetki + "','" + dateTimePicker1.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + engel + "')", baglantim);

                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Kayıt işleminiz başarı ile gerçekleştirilmiştir", "üye kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form2 frm2 = new Form2();//forma geçiş
                        frm2.Show();
                    }
                    catch (Exception htmsj)
                    {

                        MessageBox.Show(htmsj.Message, "üye kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }
                }
                else
                    MessageBox.Show("Lütfen kırmızı alanları doldurunuz", "üye kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("Girilen tc kimlik numarası ile daha önceden kayıt yapılmış", "üye kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}

