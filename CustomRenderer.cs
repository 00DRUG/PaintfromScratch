using System.Drawing;
using System.Windows.Forms;

public class CustomToolStripRenderer : ToolStripProfessionalRenderer
{
    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
        ToolStripButton btn = e.Item as ToolStripButton;
        if (btn != null)
        {

            using (SolidBrush brush = new SolidBrush(btn.BackColor))
            {
                e.Graphics.FillRectangle(brush, e.Item.Bounds);
            }

            if (btn.Image != null)
            {
                e.Graphics.DrawImage(btn.Image, new Point(23, 22));
            }
        }
    }
}


