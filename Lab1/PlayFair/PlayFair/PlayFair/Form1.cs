using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayFair
{
    public partial class PlayFair : Form
    {
        private char[,] matrix;
        public PlayFair()
        {
            InitializeComponent();
        }
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.ToUpper().Replace("J", "I");
            string plaintext = txtInput.Text.ToUpper().Replace("J", "I");
            matrix = GenerateMatrix(key);
            DisplayMatrix();
            txtOutput.Text = Encrypt(plaintext);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.ToUpper().Replace("J", "I");
            string ciphertext = txtInput.Text.ToUpper().Replace("J", "I");
            matrix = GenerateMatrix(key);
            DisplayMatrix();
            txtOutput.Text = Decrypt(ciphertext);
        }

        private char[,] GenerateMatrix(string key)
        {
            key = Regex.Replace(key, "[^A-Z]", "");
            key = new string(key.Distinct().ToArray());

            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            string keyWithAlphabet = key + new string(alphabet.Where(c => !key.Contains(c)).ToArray());

            char[,] matrix = new char[5, 5];
            for (int i = 0; i < 25; i++)
            {
                matrix[i / 5, i % 5] = keyWithAlphabet[i];
            }
            return matrix;
        }

        private void DisplayMatrix()
        {
            dataGridMatrix.ColumnCount = 5;
            dataGridMatrix.RowCount = 5;
            foreach (DataGridViewColumn column in dataGridMatrix.Columns)
            {
                column.Width = 50;
               
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    dataGridMatrix[j, i].Value = matrix[i, j];
                }
            }
        }

        private string FormatText(string text)
        {
            text = Regex.Replace(text, "[^A-Z]", "");
            StringBuilder formattedText = new StringBuilder();
            for (int i = 0; i < text.Length; i += 2)
            {
                char char1 = text[i];
                char char2 = (i + 1 < text.Length) ? text[i + 1] : 'X';

                if (char1 == char2)
                {
                    formattedText.Append(char1);
                    formattedText.Append('X');
                    i--;
                }
                else
                {
                    formattedText.Append(char1);
                    formattedText.Append(char2);
                }
            }
            if (formattedText.Length % 2 != 0)
                formattedText.Append('X');

            return formattedText.ToString();
        }

        private string Encrypt(string plaintext)
        {
            plaintext = FormatText(plaintext);
            StringBuilder ciphertext = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i += 2)
            {
                (int row1, int col1) = FindPosition(plaintext[i]);
                (int row2, int col2) = FindPosition(plaintext[i + 1]);

                if (row1 == row2)
                {
                    ciphertext.Append(matrix[row1, (col1 + 1) % 5]);
                    ciphertext.Append(matrix[row2, (col2 + 1) % 5]);
                }
                else if (col1 == col2)
                {
                    ciphertext.Append(matrix[(row1 + 1) % 5, col1]);
                    ciphertext.Append(matrix[(row2 + 1) % 5, col2]);
                }
                else
                {
                    ciphertext.Append(matrix[row1, col2]);
                    ciphertext.Append(matrix[row2, col1]);
                }
            }

            return ciphertext.ToString();
        }

        private string Decrypt(string ciphertext)
        {
            StringBuilder plaintext = new StringBuilder();

            for (int i = 0; i < ciphertext.Length; i += 2)
            {
                (int row1, int col1) = FindPosition(ciphertext[i]);
                (int row2, int col2) = FindPosition(ciphertext[i + 1]);

                if (row1 == row2)
                {
                    plaintext.Append(matrix[row1, (col1 + 4) % 5]);
                    plaintext.Append(matrix[row2, (col2 + 4) % 5]);
                }
                else if (col1 == col2)
                {
                    plaintext.Append(matrix[(row1 + 4) % 5, col1]);
                    plaintext.Append(matrix[(row2 + 4) % 5, col2]);
                }
                else
                {
                    plaintext.Append(matrix[row1, col2]);
                    plaintext.Append(matrix[row2, col1]);
                }
            }

            return plaintext.ToString();
        }

        private (int, int) FindPosition(char c)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (matrix[row, col] == c)
                        return (row, col);
                }
            }
            return (-1, -1);
        }
    }
}
