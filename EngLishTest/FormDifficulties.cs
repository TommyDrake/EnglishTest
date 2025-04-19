using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnglishTest
{
    public partial class FormDifficulties : Form
    {
        private string language;

        // Constructor nhận tham số ngôn ngữ
        public FormDifficulties(string selectedLanguage)
        {
            InitializeComponent();
            language = selectedLanguage;
        }

        private void button1_Click(object sender, EventArgs e) // EASY
        {
            if (language == "Vietnamese")
            {
                FormTiengVietTest formVN = new FormTiengVietTest(true); // true = Easy
                formVN.Show();
            }
            else
            {
                Form1 formEN = new Form1(); // bạn có thể thêm tham số nếu cần
                formEN.Show();
            }
            this.Hide();
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            if (language == "Vietnamese")
            {
                FormTiengVietTest formVN = new FormTiengVietTest(false); // false = Hard
                formVN.Show();
            }
            else
            {
                Form1 formEN = new Form1(); // bạn có thể mở giao diện tiếng Anh nâng cao nếu có
                formEN.Show();
            }
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.Show();
            this.Close();
        }
    }
}