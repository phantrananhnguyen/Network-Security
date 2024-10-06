namespace Vegenere
{
    partial class Form1
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
            this.btn_encrypt = new System.Windows.Forms.Button();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_decrypt = new System.Windows.Forms.Button();
            this.tb_output = new System.Windows.Forms.TextBox();
            this.tb_key = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_encrypt
            // 
            this.btn_encrypt.Location = new System.Drawing.Point(207, 219);
            this.btn_encrypt.Name = "btn_encrypt";
            this.btn_encrypt.Size = new System.Drawing.Size(118, 35);
            this.btn_encrypt.TabIndex = 0;
            this.btn_encrypt.Text = "Encrypt";
            this.btn_encrypt.UseVisualStyleBackColor = true;
            this.btn_encrypt.Click += new System.EventHandler(this.btn_encrypt_Click);
            // 
            // tb_input
            // 
            this.tb_input.Location = new System.Drawing.Point(207, 80);
            this.tb_input.Multiline = true;
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(291, 80);
            this.tb_input.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(289, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vegenere";
            // 
            // btn_decrypt
            // 
            this.btn_decrypt.Location = new System.Drawing.Point(380, 219);
            this.btn_decrypt.Name = "btn_decrypt";
            this.btn_decrypt.Size = new System.Drawing.Size(118, 35);
            this.btn_decrypt.TabIndex = 3;
            this.btn_decrypt.Text = "Decrypt";
            this.btn_decrypt.UseVisualStyleBackColor = true;
            this.btn_decrypt.Click += new System.EventHandler(this.btn_decrypt_Click);
            // 
            // tb_output
            // 
            this.tb_output.Location = new System.Drawing.Point(207, 267);
            this.tb_output.Multiline = true;
            this.tb_output.Name = "tb_output";
            this.tb_output.Size = new System.Drawing.Size(291, 97);
            this.tb_output.TabIndex = 4;
            // 
            // tb_key
            // 
            this.tb_key.Location = new System.Drawing.Point(207, 166);
            this.tb_key.Multiline = true;
            this.tb_key.Name = "tb_key";
            this.tb_key.Size = new System.Drawing.Size(291, 40);
            this.tb_key.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 31;
            this.label2.Text = "Input";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Output";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_key);
            this.Controls.Add(this.tb_output);
            this.Controls.Add(this.btn_decrypt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.btn_encrypt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_encrypt;
        private System.Windows.Forms.TextBox tb_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_decrypt;
        private System.Windows.Forms.TextBox tb_output;
        private System.Windows.Forms.TextBox tb_key;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

