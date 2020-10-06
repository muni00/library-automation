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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        string kitap_id, ad, yad, ysoyad, y_e, sayfa, tür;

       

        bool varyok = false;
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
        private void Form12_Load(object sender, EventArgs e)
        {
            kitapligi_göster();
        } 
        private void button4_Click(object sender, EventArgs e)
        {
            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kitaplık", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            //kitaplar taranıyor
            while (kayitokuma.Read())
            {
                if (kayitokuma["k_id"].ToString() == textBox6.Text)
                {
                    try
                    {
                        varyok = true;
                        kitap_id = kayitokuma.GetValue(0).ToString();
                        ad = kayitokuma.GetValue(1).ToString();
                        yad = kayitokuma.GetValue(2).ToString();
                        ysoyad = kayitokuma.GetValue(3).ToString();
                        y_e = kayitokuma.GetValue(4).ToString();
                        sayfa = kayitokuma.GetValue(5).ToString();
                        tür = kayitokuma.GetValue(6).ToString();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kitaplar (ad,yad,ysoyad,y_e,sayfa,tür,fav,durum,kullanici_id) values ('" + ad + "','" + yad + "','" + ysoyad + "','" + y_e + "'," + sayfa + ",'" + tür + "','" + "hayır" + "','" + "hayır" + "'," + Form1.kullanicid + ")", baglantim);
                        //ekleme yapıldı
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Kayıt işleminiz başarı ile gerçekleştirilmiştir", "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        break;
                    }

                    catch (Exception htmsj)
                    {
                        MessageBox.Show(htmsj.Message, "kitap kayıt  işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        baglantim.Close();

                    }

                }

            }
            if (varyok == false)
            {
                MessageBox.Show("Aradığınız kayıt bulunamamaktadır", "kitap kayıt işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
            }
        }
    }
}
