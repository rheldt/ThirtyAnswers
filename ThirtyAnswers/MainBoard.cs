using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ThirtyAnswers.Helpers;
using ThirtyAnswers.Models;

namespace ThirtyAnswers
{
    public partial class frmMainBoard : ThemeAwareForm
    {
        private bool _UseLargeValues = false;
        private List<string> _AnswerLabels = new List<string> { "lblAnswer_1_1", "lblAnswer_1_2", "lblAnswer_1_3", "lblAnswer_1_4", "lblAnswer_1_5", "lblAnswer_2_1", "lblAnswer_2_2", "lblAnswer_2_3", "lblAnswer_2_4", "lblAnswer_2_5", "lblAnswer_3_1", "lblAnswer_3_2", "lblAnswer_3_3", "lblAnswer_3_4", "lblAnswer_3_5", "lblAnswer_4_1", "lblAnswer_4_2", "lblAnswer_4_3", "lblAnswer_4_4", "lblAnswer_4_5", "lblAnswer_5_1", "lblAnswer_5_2", "lblAnswer_5_3", "lblAnswer_5_4", "lblAnswer_5_5", "lblAnswer_6_1", "lblAnswer_6_2", "lblAnswer_6_3", "lblAnswer_6_4", "lblAnswer_6_5" };
        private List<int> _AnswerSmallValues = new List<int> { 100, 200, 300, 400, 500 };
        private List<int> _AnswerLargeValues = new List<int> { 200, 400, 600, 800, 1000 };
        private int _AnswerLabelDisplayCount = 0;

        public frmMainBoard()
        {
            InitializeComponent();
        }

        private void frmMainBoard_Load(object sender, EventArgs e)
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
                        if (_UseLargeValues)
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
        }
    }
}
