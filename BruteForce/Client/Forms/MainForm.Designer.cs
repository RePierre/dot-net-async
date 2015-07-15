namespace DotNetAsyncExamples.BruteForce.Client.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._richTextBox = new System.Windows.Forms.RichTextBox();
            this._buttonRun = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._labelPasswordLength = new System.Windows.Forms.Label();
            this._numericUpDownPassworLength = new System.Windows.Forms.NumericUpDown();
            this._trackBarPassworLength = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownPassworLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarPassworLength)).BeginInit();
            this.SuspendLayout();
            // 
            // _richTextBox
            // 
            this._richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._richTextBox.BackColor = System.Drawing.Color.Black;
            this._richTextBox.ForeColor = System.Drawing.Color.Green;
            this._richTextBox.Location = new System.Drawing.Point(0, 89);
            this._richTextBox.Name = "_richTextBox";
            this._richTextBox.ReadOnly = true;
            this._richTextBox.Size = new System.Drawing.Size(384, 322);
            this._richTextBox.TabIndex = 0;
            this._richTextBox.Text = "";
            // 
            // _buttonRun
            // 
            this._buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonRun.Location = new System.Drawing.Point(297, 12);
            this._buttonRun.Name = "_buttonRun";
            this._buttonRun.Size = new System.Drawing.Size(75, 23);
            this._buttonRun.TabIndex = 1;
            this._buttonRun.Text = "Run";
            this._buttonRun.UseVisualStyleBackColor = true;
            this._buttonRun.Click += new System.EventHandler(this.OnButtonRunClick);
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.Enabled = false;
            this._buttonCancel.Location = new System.Drawing.Point(297, 41);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonCancel.Click += new System.EventHandler(this.OnButtonCancelClick);
            // 
            // _labelPasswordLength
            // 
            this._labelPasswordLength.AutoSize = true;
            this._labelPasswordLength.Location = new System.Drawing.Point(13, 17);
            this._labelPasswordLength.Name = "_labelPasswordLength";
            this._labelPasswordLength.Size = new System.Drawing.Size(110, 13);
            this._labelPasswordLength.TabIndex = 3;
            this._labelPasswordLength.Text = "Max password length:";
            // 
            // _numericUpDownPassworLength
            // 
            this._numericUpDownPassworLength.Location = new System.Drawing.Point(130, 13);
            this._numericUpDownPassworLength.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._numericUpDownPassworLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numericUpDownPassworLength.Name = "_numericUpDownPassworLength";
            this._numericUpDownPassworLength.Size = new System.Drawing.Size(49, 20);
            this._numericUpDownPassworLength.TabIndex = 4;
            this._numericUpDownPassworLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numericUpDownPassworLength.ValueChanged += new System.EventHandler(this.OnNumericUpDownValueChanged);
            // 
            // _trackBarPassworLength
            // 
            this._trackBarPassworLength.Location = new System.Drawing.Point(16, 41);
            this._trackBarPassworLength.Minimum = 1;
            this._trackBarPassworLength.Name = "_trackBarPassworLength";
            this._trackBarPassworLength.Size = new System.Drawing.Size(163, 45);
            this._trackBarPassworLength.TabIndex = 5;
            this._trackBarPassworLength.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this._trackBarPassworLength.Value = 1;
            this._trackBarPassworLength.ValueChanged += new System.EventHandler(this.OnTrackBarValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 411);
            this.Controls.Add(this._trackBarPassworLength);
            this.Controls.Add(this._numericUpDownPassworLength);
            this.Controls.Add(this._labelPasswordLength);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonRun);
            this.Controls.Add(this._richTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "H4ck3r";
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownPassworLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarPassworLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox _richTextBox;
        private System.Windows.Forms.Button _buttonRun;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Label _labelPasswordLength;
        private System.Windows.Forms.NumericUpDown _numericUpDownPassworLength;
        private System.Windows.Forms.TrackBar _trackBarPassworLength;
    }
}