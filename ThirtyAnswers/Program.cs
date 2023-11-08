using Dark.Net;
using System;
using System.Windows.Forms;

namespace ThirtyAnswers
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IDarkNet darkNet = DarkNet.Instance;
            darkNet.SetCurrentProcessTheme(Theme.Auto, new ThemeOptions());            
            Form mainForm = new frmMainBoard();
            darkNet.SetWindowThemeForms(mainForm, Theme.Auto);
            Application.Run(mainForm);
        }
    }
}
