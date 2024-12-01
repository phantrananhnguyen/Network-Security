namespace WindowsFormsApp1
{
    partial class GenerateKey
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbKeysize = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_PrivateKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_PublicKey = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.chkProtectPrivateKey = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_GenerateKey = new System.Windows.Forms.Button();
            this.bt_SavePublicKey = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.bt_SavePrivateKey = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbKeysize
            // 
            this.cmbKeysize.AutoCompleteCustomSource.AddRange(new string[] {
            "1024",
            "2048",
            "4096"});
            this.cmbKeysize.FormattingEnabled = true;
            this.cmbKeysize.Location = new System.Drawing.Point(208, 36);
            this.cmbKeysize.Name = "cmbKeysize";
            this.cmbKeysize.Size = new System.Drawing.Size(121, 24);
            this.cmbKeysize.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_PrivateKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_PublicKey);
            this.groupBox1.Location = new System.Drawing.Point(12, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 143);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Key Details";
            // 
            // tb_PrivateKey
            // 
            this.tb_PrivateKey.Location = new System.Drawing.Point(30, 96);
            this.tb_PrivateKey.Multiline = true;
            this.tb_PrivateKey.Name = "tb_PrivateKey";
            this.tb_PrivateKey.Size = new System.Drawing.Size(532, 22);
            this.tb_PrivateKey.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Private Key:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Public Key:";
            // 
            // tb_PublicKey
            // 
            this.tb_PublicKey.Location = new System.Drawing.Point(30, 47);
            this.tb_PublicKey.Multiline = true;
            this.tb_PublicKey.Name = "tb_PublicKey";
            this.tb_PublicKey.Size = new System.Drawing.Size(532, 22);
            this.tb_PublicKey.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 403);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(590, 10);
            this.progressBar1.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_Password);
            this.groupBox2.Controls.Add(this.chkProtectPrivateKey);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sercurity Options";
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(103, 51);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.Size = new System.Drawing.Size(459, 22);
            this.tb_Password.TabIndex = 8;
            // 
            // chkProtectPrivateKey
            // 
            this.chkProtectPrivateKey.AutoSize = true;
            this.chkProtectPrivateKey.Location = new System.Drawing.Point(30, 22);
            this.chkProtectPrivateKey.Name = "chkProtectPrivateKey";
            this.chkProtectPrivateKey.Size = new System.Drawing.Size(227, 20);
            this.chkProtectPrivateKey.TabIndex = 8;
            this.chkProtectPrivateKey.Text = "Protect private key with password";
            this.chkProtectPrivateKey.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bit length of RSA modulus:";
            // 
            // btn_GenerateKey
            // 
            this.btn_GenerateKey.Location = new System.Drawing.Point(12, 339);
            this.btn_GenerateKey.Name = "btn_GenerateKey";
            this.btn_GenerateKey.Size = new System.Drawing.Size(190, 33);
            this.btn_GenerateKey.TabIndex = 5;
            this.btn_GenerateKey.Text = "Generate RSA Key Pair";
            this.btn_GenerateKey.UseVisualStyleBackColor = true;
            this.btn_GenerateKey.Click += new System.EventHandler(this.btn_GenerateKey_Click);
            // 
            // bt_SavePublicKey
            // 
            this.bt_SavePublicKey.Location = new System.Drawing.Point(229, 339);
            this.bt_SavePublicKey.Name = "bt_SavePublicKey";
            this.bt_SavePublicKey.Size = new System.Drawing.Size(121, 33);
            this.bt_SavePublicKey.TabIndex = 6;
            this.bt_SavePublicKey.Text = "Save Public Key";
            this.bt_SavePublicKey.UseVisualStyleBackColor = true;
            this.bt_SavePublicKey.Click += new System.EventHandler(this.bt_SavePublicKey_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(506, 339);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(95, 33);
            this.btn_close.TabIndex = 7;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // bt_SavePrivateKey
            // 
            this.bt_SavePrivateKey.Location = new System.Drawing.Point(356, 339);
            this.bt_SavePrivateKey.Name = "bt_SavePrivateKey";
            this.bt_SavePrivateKey.Size = new System.Drawing.Size(123, 33);
            this.bt_SavePrivateKey.TabIndex = 8;
            this.bt_SavePrivateKey.Text = "Save Private Key";
            this.bt_SavePrivateKey.UseVisualStyleBackColor = true;
            this.bt_SavePrivateKey.Click += new System.EventHandler(this.bt_SavePrivateKey_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(336, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "bits";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(13, 381);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 16);
            this.lbStatus.TabIndex = 10;
            // 
            // GenerateKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 414);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bt_SavePrivateKey);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.bt_SavePublicKey);
            this.Controls.Add(this.btn_GenerateKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbKeysize);
            this.Name = "GenerateKey";
            this.Text = "GenerateKey";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbKeysize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_PublicKey;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_PrivateKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_GenerateKey;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.CheckBox chkProtectPrivateKey;
        private System.Windows.Forms.Button bt_SavePublicKey;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button bt_SavePrivateKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbStatus;
    }
}