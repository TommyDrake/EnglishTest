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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuanLyCauHoi quanly = new QuanLyCauHoi();
            quanly.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormDifficulties formDiff = new FormDifficulties("English");
            formDiff.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormDifficulties formDiff = new FormDifficulties("Vietnamese");
            formDiff.Show();
            this.Hide();

        }
    }
}
