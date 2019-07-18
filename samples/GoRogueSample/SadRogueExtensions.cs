using SadConsole;

namespace GoRogueSample
{
    public static class SadRogueExtensions
    {
        public static ScrollingConsole CreateRenderer(
            this BasicMap map, 
            int x, int y, int w, int h, 
            Font font = null)
        {
            return map.CreateRenderer(
                new Microsoft.Xna.Framework.Rectangle(x, y, w, h),
                font ?? SadConsole.Global.FontDefault
            );
        }
    }
}
