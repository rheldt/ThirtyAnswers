using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ThirtyAnswers.Helpers;
using ThirtyAnswers.Models;

namespace ThirtyAnswers
{
    public partial class frmGameControl : Form
    {
        private frmGameBoard _GameBoard;
        private string _GamesDirectory = "C:\\ThirtyAnswersGames"; // Path.GetDirectoryName(Application.StartupPath);
        private int _Player1Score = 0;
        private int _Player2Score = 0;
        private int _Player3Score = 0;

        private string _ActiveCategoryName = string.Empty;
        private int _ActiveAmount = 0;
        private CategoryItem _ActiveCategoryItem;
        private int _ActivePlayerNumber = 0;

        public frmGameControl()
        {
            InitializeComponent();
        }

        private void frmGameControl_Load(object sender, EventArgs e)
        {
            pnlGameStatus.BackColor = Color.FromArgb(128, 255, 255, 255);
            pnlGameStatus.Visible = false;
            btnEnd.Enabled = false;

            try
            {
                string[] games = Directory.GetFiles(_GamesDirectory, "*.30a.json");
                foreach (string game in games)
                {
                    string gameName = Path.GetFileName(game).Replace(".30a.json", "");
                    cmbGame.Items.Add(gameName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find game files: " + ex.Message, this.Text);
            }

            _ActiveCategoryName = string.Empty;
            _ActiveAmount = 0;
            _ActiveCategoryItem = null;
            _ActivePlayerNumber = 0;
            UpdateUI();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Make sure player names are entered
            if (txtPlayer1.Text.Trim() == string.Empty || txtPlayer2.Text.Trim() == string.Empty || txtPlayer3.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter names for each player.", this.Text);
                return;
            }

            // Make sure player names are unique
            if (txtPlayer1.Text.Trim() == txtPlayer2.Text.Trim() || txtPlayer1.Text.Trim() == txtPlayer3.Text.Trim() || txtPlayer2.Text.Trim() == txtPlayer3.Text.Trim())
            {
                MessageBox.Show("Please make sure each player name is unique.", this.Text);
                return;
            }

            // Make sure game file is selected
            if (cmbGame.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a game to play.", this.Text);
                return;
            }

            // Load game file and deserialize it
            Game game = new Game();
            try
            {
                string gameFile = Path.Combine(_GamesDirectory, cmbGame.SelectedItem.ToString() + ".30a.json");
                string gameJson = File.ReadAllText(gameFile);
                game = JsonConvert.DeserializeObject<Game>(gameJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load game file: " + ex.Message, this.Text);
                this.Close();
                return;
            }
            game.Player1 = new Player(txtPlayer1.Text.Trim());
            game.Player2 = new Player(txtPlayer2.Text.Trim());
            game.Player3 = new Player(txtPlayer3.Text.Trim());
            game.UseDoubleValues = chkDoubleValues.Checked;

            // Load and show game board
            _GameBoard = new frmGameBoard();
            _GameBoard.Game = game;
            _GameBoard.AnswerSelected += GameBoard_AnswerSelected;
            _GameBoard.PlayerRingIn += GameBoard_PlayerRingIn;
            _GameBoard.Show();

            pnlGameStatus.Visible = true;
            txtPlayer1.Enabled = false;
            txtPlayer2.Enabled = false;
            txtPlayer3.Enabled = false;
            cmbGame.Enabled = false;
            btnEnd.Enabled = true;
            btnStart.Enabled = false;
        }

        private void GameBoard_AnswerSelected(object sender, AnswerSelectedEventArgs e)
        {
            _ActiveCategoryName = e.CategoryName;
            _ActiveAmount = e.Amount;
            _ActiveCategoryItem = e.CategoryItem;
            _ActivePlayerNumber = 0;
            UpdateUI();
        }

        private void GameBoard_PlayerRingIn(object sender, PlayerRingInEventArgs e)
        {
            if (_ActiveCategoryItem != null && _ActivePlayerNumber == 0)
            {
                _ActivePlayerNumber = e.PlayerNumber;
                UpdateUI();
                AudioHelper.PlayRingIn();
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("End game in progress?", this.Text, MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                _GameBoard.Close();
                pnlGameStatus.Visible = false;
                txtPlayer1.Enabled = true;
                txtPlayer2.Enabled = true;
                txtPlayer3.Enabled = true;
                cmbGame.Enabled = true;
                btnEnd.Enabled = false;
                btnStart.Enabled = true;
            }
        }

        private void btnCorrect_Click(object sender, EventArgs e)
        {
        }

        private void btnWrong_Click(object sender, EventArgs e)
        {
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            _GameBoard.HideAnswerDisplay();
            _ActiveCategoryName = string.Empty;
            _ActiveAmount = 0;
            _ActiveCategoryItem = null;
            _ActivePlayerNumber = 0;
            UpdateUI();
        }

        private void btnShowBoard_Click(object sender, EventArgs e)
        {
        }

        private void UpdateUI()
        {
            // Active player
            txtPlayer1.BorderStyle = (_ActivePlayerNumber == 1 ? BorderStyle.FixedSingle : BorderStyle.Fixed3D);
            txtPlayer1.BackColor = (_ActivePlayerNumber == 1 ? Color.Yellow : Color.White);
            txtPlayer2.BorderStyle = (_ActivePlayerNumber == 2 ? BorderStyle.FixedSingle : BorderStyle.Fixed3D);
            txtPlayer2.BackColor = (_ActivePlayerNumber == 2 ? Color.Yellow : Color.White);
            txtPlayer3.BorderStyle = (_ActivePlayerNumber == 3 ? BorderStyle.FixedSingle : BorderStyle.Fixed3D);
            txtPlayer3.BackColor = (_ActivePlayerNumber == 3 ? Color.Yellow : Color.White);

            // Player scores
            lblPlayer1Score.Text = _Player1Score.ToString("C0");
            lblPlayer2Score.Text = _Player2Score.ToString("C0");
            lblPlayer3Score.Text = _Player3Score.ToString("C0");

            // Category name
            lblCategory.Text = _ActiveCategoryName;

            // Amount of active answer
            if (_ActiveAmount > 0)
            {
                lblValue.Text = _ActiveAmount.ToString("C0");
            }
            else
            {
                lblValue.Text = string.Empty;
            }

            // Answer copy
            if (_ActiveCategoryItem != null)
            {
                lblAnswer.Text = _ActiveCategoryItem.Answer;
                lblQuestion.Text = _ActiveCategoryItem.Question;
            }
            else
            {
                lblAnswer.Text = string.Empty;
                lblQuestion.Text = string.Empty;
            }
        }
    }
}