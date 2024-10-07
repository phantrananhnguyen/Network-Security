using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesar
{
    public partial class Form1 : Form
    {
        // Biến để lưu trữ giá trị key hiện tại khi brute-force
        private int currentKey = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            // Xóa dữ liệu cũ trong các textbox
            txtBoxC.Clear();

            // Lấy dữ liệu từ các textbox
            string plaintext = txtBoxP.Text;
            string keyText = txtBoxK.Text;

            // Kiểm tra nếu txtBoxP hoặc txtBoxK trống thì yêu cầu nhập dữ liệu
            if (string.IsNullOrEmpty(plaintext) || string.IsNullOrEmpty(keyText))
            {
                MessageBox.Show("Vui lòng nhập Planitext và Key!!!");
                return;
            }

            // Chuyển key thành số nguyên
            if (!int.TryParse(keyText, out int key))
            {
                MessageBox.Show("Vui lòng nhập một số nguyên làm key.");
                return;
            }

            // Mã hóa đoạn văn bản
            string cipherText = Encrypt(plaintext, key);

            // Đưa kết quả vào txtBoxC
            txtBoxC.Text = cipherText;
        }

        // Hàm mã hóa sử dụng phương pháp Caesar Cipher
        private string Encrypt(string text, int key)
        {
            string result = string.Empty;

            foreach (char c in text)
            {
                // Chỉ mã hóa các ký tự trong bảng chữ cái tiếng Anh
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result += (char)(((c + key - offset) % 26) + offset);
                }
                else
                {
                    // Nếu không phải chữ cái, giữ nguyên
                    result += c;
                }
            }

            return result;
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            // Xóa dữ liệu cũ trong các textbox
            txtBoxP.Clear();

            // Lấy dữ liệu từ các textbox
            string cipherText = txtBoxC.Text;
            string keyText = txtBoxK.Text;

            // Kiểm tra nếu txtBoxC hoặc txtBoxK trống thì yêu cầu nhập dữ liệu
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(keyText))
            {
                MessageBox.Show("Vui lòng nhập Ciphertext và Key!!!");
                return;
            }

            // Chuyển key thành số nguyên
            if (!int.TryParse(keyText, out int key))
            {
                MessageBox.Show("Vui lòng nhập một số nguyên làm key.");
                return;
            }

            // Giải mã đoạn văn bản
            string plainText = Decrypt(cipherText, key);

            // Đưa kết quả vào txtBoxP
            txtBoxP.Text = plainText;
        }

        // Hàm giải mã sử dụng phương pháp Caesar Cipher
        private string Decrypt(string text, int key)
        {
            string result = string.Empty;

            foreach (char c in text)
            {
                // Chỉ giải mã các ký tự trong bảng chữ cái tiếng Anh
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result += (char)(((c - key - offset + 26) % 26) + offset); // +26 để tránh giá trị âm
                }
                else
                {
                    // Nếu không phải chữ cái, giữ nguyên
                    result += c;
                }
            }

            return result;
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ txtBoxC
            string cipherText = txtBoxC.Text;

            // Kiểm tra nếu txtBoxC trống thì yêu cầu nhập dữ liệu
            if (string.IsNullOrEmpty(cipherText))
            {
                MessageBox.Show("Vui lòng nhập Ciphertext!!!");
                return;
            }

            // Giải mã với key hiện tại
            string plainText = Decrypt(cipherText, currentKey);

            // Đưa kết quả vào txtBoxP và hiển thị khóa hiện tại
            txtBoxP.Text = plainText;
            txtBoxK.Text = currentKey.ToString();

            // Tăng giá trị của currentKey để thử khóa tiếp theo lần sau
            currentKey++;

            // Đặt lại currentKey về 0 nếu đã thử hết các khóa (0-25)
            if (currentKey >= 26)
            {
                currentKey = 0;
            }
        }
    }
}
