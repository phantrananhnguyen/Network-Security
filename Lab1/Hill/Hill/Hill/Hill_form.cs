using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Hill
{
    public partial class Hill_form : Form
    {
        private bool isUpdating = false;
        private int size;
        public Hill_form()
        {
            InitializeComponent();
            key_alpha00.TextChanged += KeyTextBox_TextChanged;
            key_alpha01.TextChanged += KeyTextBox_TextChanged;
            key_alpha02.TextChanged += KeyTextBox_TextChanged;
            key_alpha10.TextChanged += KeyTextBox_TextChanged;
            key_alpha11.TextChanged += KeyTextBox_TextChanged;
            key_alpha12.TextChanged += KeyTextBox_TextChanged;
            key_alpha20.TextChanged += KeyTextBox_TextChanged;
            key_alpha21.TextChanged += KeyTextBox_TextChanged;
            key_alpha22.TextChanged += KeyTextBox_TextChanged;

            key_num00.TextChanged += NumTextBox_TextChanged;
            key_num01.TextChanged += NumTextBox_TextChanged;
            key_num02.TextChanged += NumTextBox_TextChanged;
            key_num10.TextChanged += NumTextBox_TextChanged;
            key_num11.TextChanged += NumTextBox_TextChanged;
            key_num12.TextChanged += NumTextBox_TextChanged;
            key_num20.TextChanged += NumTextBox_TextChanged;
            key_num21.TextChanged += NumTextBox_TextChanged;
            key_num22.TextChanged += NumTextBox_TextChanged;

            check_alpha.Checked = true;
            check_22.Checked = true;
        }

        private void btn_en_Click(object sender, EventArgs e)
        {
            string plainText = tb_input.Text.ToUpper();
            List<int> spacePositions = new List<int>();

            // Lưu vị trí của các dấu cách
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == ' ')
                {
                    spacePositions.Add(i);
                }
            }

            // Xóa bỏ dấu cách khỏi plainText
            plainText = plainText.Replace(" ", "");

            try
            {
                int[,] keyMatrix = GetKeyMatrix();
                string cipherText = Encrypt(plainText, keyMatrix);

                // Chèn lại dấu cách vào vị trí cũ
                StringBuilder cipherTextWithSpaces = new StringBuilder(cipherText);
                foreach (int position in spacePositions)
                {
                    if (position < cipherTextWithSpaces.Length)
                    {
                        cipherTextWithSpaces.Insert(position, ' ');
                    }
                }

                tb_output.Text = cipherTextWithSpaces.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy ma trận khóa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_dec_Click(object sender, EventArgs e)
        {
            string cipherText = tb_input.Text.ToUpper();
            List<int> spacePositions = new List<int>();

            // Lưu vị trí của các dấu cách trong cipherText
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (cipherText[i] == ' ')
                {
                    spacePositions.Add(i);
                }
            }

            // Xóa bỏ dấu cách khỏi cipherText
            cipherText = cipherText.Replace(" ", "");

            try
            {
                int[,] keyMatrix = GetKeyMatrix();
                int[,] inverseKeyMatrix = GetInverseKeyMatrix(keyMatrix);
                string decryptedText = Encrypt(cipherText, inverseKeyMatrix);

                // Remove padding ('X') if necessary
                decryptedText = decryptedText.TrimEnd('X');

                // Chèn lại dấu cách vào vị trí cũ
                StringBuilder decryptedTextWithSpaces = new StringBuilder(decryptedText);
                foreach (int position in spacePositions)
                {
                    if (position < decryptedTextWithSpaces.Length)
                    {
                        decryptedTextWithSpaces.Insert(position, ' ');
                    }
                }

                tb_output.Text = decryptedTextWithSpaces.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy ma trận khóa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int getSize()
        {
            if (check_22.Checked == true) size = 2;
            else if (check_33.Checked == true) size = 3;
            return size;
        }


        private int[,] GetKeyMatrix()
        {
            int sizeMatrix = getSize();
            int[,] keyMatrix = new int[sizeMatrix, sizeMatrix];
            if (sizeMatrix == 2)
            {
                keyMatrix[0, 0] = int.Parse(key_num00.Text);
                keyMatrix[0, 1] = int.Parse(key_num01.Text);
                keyMatrix[1, 0] = int.Parse(key_num10.Text);
                keyMatrix[1, 1] = int.Parse(key_num11.Text);
            }
            else if (sizeMatrix == 3)
            {
                keyMatrix[0, 0] = int.Parse(key_num00.Text);
                keyMatrix[0, 1] = int.Parse(key_num01.Text);
                keyMatrix[0, 2] = int.Parse(key_num02.Text);
                keyMatrix[1, 0] = int.Parse(key_num10.Text);
                keyMatrix[1, 1] = int.Parse(key_num11.Text);
                keyMatrix[1, 2] = int.Parse(key_num12.Text);
                keyMatrix[2, 0] = int.Parse(key_num20.Text);
                keyMatrix[2, 1] = int.Parse(key_num21.Text);
                keyMatrix[2, 2] = int.Parse(key_num22.Text);
            }
            return keyMatrix;

        }
        private int[,] GetInverseKeyMatrix(int[,] keyMatrix)
        {
            int sizeMatrix = keyMatrix.GetLength(0);
            if (sizeMatrix != 2 && sizeMatrix != 3)
            {
                throw new InvalidOperationException("This implementation currently supports only 2x2 and 3x3 matrices.");
            }

            int determinant;
            int[,] inverseMatrix;

            if (sizeMatrix == 2)
            {
                // Extract the elements of the 2x2 matrix
                int a = keyMatrix[0, 0];
                int b = keyMatrix[0, 1];
                int c = keyMatrix[1, 0];
                int d = keyMatrix[1, 1];

                // Calculate the determinant
                determinant = (a * d - b * c) % 26;
                if (determinant < 0)
                {
                    determinant += 26;
                }

                // Find the modular inverse of the determinant
                int determinantInverse = ModularInverse(determinant, 26);
                if (determinantInverse == -1)
                {
                    throw new InvalidOperationException("The key matrix is not invertible modulo 26.");
                }

                // Calculate the inverse matrix
                inverseMatrix = new int[2, 2];
                inverseMatrix[0, 0] = (d * determinantInverse) % 26;
                inverseMatrix[0, 1] = (-b * determinantInverse) % 26;
                inverseMatrix[1, 0] = (-c * determinantInverse) % 26;
                inverseMatrix[1, 1] = (a * determinantInverse) % 26;
            }
            else // size == 3
            {
                // Calculate the determinant of the 3x3 matrix
                determinant = (
                    keyMatrix[0, 0] * (keyMatrix[1, 1] * keyMatrix[2, 2] - keyMatrix[1, 2] * keyMatrix[2, 1]) -
                    keyMatrix[0, 1] * (keyMatrix[1, 0] * keyMatrix[2, 2] - keyMatrix[1, 2] * keyMatrix[2, 0]) +
                    keyMatrix[0, 2] * (keyMatrix[1, 0] * keyMatrix[2, 1] - keyMatrix[1, 1] * keyMatrix[2, 0])
                ) % 26;

                if (determinant < 0)
                {
                    determinant += 26;
                }

                // Find the modular inverse of the determinant
                int determinantInverse = ModularInverse(determinant, 26);
                if (determinantInverse == -1)
                {
                    throw new InvalidOperationException("The key matrix is not invertible modulo 26.");
                }

                // Calculate the adjugate matrix
                inverseMatrix = new int[3, 3];
                inverseMatrix[0, 0] = (keyMatrix[1, 1] * keyMatrix[2, 2] - keyMatrix[1, 2] * keyMatrix[2, 1]) * determinantInverse % 26;
                inverseMatrix[0, 1] = (keyMatrix[0, 2] * keyMatrix[2, 1] - keyMatrix[0, 1] * keyMatrix[2, 2]) * determinantInverse % 26;
                inverseMatrix[0, 2] = (keyMatrix[0, 1] * keyMatrix[1, 2] - keyMatrix[0, 2] * keyMatrix[1, 1]) * determinantInverse % 26;
                inverseMatrix[1, 0] = (keyMatrix[1, 2] * keyMatrix[2, 0] - keyMatrix[1, 0] * keyMatrix[2, 2]) * determinantInverse % 26;
                inverseMatrix[1, 1] = (keyMatrix[0, 0] * keyMatrix[2, 2] - keyMatrix[0, 2] * keyMatrix[2, 0]) * determinantInverse % 26;
                inverseMatrix[1, 2] = (keyMatrix[0, 2] * keyMatrix[1, 0] - keyMatrix[0, 0] * keyMatrix[1, 2]) * determinantInverse % 26;
                inverseMatrix[2, 0] = (keyMatrix[1, 0] * keyMatrix[2, 1] - keyMatrix[1, 1] * keyMatrix[2, 0]) * determinantInverse % 26;
                inverseMatrix[2, 1] = (keyMatrix[0, 1] * keyMatrix[2, 0] - keyMatrix[0, 0] * keyMatrix[2, 1]) * determinantInverse % 26;
                inverseMatrix[2, 2] = (keyMatrix[0, 0] * keyMatrix[1, 1] - keyMatrix[0, 1] * keyMatrix[1, 0]) * determinantInverse % 26;
            }

            // Ensure all values are positive
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    if (inverseMatrix[i, j] < 0)
                    {
                        inverseMatrix[i, j] += 26;
                    }
                }
            }

            return inverseMatrix;
        }


        private int ModularInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    return x;
                }
            }
            return -1; // No modular inverse if -1
        }




        private string Encrypt(string text, int[,] keyMatrix)
        {
            int sizeMatrix = keyMatrix.GetLength(0);
            int originalLength = text.Length;

            // Pad the text to be a multiple of the matrix size
            text = text.PadRight((text.Length + sizeMatrix - 1) / sizeMatrix * sizeMatrix, 'X');

            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i += sizeMatrix)
            {
                int[] vector = new int[sizeMatrix];
                for (int j = 0; j < sizeMatrix; j++)
                {
                    if (i + j < text.Length)
                    {
                        vector[j] = text[i + j] - 'A';
                    }
                    else
                    {
                        vector[j] = 'X' - 'A'; // Padding with 'X'
                    }
                }

                int[] encryptedVector = MultiplyMatrixVector(keyMatrix, vector);
                for (int j = 0; j < sizeMatrix && (i + j) < text.Length; j++)
                {
                    result[i + j] = (char)((encryptedVector[j] % 26 + 26) % 26 + 'A');
                }
            }

            return new string(result);
        }


        private int[] MultiplyMatrixVector(int[,] matrix, int[] vector)
        {
            int size = matrix.GetLength(0);
            int[] result = new int[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = 0;
                for (int j = 0; j < size; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }


      

        private void check_alpha_CheckedChanged(object sender, EventArgs e)
        {
            check_number.Checked = !check_alpha.Checked;
            foreach (Control control in groupBox2.Controls)
            {
                control.Enabled = !check_alpha.Checked;
            }
            if (check_22.Checked && check_alpha.Checked)
            {
                key_alpha02.Enabled = false;
                key_alpha12.Enabled = false;
                key_alpha20.Enabled = false;
                key_alpha21.Enabled = false;
                key_alpha22.Enabled = false;
            }
            else if (check_33.Checked && check_alpha.Checked)
            {
                foreach (Control control in groupBox1.Controls)
                {
                    control.Enabled = true;
                }
            }
        }

        private void check_number_CheckedChanged(object sender, EventArgs e)
        {
            check_alpha.Checked = !check_number.Checked;
            foreach (Control control in groupBox1.Controls)
            {
                control.Enabled = !check_number.Checked;
            }
            if (check_22.Checked && check_number.Checked)
            {
                key_num02.Enabled = false;
                key_num12.Enabled = false;
                key_num20.Enabled = false;
                key_num21.Enabled = false;
                key_num22.Enabled = false;
            }
            else if (check_33.Checked && check_number.Checked)
            {
                foreach (Control control in groupBox2.Controls)
                {
                    control.Enabled = true;
                }
            }
        }
        private void KeyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isUpdating) return;
            isUpdating = true;
            // Xác định TextBox nào thay đổi và cập nhật Label tương ứng
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                try
                {
                    int numericValue = CharToInt(textBox.Text);
                    if (textBox == key_alpha00)
                    {
                        key_num00.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha01)
                    {
                        key_num01.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha02)
                    {
                        key_num02.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha10)
                    {
                        key_num10.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha11)
                    {
                        key_num11.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha12)
                    {
                        key_num12.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha20)
                    {
                        key_num20.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha21)
                    {
                        key_num21.Text = numericValue.ToString();
                    }
                    else if (textBox == key_alpha22)
                    {
                        key_num22.Text = numericValue.ToString();
                    }
                }
                catch (ArgumentException)
                {
                    // Nếu giá trị không hợp lệ, xóa nội dung của Label tương ứng
                    if (textBox == key_alpha00)
                    {
                        key_num00.Text = "";
                    }
                    else if (textBox == key_alpha01)
                    {
                        key_num01.Text = "";
                    }
                    else if (textBox == key_alpha02)
                    {
                        key_num02.Text = "";
                    }
                    else if (textBox == key_alpha10)
                    {
                        key_num10.Text = "";
                    }
                    else if (textBox == key_alpha11)
                    {
                        key_num11.Text = "";
                    }
                    else if (textBox == key_alpha12)
                    {
                        key_num12.Text = "";
                    }
                    else if (textBox == key_alpha20)
                    {
                        key_num20.Text = "";
                    }
                    else if (textBox == key_alpha21)
                    {
                        key_num21.Text = "";
                    }
                    else if (textBox == key_alpha22)
                    {
                        key_num22.Text = "";
                    }
                }
            }
            isUpdating = false;
        }
        private void NumTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isUpdating) return;

            isUpdating = true;

            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                try
                {
                    int numericValue = int.Parse(textBox.Text);
                    if (numericValue < 0 || numericValue > 25)
                    {
                        throw new ArgumentOutOfRangeException("Giá trị phải nằm trong khoảng từ 0 đến 25.");
                    }

                    char character = IntToChar(numericValue);

                    if (textBox == key_num00)
                    {
                        key_alpha00.Text = character.ToString();
                    }
                    else if (textBox == key_num01)
                    {
                        key_alpha01.Text = character.ToString();
                    }
                    else if (textBox == key_num02)
                    {
                        key_alpha02.Text = character.ToString();
                    }
                    else if (textBox == key_num10)
                    {
                        key_alpha10.Text = character.ToString();
                    }
                    else if (textBox == key_num11)
                    {
                        key_alpha11.Text = character.ToString();
                    }
                    else if (textBox == key_num12)
                    {
                        key_alpha12.Text = character.ToString();
                    }
                    else if (textBox == key_num20)
                    {
                        key_alpha20.Text = character.ToString();
                    }
                    else if (textBox == key_num21)
                    {
                        key_alpha21.Text = character.ToString();
                    }
                    else if (textBox == key_num22)
                    {
                        key_alpha22.Text = character.ToString();
                    }
                }
                catch (Exception)
                {
                    // Nếu giá trị không hợp lệ, xóa nội dung của TextBox chứa ký tự tương ứng
                    if (textBox == key_num00)
                    {
                        key_alpha00.Text = "";
                    }
                    else if (textBox == key_num01)
                    {
                        key_alpha01.Text = "";
                    }
                    else if (textBox == key_num02)
                    {
                        key_alpha02.Text = "";
                    }
                    else if (textBox == key_num10)
                    {
                        key_alpha10.Text = "";
                    }
                    else if (textBox == key_num11)
                    {
                        key_alpha11.Text = "";
                    }
                    else if (textBox == key_num12)
                    {
                        key_alpha12.Text = "";
                    }
                    else if (textBox == key_num20)
                    {
                        key_alpha20.Text = "";
                    }
                    else if (textBox == key_num21)
                    {
                        key_alpha21.Text = "";
                    }
                    else if (textBox == key_num22)
                    {
                        key_alpha22.Text = "";
                    }
                }
            }

            isUpdating = false;
        }

        private int CharToInt(string input)
        {
            // Kiểm tra xem giá trị nhập có phải là một ký tự và có độ dài hợp lệ hay không
            if (input.Length != 1 || !char.IsLetter(input[0]))
            {
                throw new ArgumentException("Giá trị nhập vào phải là một ký tự chữ cái.");
            }

            char c = char.ToUpper(input[0]);
            return c - 'A'; // A = 0, B = 1, ..., Z = 25
        }
        private char IntToChar(int value)
        {
            // Chuyển đổi số thành ký tự (0 -> A, 1 -> B, ..., 25 -> Z)
            if (value < 0 || value > 25)
            {
                throw new ArgumentOutOfRangeException("Giá trị phải nằm trong khoảng từ 0 đến 25.");
            }

            return (char)('A' + value);
        }

        private void check_22_CheckedChanged(object sender, EventArgs e)
        {
            check_33.Checked = !check_22.Checked;
            if (check_22.Checked && check_alpha.Checked)
            {
                key_alpha02.Enabled = false;
                key_alpha12.Enabled = false;
                key_alpha20.Enabled = false;
                key_alpha21.Enabled = false;
                key_alpha22.Enabled = false;
            }       
            else if(check_22.Checked && check_number.Checked) {
                key_num02.Enabled = false;
                key_num12.Enabled = false;
                key_num20.Enabled = false;
                key_num21.Enabled = false;
                key_num22.Enabled = false;
            }
        }

        private void check_33_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = check_33.Checked;
            check_22.Checked = !check_33.Checked;
            check_22.Checked = !isChecked;
            if (check_alpha.Checked  && isChecked)
            {
                foreach (Control control in groupBox1.Controls)
                {
                    control.Enabled = isChecked;
                }
            }
            else if (check_number.Checked && isChecked)
            {
                foreach (Control control in groupBox2.Controls)
                {
                    control.Enabled = isChecked;
                }
            }
        }

        
    }
}
