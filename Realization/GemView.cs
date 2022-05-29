using Match_3.Core.Gems;
using Match_3.Core.Utils;
using System.Windows.Controls;


namespace Match_3.Realization
{
    public class GemView
    {
        public GemView(Gem gem, Image image, CanvasDrawer drawer)
        {
            Gem = gem;
            Image = image;
            Drawer = drawer;
        }


        public Gem Gem { get; }


        private Image Image { get; }
        private CanvasDrawer Drawer { get; }


        public void Load()
        {
            Drawer.AddUIElementToCanvas(Image);
        }

        public void Unload()
        {
            Drawer.RemoveUIElementToCanvas(Image);
        }

        public void SetPosition(Vector2 position)
        {
            Drawer.SetUIElementPosition(Image, position.X, position.Y);
        }
    }
}
