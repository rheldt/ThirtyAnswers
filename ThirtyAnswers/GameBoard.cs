using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ThirtyAnswers.Helpers;
using ThirtyAnswers.Models;

namespace ThirtyAnswers
{
    public partial class frmGameBoard : Form
    {
        private List<string> _AnswerLabels = new List<string> { "lblAnswer_1_1", "lblAnswer_1_2", "lblAnswer_1_3", "lblAnswer_1_4", "lblAnswer_1_5", "lblAnswer_2_1", "lblAnswer_2_2", "lblAnswer_2_3", "lblAnswer_2_4", "lblAnswer_2_5", "lblAnswer_3_1", "lblAnswer_3_2", "lblAnswer_3_3", "lblAnswer_3_4", "lblAnswer_3_5", "lblAnswer_4_1", "lblAnswer_4_2", "lblAnswer_4_3", "lblAnswer_4_4", "lblAnswer_4_5", "lblAnswer_5_1", "lblAnswer_5_2", "lblAnswer_5_3", "lblAnswer_5_4", "lblAnswer_5_5", "lblAnswer_6_1", "lblAnswer_6_2", "lblAnswer_6_3", "lblAnswer_6_4", "lblAnswer_6_5" };
        private int _AnswerLabelDisplayCount = 0;
        private readonly List<int> _AnswerSmallValues = new List<int> { 100, 200, 300, 400, 500 };
        private readonly List<int> _AnswerLargeValues = new List<int> { 200, 400, 600, 800, 1000 };

        public Game Game { get; set; }

        public frmGameBoard()
        {
            InitializeComponent();
        }

        private void frmGameBoard_Load(object sender, EventArgs e)
        {
            LoadBoard();
            AudioHelper.PlayNewRound();
        }

        private void tmrLoadBoard_Tick(object sender, EventArgs e)
        {
            _AnswerLabelDisplayCount++;

            if (_AnswerLabelDisplayCount >= 30)
            {
                tmrLoadBoard.Enabled = false;
                LoadCategories();
                return;
            }

            if (_AnswerLabelDisplayCount >= 0)
            {
                Label lblAnswer = (Label)tableLayoutPanel.Controls.Find(_AnswerLabels[_AnswerLabelDisplayCount], true)[0];
                if (lblAnswer != null)
                {
                    string[] parts = lblAnswer.Name.Split('_');
                    if (parts.Length == 3)
                    {
                        int index = Convert.ToInt32(parts[2]);
                        if (this.Game.UseDoubleValues)
                        {
                            lblAnswer.Text = "$" + _AnswerLargeValues[index - 1].ToString();
                            lblAnswer.Tag = _AnswerLargeValues[index - 1];
                        }
                        else
                        {
                            lblAnswer.Text = "$" + _AnswerSmallValues[index - 1].ToString();
                            lblAnswer.Tag = _AnswerSmallValues[index - 1];
                        }
                    }
                }
            }
        }

        public void LoadBoard()
        {
            _AnswerLabelDisplayCount = -5;
            _AnswerLabels = ListHelper.ShuffleList(_AnswerLabels);
            tmrLoadBoard.Enabled = true;
            lblPlayer1.Text = Game.Player1.Name;
            lblPlayer1.Enabled = false;
            lblPlayer2.Text = Game.Player2.Name;
            lblPlayer3.Enabled = false;
            lblPlayer3.Text = Game.Player3.Name;
            lblPlayer3.Enabled = false;
            prbTimer.Value = 0;
            pnlAnswerDisplay.Visible = false;
        }

        public void LoadCategories()
        {
            lblCategory_1.Text = this.Game.Category1.Name.ToUpper();
            lblCategory_2.Text = this.Game.Category2.Name.ToUpper();
            lblCategory_3.Text = this.Game.Category3.Name.ToUpper();
            lblCategory_4.Text = this.Game.Category4.Name.ToUpper();
            lblCategory_5.Text = this.Game.Category5.Name.ToUpper();
            lblCategory_6.Text = this.Game.Category6.Name.ToUpper();
        }

        private void lblAnswer_Click(object sender, EventArgs e)
        {
            Label lblAnswer = (Label)sender;
            if (lblAnswer != null)
            {
                // TODO2023
            }
        }

        private void lblAnswer_MouseEnter(object sender, EventArgs e)
        {
            Label lblAnswer = (Label)sender;
            if (lblAnswer != null)
            {
                lblAnswer.ForeColor = Color.White;
                lblAnswer.Cursor = Cursors.Default;
                if (!string.IsNullOrEmpty(lblAnswer.Text))
                {
                    lblAnswer.Cursor = Cursors.Hand;
                }
            }
        }

        private void lblAnswer_MouseLeave(object sender, EventArgs e)
        {
            Label lblAnswer = (Label)sender;
            if (lblAnswer != null)
            {
                lblAnswer.ForeColor = Color.FromArgb(215, 160, 74);
            }
        }
    }
}