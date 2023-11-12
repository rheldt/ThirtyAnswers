using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;
using ThirtyAnswers.Models;

namespace ThirtyAnswers
{
    public partial class frmGameControl : Form
    {
        private frmGameBoard _GameBoard;

        private string _GamesDirectory = Path.GetDirectoryName(Application.StartupPath);

        public frmGameControl()
        {
            InitializeComponent();
        }

        private void frmGameControl_Load(object sender, EventArgs e)
        {
            btnEnd.Enabled = false;

            try
            {
                string[] games = Directory.GetFiles(_GamesDirectory, "*.30a");
                foreach (string game in games)
                {
                    string gameName = Path.GetFileNameWithoutExtension(game);
                    cmbGame.Items.Add(gameName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to find game files: " + ex.Message, this.Text);
            }
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
                string gameFile = Path.Combine(_GamesDirectory, cmbGame.SelectedItem.ToString() + ".30a");
                string gameJson = File.ReadAllText(gameFile);
                game = JsonConvert.DeserializeObject<Game>(gameJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load game file: " + ex.Message, this.Text);
            }
            game.Player1 = new Player(txtPlayer1.Text.Trim());
            game.Player2 = new Player(txtPlayer2.Text.Trim());
            game.Player3 = new Player(txtPlayer3.Text.Trim());
            game.UseDoubleValues = chkDoubleValues.Checked;

            // Load and show game board
            _GameBoard = new frmGameBoard();
            _GameBoard.Game = game;
            _GameBoard.Show();

            btnEnd.Enabled = true;
            btnStart.Enabled = false;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("End game in progress?", this.Text, MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                _GameBoard.Close();
                btnEnd.Enabled = false;
                btnStart.Enabled = true;
            }
        }
    }
}