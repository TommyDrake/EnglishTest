using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EnglishTest
{
    public partial class QuanLyCauHoi : Form
    {
        string connectionString = @"Data Source=PHTTHANH203;Initial Catalog=DB_CauHoi;Integrated Security=True";
        public QuanLyCauHoi()
        {
            InitializeComponent();
        }

        private void QuanLyCauHoi_Load(object sender, EventArgs e)
        {
            LoadAllQuestionsToListView();
        }
        private void LoadQuestions()
        {
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            string query = @"INSERT INTO Questions (Content, Answer1, Answer2, Answer3, Answer4, CorrectAnswerIndex) 
                     VALUES (@Content, @A1, @A2, @A3, @A4, @Correct)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Content", textBox2.Text);
                cmd.Parameters.AddWithValue("@A1", textBox3.Text);
                cmd.Parameters.AddWithValue("@A2", textBox4.Text);
                cmd.Parameters.AddWithValue("@A3", textBox5.Text);
                cmd.Parameters.AddWithValue("@A4", textBox6.Text);
                cmd.Parameters.AddWithValue("@Correct", int.Parse(textBox7.Text));

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Thêm câu hỏi thành công!");
            LoadAllQuestionsToListView();

        }

        private void LoadQuestionByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Content, Answer1, Answer2, Answer3, Answer4, CorrectAnswerIndex FROM Questions WHERE Id=@ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox2.Text = reader.GetString(0);
                    textBox3.Text = reader.GetString(1);
                    textBox4.Text = reader.GetString(2);
                    textBox5.Text = reader.GetString(3);
                    textBox6.Text = reader.GetString(4);
                    textBox7.Text = reader.GetInt32(5).ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy câu hỏi với ID này.");
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID cần sửa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Questions SET Content=@Content, Answer1=@A1, Answer2=@A2, Answer3=@A3, Answer4=@A4, CorrectAnswerIndex=@Correct WHERE Id=@ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Content", textBox2.Text);
                cmd.Parameters.AddWithValue("@A1", textBox3.Text);
                cmd.Parameters.AddWithValue("@A2", textBox4.Text);
                cmd.Parameters.AddWithValue("@A3", textBox5.Text);
                cmd.Parameters.AddWithValue("@A4", textBox6.Text);
                cmd.Parameters.AddWithValue("@Correct", int.Parse(textBox7.Text));

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                MessageBox.Show(rows > 0 ? "Cập nhật thành công!" : "Không tìm thấy câu hỏi để sửa.");
                LoadAllQuestionsToListView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ID để xóa.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Questions WHERE Id=@ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                MessageBox.Show(rows > 0 ? "Xóa thành công!" : "Không tìm thấy ID để xóa.");
                LoadAllQuestionsToListView();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadQuestionByID(int.Parse(textBox1.Text));
        }

        private void LoadAllQuestionsToListView()
        {
            listBox1.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Content, CorrectAnswerIndex FROM Questions";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string display = $"{reader["Id"]}: {reader["Content"]} (Ans {reader["CorrectAnswerIndex"]})";
                    listBox1.Items.Add(display);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

