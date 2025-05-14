namespace WslReviver
{
    partial class MainForm
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
            btnStart = new Button();
            statusLabel = new Label();
            intervalLabel = new Label();
            intervalNumericUpDown = new NumericUpDown();
            textBoxLogging = new TextBox();
            getWslProcessStatus = new Button();
            startWslButton = new Button();
            stopWslbutton = new Button();
            ((System.ComponentModel.ISupportInitialize)intervalNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(22, 71);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(131, 40);
            btnStart.TabIndex = 0;
            btnStart.Text = "스케줄러 시작";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(159, 76);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(132, 30);
            statusLabel.TabIndex = 1;
            statusLabel.Text = "상태 확인 전";
            // 
            // intervalLabel
            // 
            intervalLabel.AutoSize = true;
            intervalLabel.Location = new Point(22, 20);
            intervalLabel.Name = "intervalLabel";
            intervalLabel.Size = new Size(149, 30);
            intervalLabel.TabIndex = 2;
            intervalLabel.Text = "실행 간격 (분):";
            // 
            // intervalNumericUpDown
            // 
            intervalNumericUpDown.Location = new Point(177, 20);
            intervalNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            intervalNumericUpDown.Name = "intervalNumericUpDown";
            intervalNumericUpDown.Size = new Size(120, 35);
            intervalNumericUpDown.TabIndex = 3;
            intervalNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            intervalNumericUpDown.ValueChanged += intervalNumericUpDown_ValueChanged;
            // 
            // textBoxLogging
            // 
            textBoxLogging.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxLogging.ImeMode = ImeMode.NoControl;
            textBoxLogging.Location = new Point(22, 174);
            textBoxLogging.Multiline = true;
            textBoxLogging.Name = "textBoxLogging";
            textBoxLogging.Size = new Size(826, 341);
            textBoxLogging.TabIndex = 4;
            // 
            // getWslProcessStatus
            // 
            getWslProcessStatus.Location = new Point(22, 117);
            getWslProcessStatus.Name = "getWslProcessStatus";
            getWslProcessStatus.Size = new Size(220, 40);
            getWslProcessStatus.TabIndex = 5;
            getWslProcessStatus.Text = "WSL 프로세스 확인";
            getWslProcessStatus.UseVisualStyleBackColor = true;
            getWslProcessStatus.Click += btnShowProcessList_Click;
            // 
            // startWslButton
            // 
            startWslButton.Location = new Point(257, 117);
            startWslButton.Name = "startWslButton";
            startWslButton.Size = new Size(131, 40);
            startWslButton.TabIndex = 6;
            startWslButton.Text = "WSL 시작";
            startWslButton.UseVisualStyleBackColor = true;
            startWslButton.Click += button1_Click;
            // 
            // stopWslbutton
            // 
            stopWslbutton.Location = new Point(394, 117);
            stopWslbutton.Name = "stopWslbutton";
            stopWslbutton.Size = new Size(131, 40);
            stopWslbutton.TabIndex = 7;
            stopWslbutton.Text = "WSL 종료";
            stopWslbutton.UseVisualStyleBackColor = true;
            stopWslbutton.Click += stopWslbutton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(876, 543);
            Controls.Add(stopWslbutton);
            Controls.Add(startWslButton);
            Controls.Add(getWslProcessStatus);
            Controls.Add(textBoxLogging);
            Controls.Add(intervalNumericUpDown);
            Controls.Add(intervalLabel);
            Controls.Add(statusLabel);
            Controls.Add(btnStart);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WSL Reviver";
            ((System.ComponentModel.ISupportInitialize)intervalNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Button btnStart;
        public Label statusLabel;
        private Label intervalLabel;
        private NumericUpDown intervalNumericUpDown;
        private TextBox textBoxLogging;
        private Button getWslProcessStatus;
        private Button startWslButton;
        private Button stopWslbutton;
    }
}
