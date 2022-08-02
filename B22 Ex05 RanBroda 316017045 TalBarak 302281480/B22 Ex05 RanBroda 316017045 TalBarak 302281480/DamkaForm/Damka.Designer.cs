namespace DamkaForm
{
    partial class Damka
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
            this.nameOfPlayerOne = new System.Windows.Forms.Label();
            this.nameOfPlayerTwo = new System.Windows.Forms.Label();
            this.scorePlayerOne = new System.Windows.Forms.Label();
            this.scorePlayerTwo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameOfPlayerOne
            // 
            this.nameOfPlayerOne.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nameOfPlayerOne.AutoSize = true;
            this.nameOfPlayerOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.nameOfPlayerOne.Location = new System.Drawing.Point(38, 34);
            this.nameOfPlayerOne.Name = "nameOfPlayerOne";
            this.nameOfPlayerOne.Size = new System.Drawing.Size(73, 17);
            this.nameOfPlayerOne.TabIndex = 0;
            this.nameOfPlayerOne.Text = "Player 1:";
            this.nameOfPlayerOne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameOfPlayerTwo
            // 
            this.nameOfPlayerTwo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nameOfPlayerTwo.AutoSize = true;
            this.nameOfPlayerTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.nameOfPlayerTwo.Location = new System.Drawing.Point(271, 34);
            this.nameOfPlayerTwo.Name = "nameOfPlayerTwo";
            this.nameOfPlayerTwo.Size = new System.Drawing.Size(73, 17);
            this.nameOfPlayerTwo.TabIndex = 1;
            this.nameOfPlayerTwo.Text = "Player 2:";
            this.nameOfPlayerTwo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scorePlayerOne
            // 
            this.scorePlayerOne.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.scorePlayerOne.AutoSize = true;
            this.scorePlayerOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.scorePlayerOne.Location = new System.Drawing.Point(132, 34);
            this.scorePlayerOne.Name = "scorePlayerOne";
            this.scorePlayerOne.Size = new System.Drawing.Size(17, 17);
            this.scorePlayerOne.TabIndex = 2;
            this.scorePlayerOne.Text = "0";
            this.scorePlayerOne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scorePlayerTwo
            // 
            this.scorePlayerTwo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.scorePlayerTwo.AutoSize = true;
            this.scorePlayerTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.scorePlayerTwo.Location = new System.Drawing.Point(366, 34);
            this.scorePlayerTwo.Name = "scorePlayerTwo";
            this.scorePlayerTwo.Size = new System.Drawing.Size(17, 17);
            this.scorePlayerTwo.TabIndex = 3;
            this.scorePlayerTwo.Text = "0";
            this.scorePlayerTwo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Damka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 401);
            this.Controls.Add(this.scorePlayerTwo);
            this.Controls.Add(this.scorePlayerOne);
            this.Controls.Add(this.nameOfPlayerTwo);
            this.Controls.Add(this.nameOfPlayerOne);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Damka";
            this.Text = "Damka";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameOfPlayerOne;
        private System.Windows.Forms.Label nameOfPlayerTwo;
        private System.Windows.Forms.Label scorePlayerOne;
        private System.Windows.Forms.Label scorePlayerTwo;
    }
}