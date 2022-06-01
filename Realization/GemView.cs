using Match_3.Core.Gems;
using Match_3.Core.Utils;
using System;
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
            Scale = new Vector2((float)Image.Width, (float)Image.Height);
        }


        public Gem Gem { get; }
        public Vector2 Position { get; private set; }
        public Vector2 Scale { get; private set; }


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
            Position = position;
            Drawer.SetUIElementPosition(Image, position.X, position.Y);
        }

        public void SetScale(Vector2 scale)
        {
            Scale = scale;
            Image.Width = scale.X;
            Image.Height = scale.Y;
        }

        public void Focus()
        {
            Drawer.FocusElement(Image);
        }

        public void Unfocus()
        {
            Drawer.UnfocusElement(Image);
        }
    }
}
