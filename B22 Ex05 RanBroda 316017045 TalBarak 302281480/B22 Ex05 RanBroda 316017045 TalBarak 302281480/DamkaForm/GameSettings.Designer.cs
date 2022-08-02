namespace DamkaForm
{
    partial class GameSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonSizeSix = new System.Windows.Forms.RadioButton();
            this.radioButtonSizeEight = new System.Windows.Forms.RadioButton();
            this.radioButtonSizeTen = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textPlayerOne = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.playerTwoBox = new System.Windows.Forms.CheckBox();
            this.textPlayerTwo = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // radioButtonSizeSix
            // 
            this.radioButtonSizeSix.AutoSize = true;
            this.radioButtonSizeSix.Location = new System.Drawing.Point(41, 46);
            this.radioButtonSizeSix.Name = "radioButtonSizeSix";
            this.radioButtonSizeSix.Size = new System.Drawing.Size(48, 20);
            this.radioButtonSizeSix.TabIndex = 1;
            this.radioButtonSizeSix.TabStop = true;
            this.radioButtonSizeSix.Text = "6x6";
            this.radioButtonSizeSix.UseVisualStyleBackColor = true;
            this.radioButtonSizeSix.CheckedChanged += new System.EventHandler(this.radioButtonSizeSix_CheckedChanged);
            // 
            // radioButtonSizeEight
            // 
            this.radioButtonSizeEight.AutoSize = true;
            this.radioButtonSizeEight.Location = new System.Drawing.Point(130, 46);
            this.radioButtonSizeEight.Name = "radioButtonSizeEight";
            this.radioButtonSizeEight.Size = new System.Drawing.Size(48, 20);
            this.radioButtonSizeEight.TabIndex = 2;
            this.radioButtonSizeEight.TabStop = true;
            this.radioButtonSizeEight.Text = "8x8";
            this.radioButtonSizeEight.UseVisualStyleBackColor = true;
            this.radioButtonSizeEight.CheckedChanged += new System.EventHandler(this.radioButtonSizeEight_CheckedChanged);
            // 
            // radioButtonSizeTen
            // 
            this.radioButtonSizeTen.AutoSize = true;
            this.radioButtonSizeTen.Location = new System.Drawing.Point(213, 46);
            this.radioButtonSizeTen.Name = "radioButtonSizeTen";
            this.radioButtonSizeTen.Size = new System.Drawing.Size(62, 20);
            this.radioButtonSizeTen.TabIndex = 3;
            this.radioButtonSizeTen.TabStop = true;
            this.radioButtonSizeTen.Text = "10x10";
            this.radioButtonSizeTen.UseVisualStyleBackColor = true;
            this.radioButtonSizeTen.CheckedChanged += new System.EventHandler(this.radioButtonSizeTen_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1:";
            // 
            // textPlayerOne
            // 
            this.textPlayerOne.Location = new System.Drawing.Point(166, 113);
            this.textPlayerOne.Name = "textPlayerOne";
            this.textPlayerOne.Size = new System.Drawing.Size(100, 22);
            this.textPlayerOne.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "label4";
            // 
            // playerTwoBox
            // 
            this.playerTwoBox.AutoSize = true;
            this.playerTwoBox.Location = new System.Drawing.Point(41, 169);
            this.playerTwoBox.Name = "playerTwoBox";
            this.playerTwoBox.Size = new System.Drawing.Size(81, 20);
            this.playerTwoBox.TabIndex = 8;
            this.playerTwoBox.Text = "Player 2:";
            this.playerTwoBox.UseVisualStyleBackColor = true;
            this.playerTwoBox.CheckedChanged += new System.EventHandler(this.playerTwoBox_CheckedChanged);
            // 
            // textPlayerTwo
            // 
            this.textPlayerTwo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textPlayerTwo.Enabled = false;
            this.textPlayerTwo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textPlayerTwo.Location = new System.Drawing.Point(166, 167);
            this.textPlayerTwo.Name = "textPlayerTwo";
            this.textPlayerTwo.Size = new System.Drawing.Size(100, 22);
            this.textPlayerTwo.TabIndex = 9;
            this.textPlayerTwo.Text = "[Computer]";
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(200, 211);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 10;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 257);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textPlayerTwo);
            this.Controls.Add(this.playerTwoBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textPlayerOne);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonSizeTen);
            this.Controls.Add(this.radioButtonSizeEight);
            this.Controls.Add(this.radioButtonSizeSix);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonSizeSix;
        private System.Windows.Forms.RadioButton radioButtonSizeEight;
        private System.Windows.Forms.RadioButton radioButtonSizeTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textPlayerOne;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox playerTwoBox;
        private System.Windows.Forms.TextBox textPlayerTwo;
        private System.Windows.Forms.Button buttonDone;
    }
}

