using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CryptogramSolver
{
    partial class UserInterface
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            uxMessage = new TextBox();
            uxEncrypt = new Button();
            uxDecrypt = new Button();
            uxResult = new TextBox();
            uxMessageLabel = new Label();
            SuspendLayout();
            // 
            // uxMessage
            // 
            uxMessage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uxMessage.Location = new Point(37, 53);
            uxMessage.Multiline = true;
            uxMessage.Name = "uxMessage";
            uxMessage.ScrollBars = ScrollBars.Vertical;
            uxMessage.Size = new Size(658, 184);
            uxMessage.TabIndex = 0;
            // 
            // uxEncrypt
            // 
            uxEncrypt.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            uxEncrypt.Location = new Point(109, 258);
            uxEncrypt.Name = "uxEncrypt";
            uxEncrypt.Size = new Size(132, 58);
            uxEncrypt.TabIndex = 1;
            uxEncrypt.Text = "Encrypt";
            uxEncrypt.UseVisualStyleBackColor = true;
            uxEncrypt.Click += Encrypt_Click;
            // 
            // uxDecrypt
            // 
            uxDecrypt.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            uxDecrypt.Location = new Point(459, 258);
            uxDecrypt.Name = "uxDecrypt";
            uxDecrypt.Size = new Size(132, 58);
            uxDecrypt.TabIndex = 2;
            uxDecrypt.Text = "Decrypt";
            uxDecrypt.UseVisualStyleBackColor = true;
            uxDecrypt.Click += Decrypt_Click;
            // 
            // uxResult
            // 
            uxResult.BackColor = SystemColors.ButtonFace;
            uxResult.Location = new Point(37, 341);
            uxResult.Multiline = true;
            uxResult.Name = "uxResult";
            uxResult.ReadOnly = true;
            uxResult.ScrollBars = ScrollBars.Vertical;
            uxResult.Size = new Size(658, 177);
            uxResult.TabIndex = 3;
            // 
            // uxMessageLabel
            // 
            uxMessageLabel.AutoSize = true;
            uxMessageLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            uxMessageLabel.Location = new Point(23, 27);
            uxMessageLabel.Name = "uxMessageLabel";
            uxMessageLabel.Size = new Size(88, 25);
            uxMessageLabel.TabIndex = 4;
            uxMessageLabel.Text = "Message";
            // 
            // UserInterface
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(707, 530);
            Controls.Add(uxMessageLabel);
            Controls.Add(uxResult);
            Controls.Add(uxDecrypt);
            Controls.Add(uxEncrypt);
            Controls.Add(uxMessage);
            MaximizeBox = false;
            Name = "UserInterface";
            Text = "Crypto Solver";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox uxMessage;
        private Button uxEncrypt;
        private Button uxDecrypt;
        private TextBox uxResult;
        private Label uxMessageLabel;
    }
}