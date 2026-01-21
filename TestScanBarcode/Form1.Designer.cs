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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.camInput = new System.Windows.Forms.Label();
            this.showVideo = new System.Windows.Forms.PictureBox();
            this.buttStar = new System.Windows.Forms.Button();
            this.buttStop = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxInputCam = new System.Windows.Forms.ComboBox();
            this.buttShowIMG = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.showVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // camInput
            // 
            resources.ApplyResources(this.camInput, "camInput");
            this.camInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.camInput.Name = "camInput";
            this.camInput.UseWaitCursor = true;
            // 
            // showVideo
            // 
            this.showVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.showVideo, "showVideo");
            this.showVideo.Name = "showVideo";
            this.showVideo.TabStop = false;
            this.showVideo.UseWaitCursor = true;
            // 
            // buttStar
            // 
            resources.ApplyResources(this.buttStar, "buttStar");
            this.buttStar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttStar.Name = "buttStar";
            this.buttStar.UseVisualStyleBackColor = true;
            this.buttStar.UseWaitCursor = true;
            this.buttStar.Click += new System.EventHandler(this.buttStar_Click);
            // 
            // buttStop
            // 
            resources.ApplyResources(this.buttStop, "buttStop");
            this.buttStop.ForeColor = System.Drawing.Color.Red;
            this.buttStop.Name = "buttStop";
            this.buttStop.UseVisualStyleBackColor = true;
            this.buttStop.UseWaitCursor = true;
            this.buttStop.Click += new System.EventHandler(this.buttStop_Click);
            // 
            // textOutput
            // 
            this.textOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textOutput, "textOutput");
            this.textOutput.ForeColor = System.Drawing.Color.Red;
            this.textOutput.Name = "textOutput";
            this.textOutput.UseWaitCursor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Name = "label1";
            this.label1.UseWaitCursor = true;
            // 
            // comboBoxInputCam
            // 
            resources.ApplyResources(this.comboBoxInputCam, "comboBoxInputCam");
            this.comboBoxInputCam.FormattingEnabled = true;
            this.comboBoxInputCam.Name = "comboBoxInputCam";
            this.comboBoxInputCam.UseWaitCursor = true;
            // 
            // buttShowIMG
            // 
            resources.ApplyResources(this.buttShowIMG, "buttShowIMG");
            this.buttShowIMG.Name = "buttShowIMG";
            this.buttShowIMG.UseVisualStyleBackColor = true;
            this.buttShowIMG.UseWaitCursor = true;
            this.buttShowIMG.Click += new System.EventHandler(this.buttShowIMG_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttShowIMG);
            this.Controls.Add(this.comboBoxInputCam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.buttStop);
            this.Controls.Add(this.buttStar);
            this.Controls.Add(this.showVideo);
            this.Controls.Add(this.camInput);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form1";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.showVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Button buttShowIMG;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

