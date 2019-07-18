using GoRogueSample.Monsters;
using Microsoft.Xna.Framework;

using SadConsole;
using System;
using Console = SadConsole.Console;

namespace GoRogueSample.Screens
{
    class StatsPanel : Console
    {
        public StatsPanel(int width, int height)
            : base(width, height)
        {
            this.Redraw();
        }

        public void Redraw()
        {
            // Draw border
            DrawLine(new Point(0, 0), new Point(0, Height), Color.LightGray, glyph: 186);
            SetGlyph(0, Height - 10, 185, Color.LightGray); // FIXME: This is assuming the logs panel is 10px

            // Draw Stats Info
            int row = 1;

            Print(2, row++, "Player", Color.White, Color.Black);
            Print(3, row++, $"Health : {999}", Color.White, Color.Black);
            Print(3, row++, $"Gold   : {999}", Color.White, Color.Black);
            row += 2;

            Print(2, row++, "Level".Align(HorizontalAlignment.Center, Width - 4, '_'), Color.White, Color.Black);

            row += 2;
            Print(2, row++, "Floor", Color.White, Color.Black);
            Print(3, row++, $"Depth  : {1}", Color.White, Color.Black);

            row += 2;
            Print(2, row++, "Monsters", Color.White, Color.Black);
            Print(3, row++, $"Left   : {999}", Color.White, Color.Black);
            Print(3, row++, $"Killed : {999}", Color.White, Color.Black);

            // Draw Game Info
            row = Height - 8;
            Print(2, row++, "Monsters".Align(HorizontalAlignment.Center, Width - 4, '_'), Color.White, Color.Black);
            row += 1;

            DrawMonsterTypeInfo(row++, Rat.DefaultChar, Rat.DefaultColor, "Rat");
            DrawMonsterTypeInfo(row++, Goblin.DefaultChar, Goblin.DefaultColor, "Goblin");
            DrawMonsterTypeInfo(row++, Minotaur.DefaultChar, Minotaur.DefaultColor, "Minotaur");
        }

        private void DrawMonsterTypeInfo(int row, char glyph, Color color, string description)
        {
            Print(3, row, glyph.ToString(), color, Color.Black);
            Print(5, row, $"- {description}", Color.White, Color.Black);
        }
    }
}
