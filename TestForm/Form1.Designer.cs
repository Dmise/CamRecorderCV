namespace TestForm
{
    partial class Form1
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
            this.btnStartDuty = new System.Windows.Forms.Button();
            this.cmbCameras = new System.Windows.Forms.ComboBox();
            this.labCamera = new System.Windows.Forms.Label();
            this.tbSaveTo = new System.Windows.Forms.TextBox();
            this.labSaveTo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStopDuty = new System.Windows.Forms.Button();
            this.cmbResolutions = new System.Windows.Forms.ComboBox();
            this.labResolution = new System.Windows.Forms.Label();
            this.labDuty = new System.Windows.Forms.Label();
            this.labFragment = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labStatusDuty = new System.Windows.Forms.Label();
            this.labStatusFragment = new System.Windows.Forms.Label();
            this.labShowDutyVideoStatus = new System.Windows.Forms.Label();
            this.labShowFragmentVideoStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartDuty
            // 
            this.btnStartDuty.Location = new System.Drawing.Point(132, 125);
            this.btnStartDuty.Name = "btnStartDuty";
            this.btnStartDuty.Size = new System.Drawing.Size(94, 29);
            this.btnStartDuty.TabIndex = 0;
            this.btnStartDuty.Text = "Start";
            this.btnStartDuty.UseVisualStyleBackColor = true;
            this.btnStartDuty.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cmbCameras
            // 
            this.cmbCameras.FormattingEnabled = true;
            this.cmbCameras.Location = new System.Drawing.Point(92, 27);
            this.cmbCameras.Name = "cmbCameras";
            this.cmbCameras.Size = new System.Drawing.Size(290, 28);
            this.cmbCameras.TabIndex = 1;
            // 
            // labCamera
            // 
            this.labCamera.AutoSize = true;
            this.labCamera.Location = new System.Drawing.Point(23, 30);
            this.labCamera.Name = "labCamera";
            this.labCamera.Size = new System.Drawing.Size(63, 20);
            this.labCamera.TabIndex = 2;
            this.labCamera.Text = "Camera:";
            // 
            // tbSaveTo
            // 
            this.tbSaveTo.Location = new System.Drawing.Point(92, 68);
            this.tbSaveTo.Name = "tbSaveTo";
            this.tbSaveTo.Size = new System.Drawing.Size(544, 27);
            this.tbSaveTo.TabIndex = 3;
            this.tbSaveTo.Click += new System.EventHandler(this.tbSaveTo_Click);
            // 
            // labSaveTo
            // 
            this.labSaveTo.AutoSize = true;
            this.labSaveTo.Location = new System.Drawing.Point(27, 75);
            this.labSaveTo.Name = "labSaveTo";
            this.labSaveTo.Size = new System.Drawing.Size(59, 20);
            this.labSaveTo.TabIndex = 4;
            this.labSaveTo.Text = "SaveTo:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(27, 193);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnStopDuty
            // 
            this.btnStopDuty.Location = new System.Drawing.Point(232, 125);
            this.btnStopDuty.Name = "btnStopDuty";
            this.btnStopDuty.Size = new System.Drawing.Size(94, 29);
            this.btnStopDuty.TabIndex = 6;
            this.btnStopDuty.Text = "Stop";
            this.btnStopDuty.UseVisualStyleBackColor = true;
            this.btnStopDuty.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cmbResolutions
            // 
            this.cmbResolutions.FormattingEnabled = true;
            this.cmbResolutions.Location = new System.Drawing.Point(554, 27);
            this.cmbResolutions.Name = "cmbResolutions";
            this.cmbResolutions.Size = new System.Drawing.Size(209, 28);
            this.cmbResolutions.TabIndex = 1;
            // 
            // labResolution
            // 
            this.labResolution.AutoSize = true;
            this.labResolution.Location = new System.Drawing.Point(466, 30);
            this.labResolution.Name = "labResolution";
            this.labResolution.Size = new System.Drawing.Size(82, 20);
            this.labResolution.TabIndex = 2;
            this.labResolution.Text = "Resolution:";
            // 
            // labDuty
            // 
            this.labDuty.AutoSize = true;
            this.labDuty.Location = new System.Drawing.Point(36, 130);
            this.labDuty.Name = "labDuty";
            this.labDuty.Size = new System.Drawing.Size(90, 20);
            this.labDuty.TabIndex = 4;
            this.labDuty.Text = "Duty record:";
            // 
            // labFragment
            // 
            this.labFragment.AutoSize = true;
            this.labFragment.Location = new System.Drawing.Point(375, 130);
            this.labFragment.Name = "labFragment";
            this.labFragment.Size = new System.Drawing.Size(122, 20);
            this.labFragment.TabIndex = 4;
            this.labFragment.Text = "Fragment record:";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(27, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 75);
            this.label1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(372, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 75);
            this.label2.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 125);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnStartFragment_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(603, 125);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 6;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnStopFragment_Click);
            // 
            // labStatusDuty
            // 
            this.labStatusDuty.AutoSize = true;
            this.labStatusDuty.Location = new System.Drawing.Point(36, 161);
            this.labStatusDuty.Name = "labStatusDuty";
            this.labStatusDuty.Size = new System.Drawing.Size(52, 20);
            this.labStatusDuty.TabIndex = 4;
            this.labStatusDuty.Text = "Status:";
            // 
            // labStatusFragment
            // 
            this.labStatusFragment.AutoSize = true;
            this.labStatusFragment.Location = new System.Drawing.Point(375, 161);
            this.labStatusFragment.Name = "labStatusFragment";
            this.labStatusFragment.Size = new System.Drawing.Size(52, 20);
            this.labStatusFragment.TabIndex = 4;
            this.labStatusFragment.Text = "Status:";
            // 
            // labShowDutyVideoStatus
            // 
            this.labShowDutyVideoStatus.AutoSize = true;
            this.labShowDutyVideoStatus.Location = new System.Drawing.Point(94, 161);
            this.labShowDutyVideoStatus.Name = "labShowDutyVideoStatus";
            this.labShowDutyVideoStatus.Size = new System.Drawing.Size(34, 20);
            this.labShowDutyVideoStatus.TabIndex = 8;
            this.labShowDutyVideoStatus.Text = "idle";
            // 
            // labShowFragmentVideoStatus
            // 
            this.labShowFragmentVideoStatus.AutoSize = true;
            this.labShowFragmentVideoStatus.Location = new System.Drawing.Point(433, 161);
            this.labShowFragmentVideoStatus.Name = "labShowFragmentVideoStatus";
            this.labShowFragmentVideoStatus.Size = new System.Drawing.Size(34, 20);
            this.labShowFragmentVideoStatus.TabIndex = 8;
            this.labShowFragmentVideoStatus.Text = "idle";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 685);
            this.Controls.Add(this.labShowFragmentVideoStatus);
            this.Controls.Add(this.labShowDutyVideoStatus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnStopDuty);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labFragment);
            this.Controls.Add(this.labStatusFragment);
            this.Controls.Add(this.labStatusDuty);
            this.Controls.Add(this.labDuty);
            this.Controls.Add(this.labSaveTo);
            this.Controls.Add(this.tbSaveTo);
            this.Controls.Add(this.labResolution);
            this.Controls.Add(this.labCamera);
            this.Controls.Add(this.cmbResolutions);
            this.Controls.Add(this.cmbCameras);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStartDuty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnStartDuty;
        private ComboBox cmbCameras;
        private Label labCamera;
        private TextBox tbSaveTo;
        private Label labSaveTo;
        private PictureBox pictureBox1;
        private Button btnStopDuty;
        private ComboBox cmbResolutions;
        private Label labResolution;
        private Label labDuty;
        private Label labFragment;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private Label labStatusDuty;
        private Label labStatusFragment;
        private Label labShowDutyVideoStatus;
        private Label labShowFragmentVideoStatus;
    }
}