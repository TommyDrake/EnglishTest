using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EnglishTest
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // === LABEL TIÊU ĐỀ ===
            label1.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(30, 30, 60);
            label1.TextAlign = ContentAlignment.MiddleLeft;

            // === LISTVIEW CÂU HỎI ===
            listView1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            listView1.BackColor = Color.WhiteSmoke;
            listView1.ForeColor = Color.FromArgb(40, 40, 40);
            listView1.BorderStyle = BorderStyle.FixedSingle;
            listView1.FullRowSelect = true;

            // === GROUPBOX CHỨA CÂU HỎI ===
            groupBox1.Text = "Question Panel";
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(30, 30, 30);
            groupBox1.BackColor = Color.WhiteSmoke;
            groupBox1.FlatStyle = FlatStyle.Flat;

            // === LABEL CÂU HỎI ===
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(20, 20, 80);

            // === RADIO BUTTONS ===
            RadioButton[] radios = { radioButton1, radioButton2, radioButton3, radioButton4 };
            foreach (var rb in radios)
            {
                rb.Font = new Font("Segoe UI", 11F);
                rb.ForeColor = Color.FromArgb(30, 30, 30);
                rb.AutoSize = true;
            }

            // === BUTTONS ===
            button1.Text = "NEXT";
            button2.Text = "BACK";
            button3.Text = "SUBMIT";

            Button[] buttons = { button1, button2, button3 };
            Color[] colors = { Color.SteelBlue, Color.Gray, Color.SeaGreen };

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                buttons[i].FlatStyle = FlatStyle.Flat;
                buttons[i].BackColor = colors[i];
                buttons[i].ForeColor = Color.White;
                buttons[i].FlatAppearance.BorderSize = 0;
                buttons[i].Size = new Size(100, 36);
            }

            // === PROGRESS BAR ===
            progressBar1.ForeColor = Color.MediumSeaGreen;
            progressBar1.Style = ProgressBarStyle.Continuous;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {

                currentQuestionIndex = listView1.SelectedItems[0].Index;
                DisplayQuestion(currentQuestionIndex);

            }
        }

        private void DisplayQuestion(int index)
        {
            if (index < 0 || index >= questions.Count) return;

            var q = questions[index];

            label3.Text = q.Content;
            radioButton1.Text = q.Answers[0];
            radioButton2.Text = q.Answers[1];
            radioButton3.Text = q.Answers[2];
            radioButton4.Text = q.Answers[3];

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            if (userAnswers.ContainsKey(index))
            {
                switch (userAnswers[index])
                {
                    case 0: radioButton1.Checked = true; break;
                    case 1: radioButton2.Checked = true; break;
                    case 2: radioButton3.Checked = true; break;
                    case 3: radioButton4.Checked = true; break;
                }
            }

            listView1.Items[index].Selected = true;
            listView1.Select();
        }



        List<Question> questions = new List<Question>();
        Dictionary<int, int> userAnswers = new Dictionary<int, int>(); // Lưu câu nào người dùng đã chọn
        int currentQuestionIndex = 0;

        private void Form4_Load(object sender, EventArgs e)
        {
            //           // questions = new List<Question>()
            //{
            //       new Question {
            //        Content = "Thủ đô của Việt Nam ở đâu ?", 
            //        Answers = new[] { "Đà Nẵng", "Hà Nội", "Hồ Chí Minh", "Vũng Tàu" },
            //        CorrectAnswerIndex = 1
            //    },
            //    new Question {
            //        Content = "30/4 là ngày gì ?",
            //        Answers = new[] { "Tự do", "Nhà giáo VN", "Phụ nữ", "Giải Phóng Miền Nam Thống nhất đất nước" },
            //        CorrectAnswerIndex = 3
            //    },
            //    new Question {
            //        Content = "Which planet is red?",
            //        Answers = new[] { "Earth", "Venus", "Mars", "Jupiter" },
            //        CorrectAnswerIndex = 2
            //    },
            //    new Question {
            //        Content = "Which planet is blue?",
            //        Answers = new[] { "blue", "Venus", "Mars", "Jupiter" },
            //        CorrectAnswerIndex = 0
            //    },
            //    new Question {
            //        Content = "Which planet is dark?",
            //        Answers = new[] { "black hole", "Venus", "Mars", "Jupiter" },
            //        CorrectAnswerIndex = 0
            //    }
            //}; 
            questions = LoadQuestionsFromDatabase();
            listView1.Items.Clear();
            for (int i = 0; i < questions.Count; i++)
            {
                listView1.Items.Add($"Question {i + 1}");
            }

            listView1.View = View.List;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = questions.Count;
            progressBar1.Value = 0;

            DisplayQuestion(currentQuestionIndex);
            button2.Enabled = false;
            button1.Enabled = questions.Count > 1;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            HandleAnswerSelection();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            HandleAnswerSelection();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            HandleAnswerSelection();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            HandleAnswerSelection();
        }




        private void HandleAnswerSelection()
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            int index = listView1.SelectedItems[0].Index;

            int selectedAnswer = -1;
            if (radioButton1.Checked) selectedAnswer = 0;
            if (radioButton2.Checked) selectedAnswer = 1;
            if (radioButton3.Checked) selectedAnswer = 2;
            if (radioButton4.Checked) selectedAnswer = 3;

            if (selectedAnswer != -1)
            {
                bool isNewSelection = !userAnswers.ContainsKey(index);
                userAnswers[index] = selectedAnswer;

                if (isNewSelection)
                {
                    progressBar1.Value++;
                }
            }
        }

       
        private void button1_Click(object sender, EventArgs e) // NEXT
        {
            if (currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;
                DisplayQuestion(currentQuestionIndex);
            }

            // Cập nhật trạng thái nút
            button1.Enabled = currentQuestionIndex < questions.Count - 1;
            button2.Enabled = currentQuestionIndex > 0;
        }


        private void button2_Click(object sender, EventArgs e) // BACK
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                DisplayQuestion(currentQuestionIndex);
            }

            // Cập nhật trạng thái nút
            button2.Enabled = currentQuestionIndex > 0;
            button1.Enabled = currentQuestionIndex < questions.Count - 1;
        }




        private void button3_Click(object sender, EventArgs e)
        {
            int correctCount = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                if (userAnswers.ContainsKey(i))
                {
                    if (userAnswers[i] == questions[i].CorrectAnswerIndex)
                    {
                        correctCount++;
                    }
                }
            }

            MessageBox.Show($"Bạn trả lời đúng {correctCount}/{questions.Count} câu.\nĐiểm: {(correctCount * 10.0 / questions.Count):0.0}/10", "Kết quả");
            Application.Exit();
        }
        private void UpdateNavButtons()
        {
            button1.Enabled = currentQuestionIndex < questions.Count - 1;
            button2.Enabled = currentQuestionIndex == 0;
        }

        private List<Question> LoadQuestionsFromDatabase()
        {
            var questions = new List<Question>();
            string connectionString = @"Data Source=PHTTHANH203;Initial Catalog=DB_CauHoi;Integrated Security=True";


            string query = "SELECT Content, Answer1, Answer2, Answer3, Answer4, CorrectAnswerIndex FROM Questions";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var question = new Question
                    {
                        Content = reader.GetString(0),
                        Answers = new[]
                        {
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4)
                },
                        CorrectAnswerIndex = reader.GetInt32(5)
                    };
                    questions.Add(question);
                }
            }

            return questions;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }


}
