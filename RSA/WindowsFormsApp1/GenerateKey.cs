using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class GenerateKey : Form
    {
        public GenerateKey()
        {
            InitializeComponent();
        }

        private void btn_GenerateKey_Click(object sender, EventArgs e)
        {
            try
            {
                int keySize = int.Parse(cmbKeysize.SelectedItem.ToString());
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
                {
                    rsa.PersistKeyInCsp = false;

                    string publicKey = rsa.ToXmlString(false);  // Khóa công khai
                    string privateKey = rsa.ToXmlString(true);  // Khóa riêng

                    tb_PublicKey.Text = publicKey;
                    tb_PrivateKey.Text = privateKey;

                    // Lưu cặp khóa
                    using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                    {
                        folderDialog.Description = "Select folder to save the key pair";

                        if (folderDialog.ShowDialog() == DialogResult.OK)
                        {
                            SaveKeyPair(folderDialog.SelectedPath, publicKey, privateKey);
                        }
                    }

                    lbStatus.Text = "Key pair generated and saved successfully.";
                }
            }
            catch (Exception ex)
            {
                lbStatus.Text = $"Error: {ex.Message}";
            }
        }

        private void bt_SavePublicKey_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Public Key Files (*.xml)|*.xml",
                Title = "Save Public Key"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK) 
            {
                File.WriteAllText(saveFileDialog.FileName, tb_PublicKey.Text);
                lbStatus.Text = "Public key saved successfully.";
            }
        }

        private void bt_SavePrivateKey_Click(object sender, EventArgs e)
        {
            if(chkProtectPrivateKey.Checked && string.IsNullOrEmpty(tb_Password.Text))
            {
                lbStatus.Text = "Please enter a password to protect the private key";
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Private Key Files (*.xml)|*.xml",
                Title = "Save Private Key"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string privatekey = tb_PrivateKey.Text;

                if (chkProtectPrivateKey.Checked)
                {
                    privatekey = EncryptPrivateKey(privatekey, tb_Password.Text);
                }
                File.WriteAllText(saveFileDialog.FileName, privatekey);
                lbStatus.Text = "Private key saved successfully.";
            }
        }
        private string EncryptPrivateKey(string privateKey, string password)
        {
            // Triển khai mã hóa AES hoặc các thuật toán bảo mật khác
            return Convert.ToBase64String(
                ProtectedData.Protect(
                    System.Text.Encoding.UTF8.GetBytes(privateKey),
                    System.Text.Encoding.UTF8.GetBytes(password),
                    DataProtectionScope.CurrentUser));
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SaveKeyPair(string folderPath, string publicKey, string privateKey)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string publicKeyPath = Path.Combine(folderPath, "PublicKey.xml");
                File.WriteAllText(publicKeyPath, publicKey);

                string privateKeyPath = Path.Combine(folderPath, "PrivateKey.xml");
                File.WriteAllText(privateKeyPath, privateKey);

                lbStatus.Text = "Key pair saved successfully.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving key pair: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
