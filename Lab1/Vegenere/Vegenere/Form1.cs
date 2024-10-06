using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vegenere
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void btn_encrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_input.Text) && !string.IsNullOrEmpty(tb_key.Text))
            {
                string plaintext = tb_input.Text;
                string key = tb_key.Text;
                tb_output.Text = Encrypt(plaintext, key);
            }
            else MessageBox.Show("Please enter enough information");
        }

        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_input.Text) && !string.IsNullOrEmpty(tb_key.Text))
            {
                string ciphertext = tb_input.Text;
            string key = tb_key.Text;
            tb_output.Text = Decrypt(ciphertext, key);
            }
            else MessageBox.Show("Please enter enough information");
        }
        private string Encrypt(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            key = key.ToUpper();
            for (int i = 0, j = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int p = char.ToUpper(c) - 'A'; // Always work with uppercase
                    int k = key[j % key.Length] - 'A';
                    char encryptedChar = (char)((p + k) % 26 + 'A');
                    result.Append(isUpper ? encryptedChar : char.ToLower(encryptedChar));
                    j++;
                }
                else
                {
                    result.Append(c); // Keep non-letter characters unchanged
                }
            }
            return result.ToString();
        }

        private string Decrypt(string text, string key)
        {
            StringBuilder result = new StringBuilder();
            key = key.ToUpper();
            for (int i = 0, j = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetter(c))
                {
                    bool isUpper = char.IsUpper(c);
                    int C = char.ToUpper(c) - 'A'; // Always work with uppercase
                    int k = key[j % key.Length] - 'A';
                    char decryptedChar = (char)((C - k + 26) % 26 + 'A');
                    result.Append(isUpper ? decryptedChar : char.ToLower(decryptedChar));
                    j++;
                }
                else
                {
                    result.Append(c); // Keep non-letter characters unchanged
                }
            }
            return result.ToString();
        }




    }
}
