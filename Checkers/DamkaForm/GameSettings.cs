using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ex02;

namespace DamkaForm
{
    public partial class GameSettings : Form
    {
        private int m_SizeOfBoard = 6;
        private bool m_IsPlayer = false;

        public GameSettings()
        {
            InitializeComponent();
            this.radioButtonSizeSix.Click += new EventHandler(this.radioButtonSizeSix_CheckedChanged);
            this.radioButtonSizeEight.Click += new EventHandler(this.radioButtonSizeEight_CheckedChanged);
            this.radioButtonSizeTen.Click += new EventHandler(this.radioButtonSizeTen_CheckedChanged);
            this.playerTwoBox.Click += new EventHandler(this.playerTwoBox_CheckedChanged);
            this.buttonDone.Click += new EventHandler(this.buttonDone_Click);
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            this.playerTwoBox.Click += new EventHandler(this.playerTwoBox_CheckedChanged);
            if (validatePlayerName()) 
            {
                Damka startGame = new Damka(this.textPlayerOne.Text, this.textPlayerTwo.Text, this.m_SizeOfBoard, this.m_IsPlayer);
                buttonDone.DialogResult = DialogResult.OK;               
                this.Hide();
                startGame.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please write a valid player name", "Wrong input name!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
            
        }

        private void playerTwoBox_CheckedChanged(object sender, EventArgs e)
        {
            if (playerTwoBox.Checked)
            {
                this.textPlayerTwo.Enabled = true;
                this.textPlayerTwo.Text = "";              
            }
            else
            {
                this.textPlayerTwo.Enabled = false;
                this.textPlayerTwo.Text = "[Computer]";
            }

            this.m_IsPlayer = this.playerTwoBox.Checked;
        }

        private bool validatePlayerName()
        {
            bool isWhiteSpace = this.textPlayerOne.Text.Any(Char.IsWhiteSpace);

            return (!(isWhiteSpace || this.textPlayerOne.Text.Length > 10 ));   
        }

        private void radioButtonSizeSix_CheckedChanged(object sender, EventArgs e)
        {
            this.m_SizeOfBoard = 6;
        }

        private void radioButtonSizeEight_CheckedChanged(object sender, EventArgs e)
        {
            this.m_SizeOfBoard = 8;
        }

        private void radioButtonSizeTen_CheckedChanged(object sender, EventArgs e)
        {
            this.m_SizeOfBoard = 10;
        }

    }
}
