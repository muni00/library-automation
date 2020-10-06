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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            customize_desing();
        }
        //panellerin visible durumlarını false yaptık
        private void customize_desing()
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }
        // menudeki alt alanların gizlenmesi için kısa bi fonksiyon
        private void hide_submenu()
        {
            if (panel2.Visible == true)
                panel2.Visible = false;
            if (panel3.Visible == true)
                panel3.Visible = false;
            if (panel4.Visible == true)
                panel4.Visible = false;
            if (panel6.Visible == true)
                panel6.Visible = false;
            if (panel7.Visible == true)
                panel7.Visible = false;
        }
        //tıklanan seçeneğin alt alanlarını açmak için bir fonksiyon
        private void show_submanu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hide_submenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;

        }
        private Form activeform = null;
        //childform açmak için
        private void openchildform(Form childform)
        {
            if (activeform != null)
                activeform.Close();
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            panelchild.Controls.Add(childform);
            panelchild.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
        //kitap ekle için


        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.yetki == "yonetici")
            {
                show_submanu(panel2);
            }
            else
                MessageBox.Show("Yönetici olmadığınızdan bu işlemi gerçekleştiremezsiniz", "üye kullanım uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form3());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form5());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form7());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form6());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Form1.yetki == "yonetici")
            {
                show_submanu(panel3);
            }
            else
                MessageBox.Show("Yönetici olmadığınızdan bu işlemi gerçekleştiremezsiniz", "üye kullanım uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form4());
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (Form1.yetki == "yonetici")
            {
                show_submanu(panel4);
            }
            else
                MessageBox.Show("Yönetici olmadığınızdan bu işlemi gerçekleştiremezsiniz", "üye kullanım uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button12_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form8());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Form1.yetki == "uye")
            {
                show_submanu(panel7);
            }
            else
                MessageBox.Show("yönetici olduğunuzdan üye işlemleri gerçekleştiremezsiniz", "yönetici kullanım uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form9());
        }

        private void button16_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form12());
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (Form1.yetki == "uye")
            {
                show_submanu(panel6);
            }
            else
                MessageBox.Show("yönetici olduğunuzdan üye işlemleri gerçekleştiremezsiniz", "yönetici kullanım uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form11());
        }

        private void button6_Click(object sender, EventArgs e)
        {

            hide_submenu();
            openchildform(new Form10());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            hide_submenu();
            openchildform(new Form13());
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //resim koyma işlemi 
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tc + ".jpg");
            }
            catch
            {

                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\üyeresim.png");
            }

            label1.Text = Form1.ad.ToUpper(); ;//kullanıcının isim soyisim ve id sini koyması için
            label2.Text = Form1.soyad.ToUpper();
            label3.Text = "ID = " + Form1.kullanicid.ToUpper();
            label4.Text = Form1.kullanici_ad.ToUpper();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statusBar1.Panels[0].Text = "Saat :" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;//güncel saatin statusbar da gösterilmesi

        }
    }
}
