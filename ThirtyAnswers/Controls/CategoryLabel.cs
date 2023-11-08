using System.Drawing;
using System.Windows.Forms;

namespace ThirtyAnswers.Controls
{
    public class CategoryLabel : Label
    {
        public CategoryLabel()
        {
            this.Dock = DockStyle.Fill;
            this.Font = new Font("Swiss 911 Compressed", 19.8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = Color.White;
            this.Image = Properties.Resources.BlueBoxDark;
            this.Location = new Point(3, 3);
            this.Margin = new Padding(3);
            this.Size = new Size(127, 69);
            this.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}