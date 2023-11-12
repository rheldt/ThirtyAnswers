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
            Form form = new frmGameControl();
            Application.Run(form);
        }
    }
}