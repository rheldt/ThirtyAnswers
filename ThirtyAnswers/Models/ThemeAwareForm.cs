using Dark.Net;
using System.Drawing;

namespace ThirtyAnswers.Models
{
    public class ThemeAwareForm : System.Windows.Forms.Form
    {
        public ThemeAwareForm()
        {
            DarkNet.Instance.EffectiveCurrentProcessThemeIsDarkChanged += (_, isDarkTheme) => RenderTheme(isDarkTheme);
            RenderTheme(DarkNet.Instance.EffectiveCurrentProcessThemeIsDark);
        }

        private void RenderTheme(bool isDarkTheme)
        {
            BackColor = isDarkTheme ? Color.FromArgb(19, 19, 19) : Color.White;
            ForeColor = isDarkTheme ? Color.White : Color.Black;
        }
    }
}