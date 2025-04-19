using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnglishTest
{
    public partial class FormTiengVietTest : Form
    {
        private List<Question> questions = new List<Question>();
        private Dictionary<int, int> userAnswers = new Dictionary<int, int>();
        private int currentQuestionIndex = 0;
        private bool isEasy; // dùng để chọn nhóm câu hỏi

        public FormTiengVietTest(bool isEasyLevel)
        {
            InitializeComponent();
            isEasy = isEasyLevel;

            // LABEL TIÊU ĐỀ
            label1.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(30, 30, 60);
            label1.TextAlign = ContentAlignment.MiddleLeft;
            label1.Text = "Kiểm tra Tiếng Việt";

            // LISTVIEW
            listView1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            listView1.BackColor = Color.WhiteSmoke;
            listView1.ForeColor = Color.FromArgb(40, 40, 40);
            listView1.BorderStyle = BorderStyle.FixedSingle;
            listView1.FullRowSelect = true;

            // GROUPBOX
            groupBox1.Text = "Câu hỏi";
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.ForeColor = Color.FromArgb(30, 30, 30);
            groupBox1.BackColor = Color.WhiteSmoke;
            groupBox1.FlatStyle = FlatStyle.Flat;

            // LABEL CÂU HỎI
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(20, 20, 80);

            // RADIO BUTTONS
            RadioButton[] radios = { radioButton1, radioButton2, radioButton3, radioButton4 };
            foreach (var rb in radios)
            {
                rb.Font = new Font("Segoe UI", 11F);
                rb.ForeColor = Color.FromArgb(30, 30, 30);
                rb.AutoSize = true;
            }

            // BUTTONS
            btnNext.Text = "Next";
            btnBack.Text = "Back";
            btnSubmit.Text = "Submit";

            Button[] buttons = { btnNext, btnBack, btnSubmit };
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

            // PROGRESS BAR
            progressBar1.ForeColor = Color.MediumSeaGreen;
            progressBar1.Style = ProgressBarStyle.Continuous;

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions = LoadVietnameseQuestions(isEasy);
            listView1.Items.Clear();
            for (int i = 0; i < questions.Count; i++)
            {
                listView1.Items.Add($"Câu {i + 1}");
            }

            listView1.View = View.List;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = questions.Count;
            progressBar1.Value = 0;

            DisplayQuestion(currentQuestionIndex);
            btnBack.Enabled = currentQuestionIndex > 0;
            btnNext.Enabled = currentQuestionIndex < questions.Count - 1;

        }

        private List<Question> LoadVietnameseQuestions(bool isEasy)
        {
            var list = new List<Question>();
            if (isEasy)
            {
                list.Add(new Question { Content = "Thủ đô của Việt Nam là gì?", Answers = new[] { "Huế", "Hà Nội", "Đà Lạt", "TP.HCM" }, CorrectAnswerIndex = 1 });
                list.Add(new Question { Content = "Quốc khánh Việt Nam là ngày nào?", Answers = new[] { "1/5", "2/9", "30/4", "20/11" }, CorrectAnswerIndex = 1 });
                list.Add(new Question { Content = "Màu cờ Việt Nam?", Answers = new[] { "Xanh - Trắng", "Vàng - Đỏ", "Đỏ - Xanh", "Vàng - Xanh" }, CorrectAnswerIndex = 1 });
                list.Add(new Question { Content = "Tiếng Việt có bao nhiêu dấu?", Answers = new[] { "5", "6", "7", "8" }, CorrectAnswerIndex = 2 });
                list.Add(new Question { Content = "Chữ cái nào không có trong tiếng Việt?", Answers = new[] { "F", "K", "L", "M" }, CorrectAnswerIndex = 0 });
            }
            else
            {
                list.Add(new Question { Content = "Tác giả 'Truyện Kiều' là ai?", Answers = new[] { "Nguyễn Trãi", "Nguyễn Du", "Hồ Xuân Hương", "Tản Đà" }, CorrectAnswerIndex = 1 });
                list.Add(new Question { Content = "Câu nào có biện pháp nhân hóa?", Answers = new[] { "Gió thổi hiu hiu", "Trăng cười dịu dàng", "Nước chảy róc rách", "Lá rơi nhẹ nhàng" }, CorrectAnswerIndex = 1 });
                list.Add(new Question { Content = "Từ láy nào sau đây là láy hoàn toàn?", Answers = new[] { "Lấp lánh", "Lung linh", "Lóng lánh", "Long lanh" }, CorrectAnswerIndex = 1 });
                list.Add(new Question { Content = "Từ nào là từ ghép đẳng lập?", Answers = new[] { "Máy bay", "Bàn ghế", "Nhà cửa", "Xe đạp" }, CorrectAnswerIndex = 1 });
                list.Add(new Question { Content = "Tác phẩm nào không phải văn học trung đại?", Answers = new[] { "Tắt đèn", "Truyện Kiều", "Chinh phụ ngâm", "Lục Vân Tiên" }, CorrectAnswerIndex = 0 });
            }
            return list;
        }

        private void DisplayQuestion(int index)
        {
            if (index < 0 || index >= questions.Count) return;

            var q = questions[index];
            label2.Text = q.Content;
            radioButton1.Text = q.Answers[0];
            radioButton2.Text = q.Answers[1];
            radioButton3.Text = q.Answers[2];
            radioButton4.Text = q.Answers[3];

            // Khi hiển thị câu hỏi, phải tạm ngắt sự kiện CheckedChanged nếu cần
radioButton1.CheckedChanged -= radioButton_CheckedChanged;
radioButton2.CheckedChanged -= radioButton_CheckedChanged;
radioButton3.CheckedChanged -= radioButton_CheckedChanged;
radioButton4.CheckedChanged -= radioButton_CheckedChanged;

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

            // Gắn lại sự kiện
            radioButton1.CheckedChanged += radioButton_CheckedChanged;
            radioButton2.CheckedChanged += radioButton_CheckedChanged;
            radioButton3.CheckedChanged += radioButton_CheckedChanged;
            radioButton4.CheckedChanged += radioButton_CheckedChanged;

            listView1.Items[index].Selected = true;
            listView1.Select();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                currentQuestionIndex = listView1.SelectedItems[0].Index;
                DisplayQuestion(currentQuestionIndex);
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            int index = listView1.SelectedItems[0].Index;

            int selectedAnswer = -1;
            if (radioButton1.Checked) selectedAnswer = 0;
            else if (radioButton2.Checked) selectedAnswer = 1;
            else if (radioButton3.Checked) selectedAnswer = 2;
            else if (radioButton4.Checked) selectedAnswer = 3;

            if (selectedAnswer != -1)
            {
                userAnswers[index] = selectedAnswer;
            }
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;

                progressBar1.Value = Math.Max(progressBar1.Value - 1, progressBar1.Minimum);

                DisplayQuestion(currentQuestionIndex);
            }

            btnBack.Enabled = currentQuestionIndex > 0;
            btnNext.Enabled = currentQuestionIndex < questions.Count - 1;
        }




        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questions.Count - 1)
            {
                currentQuestionIndex++;

                progressBar1.Value = Math.Min(progressBar1.Value + 1, progressBar1.Maximum);

                DisplayQuestion(currentQuestionIndex);
            }

            btnBack.Enabled = currentQuestionIndex > 0;
            btnNext.Enabled = currentQuestionIndex < questions.Count - 1;
        }




        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int correctCount = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (userAnswers.ContainsKey(i) && userAnswers[i] == questions[i].CorrectAnswerIndex)
                {
                    correctCount++;
                }
            }

            MessageBox.Show($"Bạn trả lời đúng {correctCount}/{questions.Count} câu.\nĐiểm: {(correctCount * 10.0 / questions.Count):0.0}/10", "Kết quả");
            Application.Exit();
        }



        private void FormTiengVietTest_Load(object sender, EventArgs e)
        {

        }
    }
}