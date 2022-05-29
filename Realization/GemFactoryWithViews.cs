using Match_3.Core.Gems;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Match_3.Realization
{
    public class GemWithViewsFactory : IGemFactory
    {
        public GemWithViewsFactory(CanvasDrawer drawer, IGemViewsContainer viewsContainer, Settings settings)
        {
            Drawer = drawer;
            ViewsContainer = viewsContainer;
            Settings = settings;
        }

        private CanvasDrawer Drawer { get; }
        private IGemViewsContainer ViewsContainer { get; }
        private Settings Settings { get; }
        private Dictionary<GemType, ImageSource> ImageSources = new Dictionary<GemType, ImageSource>
        {
            { GemType.Red, new BitmapImage(new Uri("Assets/Red.png", UriKind.Relative)) },
            { GemType.Yellow, new BitmapImage(new Uri("Assets/Yellow.png", UriKind.Relative)) },
            { GemType.Green, new BitmapImage(new Uri("Assets/Green.png", UriKind.Relative)) },
            { GemType.Blue, new BitmapImage(new Uri("Assets/Blue.png", UriKind.Relative)) },
            { GemType.Violet, new BitmapImage(new Uri("Assets/Violet.png", UriKind.Relative)) },
        };


        public Gem Create(GemType type)
        {
            var gem = CreateGem(type);
            var view = CreateGemView(gem);
            ViewsContainer.Add(view);
            view.Load();

            return gem;
        }


        private GemView CreateGemView(Gem gem)
        {
            var image = new Image
            {
                Width = Settings.CellSize,
                Height = Settings.CellSize
            };
            image.Source = ImageSources[gem.Type];

            var view = new GemView(gem, image, Drawer);

            return view;
        }

        private Gem CreateGem(GemType type)
        {
            switch (type)
            {
                case GemType.Red: return new RedGem();
                case GemType.Yellow: return new YellowGem();
                case GemType.Green: return new GreenGem();
                case GemType.Blue: return new BlueGem();
                case GemType.Violet: return new VioletGem();
                default: throw new System.NotImplementedException();
            }
        }
    }
}
