using SharpDX.DirectInput;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ThirtyAnswers.Services
{
    // Adapted from: codereview.stackexchange.com/questions/68711/joystick-helper-class
    public class BuzzerService
    {
        private readonly DirectInput _DirectInput;
        private Joystick _Joystick;
        private Thread _PollingThread;

        public event EventHandler<BuzzerButtonPressedEventArgs> BuzzerPressed;

        public BuzzerService()
        {
            // Initialize DirectInput
            _DirectInput = new DirectInput();
        }

        public void StartCapture()
        {
            // Look for a Joystick
            Guid joystickGuid = Guid.Empty;
            try
            {
                foreach (var deviceInstance in _DirectInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid = deviceInstance.InstanceGuid;
                }

                // Initialize the joystick
                _Joystick = new Joystick(_DirectInput, joystickGuid);
                _Joystick.Properties.BufferSize = 128;
                _Joystick.Acquire();
                _PollingThread = new Thread(new ThreadStart(PollBuzzer));
                _PollingThread.Start();
            }
            catch
            {
                MessageBox.Show("Unable to find buzzers.");
                return;
            }

            // Spin for a while waiting for the started thread to become alive
            while (!_PollingThread.IsAlive);
        }

        public void StopCapture()
        {
            if (_PollingThread != null)
            {
                _PollingThread.Abort();
                _PollingThread.Join();
            }

            if (_Joystick != null)
            {
                _Joystick.Dispose();
            }
        }

        public void PollBuzzer()
        {
            while (true)
            {
                _Joystick.Poll();
                JoystickUpdate[] datas = _Joystick.GetBufferedData();
                foreach (JoystickUpdate state in datas)
                {
                    if (state.Offset >= JoystickOffset.Buttons0 && state.Offset <= JoystickOffset.Buttons127)
                    {
                        if (state.Value == 128)
                        {
                            BuzzerButtonPressedEventArgs args = new BuzzerButtonPressedEventArgs();
                            if (state.Offset == JoystickOffset.Buttons9)
                            {
                                args.Buzzer = Buzzer.Buzzer1;
                            }
                            else if (state.Offset == JoystickOffset.Buttons10)
                            {
                                args.Buzzer = Buzzer.Buzzer2;
                            }
                            else if (state.Offset == JoystickOffset.Buttons11)
                            {
                                args.Buzzer = Buzzer.Buzzer3;
                            }
                            args.TimeStamp = DateTime.Now;
                            OnBuzzerPressed(args);
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        protected virtual void OnBuzzerPressed(BuzzerButtonPressedEventArgs e)
        {
            BuzzerPressed?.Invoke(this, e);
        }
    }

    public class BuzzerButtonPressedEventArgs : EventArgs
    {
        public Buzzer Buzzer { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public enum Buzzer
    {
        None,
        Buzzer1,
        Buzzer2,
        Buzzer3
    }
}