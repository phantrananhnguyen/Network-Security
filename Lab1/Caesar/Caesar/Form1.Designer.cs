namespace Caesar
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
            this.btnEncode = new System.Windows.Forms.Button();
            this.txtBoxP = new System.Windows.Forms.TextBox();
            this.txtBoxC = new System.Windows.Forms.TextBox();
            this.txtBoxK = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(170, 313);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(136, 30);
            this.btnEncode.TabIndex = 1;
            this.btnEncode.Text = "Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // txtBoxP
            // 
            this.txtBoxP.Location = new System.Drawing.Point(12, 53);
            this.txtBoxP.Multiline = true;
            this.txtBoxP.Name = "txtBoxP";
            this.txtBoxP.Size = new System.Drawing.Size(294, 232);
            this.txtBoxP.TabIndex = 2;
            // 
            // txtBoxC
            // 
            this.txtBoxC.Location = new System.Drawing.Point(494, 53);
            this.txtBoxC.Multiline = true;
            this.txtBoxC.Name = "txtBoxC";
            this.txtBoxC.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoxC.Size = new System.Drawing.Size(294, 232);
            this.txtBoxC.TabIndex = 3;
            // 
            // txtBoxK
            // 
            this.txtBoxK.Location = new System.Drawing.Point(331, 89);
            this.txtBoxK.Multiline = true;
            this.txtBoxK.Name = "txtBoxK";
            this.txtBoxK.Size = new System.Drawing.Size(130, 34);
            this.txtBoxK.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Plaintext";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(376, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(490, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ciphertext";
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(494, 313);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(136, 30);
            this.btnDecode.TabIndex = 8;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // btnKey
            // 
            this.btnKey.Location = new System.Drawing.Point(358, 205);
            this.btnKey.Name = "btnKey";
            this.btnKey.Size = new System.Drawing.Size(75, 23);
            this.btnKey.TabIndex = 9;
            this.btnKey.Text = "Key?";
            this.btnKey.UseVisualStyleBackColor = true;
            this.btnKey.Click += new System.EventHandler(this.btnKey_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKey);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxK);
            this.Controls.Add(this.txtBoxC);
            this.Controls.Add(this.txtBoxP);
            this.Controls.Add(this.btnEncode);
            this.Name = "Form1";
            this.Text = "CaesarCipher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.TextBox txtBoxP;
        private System.Windows.Forms.TextBox txtBoxC;
        private System.Windows.Forms.TextBox txtBoxK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnKey;
    }
}

