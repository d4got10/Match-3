using System.Windows;
using System.Windows.Controls;

namespace Match_3
{
    public class CanvasDrawer
    {
        public CanvasDrawer(Canvas canvas)
        {
            Target = canvas;
        }


        private readonly Canvas Target;


        public void AddUIElementToCanvas(UIElement element)
        {
            Target.Children.Add(element);
        }

        public void RemoveUIElementToCanvas(UIElement element)
        {
            Target.Children.Remove(element);
        }

        public void SetUIElementPosition(UIElement element, int x, int y)
        {
            Canvas.SetTop(element, y);
            Canvas.SetLeft(element, x);
        }
    }
}
