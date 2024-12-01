using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private TextBox activeTextBox;
        private Point mouseOffset; 
        private bool isMouseDown = false;
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point location = new Point(0, 0 + this.Controls.Count * 220); // Tạo TextBox mới bên dưới các TextBox hiện có
            CreateDynamicTextBox(location);
        }
        private TextBox CreateDynamicTextBox(Point location, string content = "")
        {
            TextBox dynamicTB = new TextBox
            {
                Multiline = true,
                Size = new Size(420, 200),  // Kích thước mặc định
                Location = location,        // Vị trí bắt đầu
                ScrollBars = ScrollBars.Vertical,
                Text = content              // Nội dung của TextBox
            };

            dynamicTB.MouseDown += DynamicTB_MouseDown;  // Bắt sự kiện nhấn chuột
            dynamicTB.MouseMove += DynamicTB_MouseMove;  // Bắt sự kiện di chuyển chuột
            dynamicTB.MouseUp += DynamicTB_MouseUp;      // Bắt sự kiện thả chuột

            // Tạo nút đóng
            Button closeButton = new Button
            {
                Text = "X",
                Size = new Size(30, 30),  // Kích thước nút đóng
                Location = new Point(dynamicTB.Width - 50, 10), // Vị trí nút ở góc trên bên phải
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            closeButton.Click += (sender, e) => CloseTextBox(dynamicTB, closeButton); // Gắn sự kiện nút đóng

            // Thêm nút vào form
            dynamicTB.Controls.Add(closeButton);

            this.Controls.Add(dynamicTB);  // Thêm TextBox vào Form
            return dynamicTB;
        }
        private void CloseTextBox(TextBox textBox, Button closeButton)
        {
            this.Controls.Remove(textBox);   // Xóa TextBox khỏi form
            textBox.Dispose();               // Giải phóng tài nguyên TextBox

            this.Controls.Remove(closeButton); // Xóa nút đóng khỏi form
            closeButton.Dispose();            // Giải phóng tài nguyên Button
        }
        private void DynamicTB_MouseDown(object sender, MouseEventArgs e)
        {
            // Khi nhấn chuột lên TextBox, lưu vị trí của chuột
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                mouseOffset = e.Location;  // Lưu tọa độ chuột tại thời điểm nhấn
            }
        }

        private void DynamicTB_MouseMove(object sender, MouseEventArgs e)
        {
            // Khi kéo chuột trong lúc nhấn, di chuyển TextBox
            if (isMouseDown)
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    // Tính toán vị trí mới của TextBox
                    textBox.Left = e.X + textBox.Left - mouseOffset.X;
                    textBox.Top = e.Y + textBox.Top - mouseOffset.Y;
                }
            }
        }

        private void DynamicTB_MouseUp(object sender, MouseEventArgs e)
        {
            // Khi thả chuột, ngừng di chuyển
            isMouseDown = false;
        }
        private void TextBox_Click(object sender, EventArgs e)
        {
            activeTextBox = sender as TextBox; 
        }
        
        private void rSAEncryptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (activeTextBox == null)
                {
                    MessageBox.Show("No active text box to encrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string plainText = activeTextBox.Text; 
                if (string.IsNullOrEmpty(plainText))
                {
                    MessageBox.Show("Please enter data to encrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chọn khóa công khai
                string publicKey = SelectAndLoadKeyPair(loadPrivateKey: false);
                if (string.IsNullOrEmpty(publicKey))
                {
                    MessageBox.Show("Encryption canceled: No public key selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mã hóa
                string encryptedData = EncryptData(plainText, publicKey);

                // Hiển thị kết quả trong TextBox mới
                CreateDynamicTextBox(new Point(50, 50 + this.Controls.Count * 220), encryptedData);
                MessageBox.Show("Data encrypted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during encryption: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string SelectAndLoadKeyPair(bool loadPrivateKey)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select folder containing the RSA key pair";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;

                    try
                    {
                        string keyFile = loadPrivateKey ? "PrivateKey.xml" : "PublicKey.xml";
                        string keyPath = Path.Combine(folderPath, keyFile);

                        if (!File.Exists(keyPath))
                        {
                            throw new Exception($"{keyFile} not found in the selected folder.");
                        }

                        return File.ReadAllText(keyPath); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading key: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return null; 
        }
        private string EncryptData(string plainText, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(publicKey);

                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedData = rsa.Encrypt(dataToEncrypt, false);

                return Convert.ToBase64String(encryptedData); 
            }
        }
        private string DecryptData(string encryptedText, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(privateKey);

                byte[] dataToDecrypt = Convert.FromBase64String(encryptedText);
                byte[] decryptedData = rsa.Decrypt(dataToDecrypt, false);

                return Encoding.UTF8.GetString(decryptedData); 
            }
        }

        private void rSADecryptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (activeTextBox == null)
                {
                    MessageBox.Show("No active text box to decrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string encryptedText = activeTextBox.Text; 
                if (string.IsNullOrEmpty(encryptedText))
                {
                    MessageBox.Show("Please enter encrypted data to decrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chọn khóa riêng
                string privateKey = SelectAndLoadKeyPair(loadPrivateKey: true);
                if (string.IsNullOrEmpty(privateKey))
                {
                    MessageBox.Show("Decryption canceled: No private key selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Giải mã
                string decryptedData = DecryptData(encryptedText, privateKey);

                // Hiển thị kết quả trong TextBox mới
                CreateDynamicTextBox(new Point(50, 50 + this.Controls.Count * 220), decryptedData);
                MessageBox.Show("Data decrypted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during decryption: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Open a File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileContent = File.ReadAllText(openFileDialog.FileName);
                    Point location = new Point(0, 0+ this.Controls.Count * 220); // Tạo TextBox mới bên dưới các TextBox hiện có
                    CreateDynamicTextBox(location, fileContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void generateKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateKey form = new GenerateKey();
            form.ShowDialog();
        }
    }
}
