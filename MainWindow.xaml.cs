using Match_3.Core;
using Match_3.Core.Utils;
using Match_3.Realization;
using Match_3.Realization.Animation;
using Match_3.Utils;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        }


        private readonly CanvasDrawer Drawer;
        private GameCore _core;
        private IGemViewsContainer _viewsContainer;
        private float _time;
        private Settings _settings;

        private void OnMainWindowLoaded(object sender, System.EventArgs e)
        {
            _time = 0;

            _settings = new Settings { CellSize = 64 };

            var animationDurationSettings = new DurationSettings { Appearence = 0f, Movement = 0.25f, Swap = 0.5f, Destruction = 0.5f };

            var gemViewsContainer = new GemViewsContainer();
            _viewsContainer = gemViewsContainer;
            var gemFactory = new GemWithViewsFactory(Drawer, gemViewsContainer, _settings);

            var animationSystem = new AnimationSystem(gemViewsContainer, _settings, animationDurationSettings);
            var generationSystem = new GenerationSystem(gemFactory, animationSystem);
            var sheddingSystem = new SheddingSystem(animationSystem);
            var matchSystem = new MatchSystem(animationSystem);
            var selectionSystem = new SelectionSystem(animationSystem);
            var swappingSystem = new SwappingSystem(animationSystem);

            var dependencies = new GameCoreDependecies
            {
                AnimationSystem = animationSystem,
                GenerationSystem = generationSystem,
                SheddingSystem = sheddingSystem,
                SelectionSystem = selectionSystem,
                MatchSystem = matchSystem,
                SwappingSystem = swappingSystem,
            };

            _core = new GameCore(new Vector2Int(8, 9), dependencies);

            _core.Start();

            TickLoop(1);
        }

        private async void TickLoop(int interval)
        {
            while (true)
            {
                var time = DateTime.Now;
                await Task.Delay(interval);
                var delta = DateTime.Now - time;
                OnTimerTick((float)delta.TotalSeconds);
            }
        }

        private void OnTimerTick(float interval)
        {
            _time += interval;
            _core.Tick(_time);
        }

        private void GameCanvas_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var point = Mouse.GetPosition(GameCanvas);

            var position = new Vector2((float)point.X, (float)point.Y);
            var cellPosition = PositionUtils.ConvertWorldToGridPosition(position, _settings);
            cellPosition.Y = _core.GridSize.Y - 2 - cellPosition.Y;

            _core.OnClick(cellPosition);
        }
    }
}