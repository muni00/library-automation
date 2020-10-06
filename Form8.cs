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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.Oledb.12.0;Data Source =" + Application.StartupPath + "\\Database2.accdb");
        //kullanıcılar listelenerek görülebilmmesi sağlanıyor
        private void kullanicileri_göster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicileri_listele = new OleDbDataAdapter("select id AS[KULLANICI ID],tc AS[TC NO],ad AS[AD],soyad AS[SOYAD],yetki AS[YETKİ],dg AS[DOĞUM GÜNÜ],kullanici_ad AS[KULLANICI ADI],parola AS[PAROLA],engel AS[ENGEL] from kullanici Order By ad ASC", baglantim);
                DataSet dshafıza = new DataSet();
                kullanicileri_listele.Fill(dshafıza);
                dataGridView3.DataSource = dshafıza.Tables[0];
                baglantim.Close();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "kullanıcı engelleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }


        }
        private void Form8_Load(object sender, EventArgs e)
        {
            kullanicileri_göster();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {//kullanıcının engel kolonundaki hayır evet ile güncelleniyor ve artık engelli bir kullanıcı oluyor bunun kontrolu ile girişi engelleniyor
                //bu sayede ne kadar sisteme giremese de yönetici onun bilgilerini görmeye devam ediyor
                baglantim.Open();
                OleDbCommand kullanici_güncelle = new OleDbCommand("update  kullanici set engel = 'evet' where id =" + textBox11.Text + "", baglantim);
                kullanici_güncelle.ExecuteNonQuery();
                baglantim.Close();
                MessageBox.Show("Kullanıcı başarı ile engellendi ", "kullanıcı engelleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                kullanicileri_göster();
            }
            catch (Exception htmsj)
            {
                MessageBox.Show(htmsj.Message, "kullanıcı engelleme işlemleri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();

            }
        }
    }
}
