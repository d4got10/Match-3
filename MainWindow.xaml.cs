using Match_3.Core;
using Match_3.Core.Utils;
using Match_3.Realization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Match_3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Drawer = new CanvasDrawer(GameCanvas);
            Timer = new DispatcherTimer();
        }


        private readonly CanvasDrawer Drawer;
        private readonly DispatcherTimer Timer;
        private GameCore _core;
        private IGemViewsContainer _viewsContainer;

        private void OnTimerTick(object sender, System.EventArgs e)
        {
        }

        private void OnMainWindowLoaded(object sender, System.EventArgs e)
        {
            Timer.Tick += OnTimerTick;

            var settings = new Settings { CellSize = 64 };

            var gemViewsContainer = new GemViewsContainer();
            _viewsContainer = gemViewsContainer;
            var gemFactory = new GemWithViewsFactory(Drawer, gemViewsContainer, settings);

            var generationSystem = new GenerationSystem(gemFactory);
            var animationSystem = new AnimationSystem(gemViewsContainer, settings);
            var sheddingSystem = new SheddingSystem(animationSystem);
            var matchSystem = new MatchSystem();

            _core = new GameCore(new Vector2Int(8, 8), sheddingSystem, matchSystem, generationSystem, animationSystem);

            _core.Start();
            Timer.Start();
        }
    }
}