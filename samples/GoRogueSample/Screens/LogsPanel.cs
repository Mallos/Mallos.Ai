using Microsoft.Xna.Framework;

using SadConsole;
using System;
using Console = SadConsole.Console;

namespace GoRogueSample.Screens
{
    class LogsPanel : Console
    {
        public LogsPanel(int width, int height)
            : base(width, height)
        {
            this.Redraw();
        }

        public void Redraw()
        {
            // Draw border
            DrawLine(new Point(0, 0), new Point(Width, 0), Color.LightGray, glyph: 205);
        }
    }
}
