namespace WinFormCharpWebCam
{
    //Design by Pongsakorn Poosankam
    partial class mainWinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWinForm));
            this.imgVideo = new System.Windows.Forms.PictureBox();
            this.imgCapture = new System.Windows.Forms.PictureBox();
            this.bntStart = new System.Windows.Forms.Button();
            this.bntStop = new System.Windows.Forms.Button();
            this.bntContinue = new System.Windows.Forms.Button();
            this.bntCapture = new System.Windows.Forms.Button();
            this.bntSave = new System.Windows.Forms.Button();
            this.bntVideoFormat = new System.Windows.Forms.Button();
            this.bntVideoSource = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // imgVideo
            // 
            this.imgVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgVideo.Location = new System.Drawing.Point(55, 41);
            this.imgVideo.Name = "imgVideo";
            this.imgVideo.Size = new System.Drawing.Size(163, 160);
            this.imgVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgVideo.TabIndex = 0;
            this.imgVideo.TabStop = false;
            // 
            // imgCapture
            // 
            this.imgCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture.Location = new System.Drawing.Point(415, 41);
            this.imgCapture.Name = "imgCapture";
            this.imgCapture.Size = new System.Drawing.Size(163, 160);
            this.imgCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgCapture.TabIndex = 1;
            this.imgCapture.TabStop = false;
            // 
            // bntStart
            // 
            this.bntStart.Location = new System.Drawing.Point(55, 284);
            this.bntStart.Name = "bntStart";
            this.bntStart.Size = new System.Drawing.Size(100, 40);
            this.bntStart.TabIndex = 2;
            this.bntStart.Text = "Inicio";
            this.bntStart.UseVisualStyleBackColor = true;
            this.bntStart.Click += new System.EventHandler(this.bntStart_Click);
            // 
            // bntStop
            // 
            this.bntStop.Location = new System.Drawing.Point(161, 284);
            this.bntStop.Name = "bntStop";
            this.bntStop.Size = new System.Drawing.Size(100, 40);
            this.bntStop.TabIndex = 3;
            this.bntStop.Text = "Parar";
            this.bntStop.UseVisualStyleBackColor = true;
            this.bntStop.Click += new System.EventHandler(this.bntStop_Click);
            // 
            // bntContinue
            // 
            this.bntContinue.Location = new System.Drawing.Point(267, 284);
            this.bntContinue.Name = "bntContinue";
            this.bntContinue.Size = new System.Drawing.Size(100, 40);
            this.bntContinue.TabIndex = 4;
            this.bntContinue.Text = "Continuar";
            this.bntContinue.UseVisualStyleBackColor = true;
            this.bntContinue.Click += new System.EventHandler(this.bntContinue_Click);
            // 
            // bntCapture
            // 
            this.bntCapture.Location = new System.Drawing.Point(415, 284);
            this.bntCapture.Name = "bntCapture";
            this.bntCapture.Size = new System.Drawing.Size(100, 40);
            this.bntCapture.TabIndex = 5;
            this.bntCapture.Text = "Capturar";
            this.bntCapture.UseVisualStyleBackColor = true;
            this.bntCapture.Click += new System.EventHandler(this.bntCapture_Click);
            // 
            // bntSave
            // 
            this.bntSave.Location = new System.Drawing.Point(521, 284);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(100, 40);
            this.bntSave.TabIndex = 6;
            this.bntSave.Text = "Guardar";
            this.bntSave.UseVisualStyleBackColor = true;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // bntVideoFormat
            // 
            this.bntVideoFormat.Location = new System.Drawing.Point(55, 330);
            this.bntVideoFormat.Name = "bntVideoFormat";
            this.bntVideoFormat.Size = new System.Drawing.Size(100, 40);
            this.bntVideoFormat.TabIndex = 7;
            this.bntVideoFormat.Text = "Formato de Video";
            this.bntVideoFormat.UseVisualStyleBackColor = true;
            this.bntVideoFormat.Click += new System.EventHandler(this.bntVideoFormat_Click);
            // 
            // bntVideoSource
            // 
            this.bntVideoSource.Location = new System.Drawing.Point(415, 330);
            this.bntVideoSource.Name = "bntVideoSource";
            this.bntVideoSource.Size = new System.Drawing.Size(100, 40);
            this.bntVideoSource.TabIndex = 8;
            this.bntVideoSource.Text = "Configuración";
            this.bntVideoSource.UseVisualStyleBackColor = true;
            this.bntVideoSource.Click += new System.EventHandler(this.bntVideoSource_Click);
            // 
            // mainWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 382);
            this.Controls.Add(this.bntVideoSource);
            this.Controls.Add(this.bntVideoFormat);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.bntCapture);
            this.Controls.Add(this.bntContinue);
            this.Controls.Add(this.bntStop);
            this.Controls.Add(this.bntStart);
            this.Controls.Add(this.imgCapture);
            this.Controls.Add(this.imgVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainWinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captura de Fotografía";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainWinForm_FormClosing);
            this.Load += new System.EventHandler(this.mainWinForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgVideo;
        private System.Windows.Forms.PictureBox imgCapture;
        private System.Windows.Forms.Button bntStart;
        private System.Windows.Forms.Button bntStop;
        private System.Windows.Forms.Button bntContinue;
        private System.Windows.Forms.Button bntCapture;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Button bntVideoFormat;
        private System.Windows.Forms.Button bntVideoSource;


    }
}

