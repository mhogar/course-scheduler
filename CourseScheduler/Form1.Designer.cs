namespace CourseScheduler
{
    partial class MainWindow
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
            this.userInputBox = new System.Windows.Forms.TextBox();
            this.inputTextLabel = new System.Windows.Forms.Label();
            this.scheduleGenButton = new System.Windows.Forms.Button();
            this.courseLabel = new System.Windows.Forms.Label();
            this.schedulesComboBox = new System.Windows.Forms.ComboBox();
            this.randScheduleButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.scheduleGrid = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // userInputBox
            // 
            this.userInputBox.Location = new System.Drawing.Point(12, 29);
            this.userInputBox.Multiline = true;
            this.userInputBox.Name = "userInputBox";
            this.userInputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.userInputBox.Size = new System.Drawing.Size(238, 774);
            this.userInputBox.TabIndex = 0;
            this.userInputBox.WordWrap = false;
            // 
            // inputTextLabel
            // 
            this.inputTextLabel.AutoSize = true;
            this.inputTextLabel.Location = new System.Drawing.Point(12, 9);
            this.inputTextLabel.Name = "inputTextLabel";
            this.inputTextLabel.Size = new System.Drawing.Size(238, 17);
            this.inputTextLabel.TabIndex = 1;
            this.inputTextLabel.Text = "Paste course text form Web-Advisor:";
            // 
            // scheduleGenButton
            // 
            this.scheduleGenButton.Location = new System.Drawing.Point(12, 809);
            this.scheduleGenButton.Name = "scheduleGenButton";
            this.scheduleGenButton.Size = new System.Drawing.Size(238, 32);
            this.scheduleGenButton.TabIndex = 2;
            this.scheduleGenButton.Text = "Generate Schedules\r\n\r\n";
            this.scheduleGenButton.UseVisualStyleBackColor = true;
            this.scheduleGenButton.Click += new System.EventHandler(this.scheduleGenButton_Click);
            // 
            // courseLabel
            // 
            this.courseLabel.AutoSize = true;
            this.courseLabel.Location = new System.Drawing.Point(284, 141);
            this.courseLabel.Name = "courseLabel";
            this.courseLabel.Size = new System.Drawing.Size(0, 17);
            this.courseLabel.TabIndex = 3;
            // 
            // schedulesComboBox
            // 
            this.schedulesComboBox.FormattingEnabled = true;
            this.schedulesComboBox.Location = new System.Drawing.Point(287, 29);
            this.schedulesComboBox.MaxDropDownItems = 100;
            this.schedulesComboBox.Name = "schedulesComboBox";
            this.schedulesComboBox.Size = new System.Drawing.Size(135, 24);
            this.schedulesComboBox.TabIndex = 4;
            this.schedulesComboBox.SelectedIndexChanged += new System.EventHandler(this.schedulesComboBox_SelectedIndexChanged);
            // 
            // randScheduleButton
            // 
            this.randScheduleButton.Location = new System.Drawing.Point(287, 59);
            this.randScheduleButton.Name = "randScheduleButton";
            this.randScheduleButton.Size = new System.Drawing.Size(135, 45);
            this.randScheduleButton.TabIndex = 5;
            this.randScheduleButton.Text = "Choose Random Schedule";
            this.randScheduleButton.UseVisualStyleBackColor = true;
            this.randScheduleButton.Click += new System.EventHandler(this.randScheduleButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(287, 111);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(135, 23);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "Next Schedule";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // scheduleGrid
            // 
            this.scheduleGrid.Location = new System.Drawing.Point(712, 12);
            this.scheduleGrid.Name = "scheduleGrid";
            this.scheduleGrid.Size = new System.Drawing.Size(658, 812);
            this.scheduleGrid.TabIndex = 8;
            this.scheduleGrid.TabStop = false;
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 853);
            this.Controls.Add(this.scheduleGrid);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.randScheduleButton);
            this.Controls.Add(this.schedulesComboBox);
            this.Controls.Add(this.courseLabel);
            this.Controls.Add(this.scheduleGenButton);
            this.Controls.Add(this.inputTextLabel);
            this.Controls.Add(this.userInputBox);
            this.MaximizeBox = false;
            this.Name = "mainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Course Scheduler";
            ((System.ComponentModel.ISupportInitialize)(this.scheduleGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userInputBox;
        private System.Windows.Forms.Label inputTextLabel;
        private System.Windows.Forms.Button scheduleGenButton;
        private System.Windows.Forms.Label courseLabel;
        private System.Windows.Forms.ComboBox schedulesComboBox;
        private System.Windows.Forms.Button randScheduleButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.PictureBox scheduleGrid;
    }
}

