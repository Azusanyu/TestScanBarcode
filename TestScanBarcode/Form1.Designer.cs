namespace TestScanBarcode
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
            this.camInput = new System.Windows.Forms.Label();
            this.showVideo = new System.Windows.Forms.PictureBox();
            this.buttStar = new System.Windows.Forms.Button();
            this.buttStop = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxInputCam = new System.Windows.Forms.ComboBox();
            this.buttGetCodePBH = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.showVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // camInput
            // 
            this.camInput.AutoSize = true;
            this.camInput.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.camInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.camInput.Location = new System.Drawing.Point(290, 22);
            this.camInput.Name = "camInput";
            this.camInput.Size = new System.Drawing.Size(246, 32);
            this.camInput.TabIndex = 0;
            this.camInput.Text = "Chọn thiết bị Scan:";
            // 
            // showVideo
            // 
            this.showVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.showVideo.Location = new System.Drawing.Point(111, 73);
            this.showVideo.Name = "showVideo";
            this.showVideo.Size = new System.Drawing.Size(941, 494);
            this.showVideo.TabIndex = 2;
            this.showVideo.TabStop = false;
            // 
            // buttStar
            // 
            this.buttStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttStar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttStar.Location = new System.Drawing.Point(12, 92);
            this.buttStar.Name = "buttStar";
            this.buttStar.Size = new System.Drawing.Size(93, 54);
            this.buttStar.TabIndex = 3;
            this.buttStar.Text = "Start";
            this.buttStar.UseVisualStyleBackColor = true;
            this.buttStar.Click += new System.EventHandler(this.buttStar_Click);
            // 
            // buttStop
            // 
            this.buttStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttStop.ForeColor = System.Drawing.Color.Red;
            this.buttStop.Location = new System.Drawing.Point(12, 192);
            this.buttStop.Name = "buttStop";
            this.buttStop.Size = new System.Drawing.Size(93, 56);
            this.buttStop.TabIndex = 4;
            this.buttStop.Text = "Pause";
            this.buttStop.UseVisualStyleBackColor = true;
            this.buttStop.Click += new System.EventHandler(this.buttStop_Click);
            // 
            // textOutput
            // 
            this.textOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textOutput.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textOutput.ForeColor = System.Drawing.Color.Red;
            this.textOutput.Location = new System.Drawing.Point(406, 606);
            this.textOutput.Name = "textOutput";
            this.textOutput.Size = new System.Drawing.Size(289, 53);
            this.textOutput.TabIndex = 5;
            this.textOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(105, 616);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 35);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mã phiếu bán hàng:";
            // 
            // comboBoxInputCam
            // 
            this.comboBoxInputCam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxInputCam.FormattingEnabled = true;
            this.comboBoxInputCam.Location = new System.Drawing.Point(542, 24);
            this.comboBoxInputCam.Name = "comboBoxInputCam";
            this.comboBoxInputCam.Size = new System.Drawing.Size(318, 33);
            this.comboBoxInputCam.TabIndex = 7;
            // 
            // buttGetCodePBH
            // 
            this.buttGetCodePBH.Font = new System.Drawing.Font("Noto Serif JP", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttGetCodePBH.Location = new System.Drawing.Point(701, 606);
            this.buttGetCodePBH.Name = "buttGetCodePBH";
            this.buttGetCodePBH.Size = new System.Drawing.Size(173, 56);
            this.buttGetCodePBH.TabIndex = 8;
            this.buttGetCodePBH.Text = "Lấy mã PBH";
            this.buttGetCodePBH.UseVisualStyleBackColor = true;
            this.buttGetCodePBH.Click += new System.EventHandler(this.buttGetCodePBH_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(12, 295);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 56);
            this.button1.TabIndex = 9;
            this.button1.Text = "Seach";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 685);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttGetCodePBH);
            this.Controls.Add(this.comboBoxInputCam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.buttStop);
            this.Controls.Add(this.buttStar);
            this.Controls.Add(this.showVideo);
            this.Controls.Add(this.camInput);
            this.Name = "Form1";
            this.Text = "Scan Barcode";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.showVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label camInput;
        private System.Windows.Forms.PictureBox showVideo;
        private System.Windows.Forms.Button buttStar;
        private System.Windows.Forms.Button buttStop;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInputCam;
        private System.Windows.Forms.Button buttGetCodePBH;
        private System.Windows.Forms.Button button1;
    }
}

