﻿using System;
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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //kitaplık gösteriliyor
        private void kitaplari_göster()
        {
            try
            {

                baglantim.Open();
                OleDbDataAdapter kitaplari_listele = new OleDbDataAdapter("select ad AS[KİTAP ADI],yad AS[YAZAR ADI],ysoyad AS[YAZAR SOYADI],y_e AS[YAYIN EVİ],sayfa AS[SAYFA SAYISI],tür AS[TÜR],kullanici_id AS[AİT OLDUĞU KULLANICININ ID'Sİ] from kitaplar where (  durum = 'evet' AND kullanici_id =" + Form1.kullanicid + " )", baglantim);
                DataSet dshafıza = new DataSet();
                kitaplari_listele.Fill(dshafıza);
                dataGridView2.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "kiyapları listeleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }


        }
        private void Form10_Load(object sender, EventArgs e)
        {
            kitaplari_göster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                OleDbCommand okuduklarımı_güncelle = new OleDbCommand("update  kitaplar set durum = 'hayır' where ( kullanici_id =" + Form1.kullanicid + "AND ad = '" + textBox2.Text + "')", baglantim);
                okuduklarımı_güncelle.ExecuteNonQuery();
                baglantim.Close();
                kitaplari_göster();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "okuduklarımdan kaldırma işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }
        }
    }
}
