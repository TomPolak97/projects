namespace DamkaUI
{
    partial class gameSettingsForm
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
            this.buttonDone = new System.Windows.Forms.Button();
            this.lableBoardSize = new System.Windows.Forms.Label();
            this.radioButton6x6 = new System.Windows.Forms.RadioButton();
            this.radioButton8x8 = new System.Windows.Forms.RadioButton();
            this.radioButton10x10 = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(245, 217);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(79, 31);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.button1_Click);
            // 
            // lableBoardSize
            // 
            this.lableBoardSize.AutoSize = true;
            this.lableBoardSize.Location = new System.Drawing.Point(12, 9);
            this.lableBoardSize.Name = "lableBoardSize";
            this.lableBoardSize.Size = new System.Drawing.Size(91, 20);
            this.lableBoardSize.TabIndex = 0;
            this.lableBoardSize.Text = "Board Size:";
            this.lableBoardSize.Click += new System.EventHandler(this.label1_Click);
            // 
            // radioButton6x6
            // 
            this.radioButton6x6.AutoSize = true;
            this.radioButton6x6.Location = new System.Drawing.Point(38, 47);
            this.radioButton6x6.Name = "radioButton6x6";
            this.radioButton6x6.Size = new System.Drawing.Size(67, 24);
            this.radioButton6x6.TabIndex = 1;
            this.radioButton6x6.TabStop = true;
            this.radioButton6x6.Text = "6 x 6";
            this.radioButton6x6.UseVisualStyleBackColor = true;
            this.radioButton6x6.CheckedChanged += new System.EventHandler(this.radioButton6x6_CheckedChanged);
            // 
            // radioButton8x8
            // 
            this.radioButton8x8.AutoSize = true;
            this.radioButton8x8.Location = new System.Drawing.Point(112, 47);
            this.radioButton8x8.Name = "radioButton8x8";
            this.radioButton8x8.Size = new System.Drawing.Size(67, 24);
            this.radioButton8x8.TabIndex = 2;
            this.radioButton8x8.TabStop = true;
            this.radioButton8x8.Text = "8 x 8";
            this.radioButton8x8.UseVisualStyleBackColor = true;
            // 
            // radioButton10x10
            // 
            this.radioButton10x10.AutoSize = true;
            this.radioButton10x10.Location = new System.Drawing.Point(185, 47);
            this.radioButton10x10.Name = "radioButton10x10";
            this.radioButton10x10.Size = new System.Drawing.Size(85, 24);
            this.radioButton10x10.TabIndex = 3;
            this.radioButton10x10.TabStop = true;
            this.radioButton10x10.Text = "10 x 10";
            this.radioButton10x10.UseVisualStyleBackColor = true;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(17, 78);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(64, 20);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "Players:";
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(153, 115);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(171, 26);
            this.textBoxPlayer1.TabIndex = 6;
            this.textBoxPlayer1.TextChanged += new System.EventHandler(this.textBoxPlayer1_TextChanged);
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(38, 118);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(69, 20);
            this.labelPlayer1.TabIndex = 5;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(42, 168);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(95, 24);
            this.checkBoxPlayer2.TabIndex = 7;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(153, 168);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(171, 26);
            this.textBoxPlayer2.TabIndex = 8;
            this.textBoxPlayer2.Text = "[Computer]";
            this.textBoxPlayer2.TextChanged += new System.EventHandler(this.textBoxPlayer2_TextChanged);
            // 
            // gameSettingsForm
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 260);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButton10x10);
            this.Controls.Add(this.radioButton8x8);
            this.Controls.Add(this.radioButton6x6);
            this.Controls.Add(this.lableBoardSize);
            this.Controls.Add(this.buttonDone);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "gameSettingsForm";
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Label lableBoardSize;
        private System.Windows.Forms.RadioButton radioButton6x6;
        private System.Windows.Forms.RadioButton radioButton8x8;
        private System.Windows.Forms.RadioButton radioButton10x10;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer2;
    }
}

