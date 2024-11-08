using System;
using System.Windows.Forms;
using ThirtyAnswers.Services;

namespace ThirtyAnswers
{
    public partial class BuzzerTest : Form
    {
        private BuzzerService _BuzzerService;
        private Buzzer _ActivatedBuzzer;

        public BuzzerTest()
        {
            InitializeComponent();
            _ActivatedBuzzer = Buzzer.None;
            _BuzzerService = new BuzzerService();
            _BuzzerService.BuzzerPressed += BuzzerService_BuzzerPressed;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _BuzzerService.StartCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The buzzers probably aren't connected. Please connect and restart Buzzer Commander.\n\n" + ex.Message, "This is embarrassing", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _BuzzerService.StopCapture();
        }

        private void BuzzerService_BuzzerPressed(object sender, BuzzerButtonPressedEventArgs e)
        {
            if (_ActivatedBuzzer != Buzzer.None)
            {
                return;
            }

            Action buzzerAction = () =>
            {
                // Check for buzzer
                _ActivatedBuzzer = e.Buzzer;
                switch (e.Buzzer)
                {
                    case Buzzer.Buzzer1:
                        label1.Text = "Buzzer 1";
                        break;
                    case Buzzer.Buzzer2:
                        label1.Text = "Buzzer 2";
                        break;
                    case Buzzer.Buzzer3:
                        label1.Text = "Buzzer 3";
                        break;
                }

                // Reset buzzers after 5 seconds
                Timer resetTimer = new Timer();
                resetTimer.Interval = 5000;
                resetTimer.Tick += (s, en) =>
                {
                    label1.Text = "None";
                    _ActivatedBuzzer = Buzzer.None;
                    resetTimer.Stop();
                };
                resetTimer.Start();
            };

            if (this.InvokeRequired)
            {
                this.Invoke(buzzerAction);
            }
        }
    }
}
