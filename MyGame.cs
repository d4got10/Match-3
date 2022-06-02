using Match_3.Core;
using Match_3.Core.Utils;
using Match_3.Realization;
using Match_3.Realization.Animation;
using Match_3.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Match_3
{
    public class MyGame
    {
        public MyGame(Canvas canvas)
        {
            GameCanvas = canvas;
            Drawer = new CanvasDrawer(canvas);
        }


        public event Action<ITimeProvider> Ticked;
        public event Action<int> ScoreChanged;


        private readonly Canvas GameCanvas;
        private readonly CanvasDrawer Drawer;
        private GameCore _core;
        private GemViewsContainer _viewsContainer;
        private float _time;
        private Settings _settings;


        public void Load()
        {
            _time = 0;

            var dependencies = CreateGameCoreDependecies();

            _core = new GameCore(new Vector2Int(8, 9), dependencies);

            _core.Start();

            TickLoop(1);
        }
        public void Unload()
        {
            _core.Unload();
            _viewsContainer.Unload();
        }

        public void Pause()
        {
            _core.Pause();
        }

        public void OnMouseDown()
        {
            var point = Mouse.GetPosition(GameCanvas);

            var position = new Vector2((float)point.X, (float)point.Y);
            var cellPosition = PositionUtils.ConvertWorldToGridPosition(position, _settings);
            cellPosition.Y = _core.GridSize.Y - 2 - cellPosition.Y;

            _core.OnClick(cellPosition);
        }

        private async void TickLoop(int interval)
        {
            while (_core.IsRunning)
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
            Ticked?.Invoke(_core);
        }

        private GameCoreDependecies CreateGameCoreDependecies()
        {
            _settings = new Settings { CellSize = 64 };

            var animationDurationSettings = new DurationSettings { Appearence = 0f, Movement = 0.15f, Swap = 0.25f, Destruction = 0.25f };

            var gemViewsContainer = new GemViewsContainer();
            _viewsContainer = gemViewsContainer;
            var gemFactory = new GemWithViewsFactory(Drawer, gemViewsContainer, _settings);

            var scoreSystem = new ScoreSystem();
            scoreSystem.ScoreChanged += score => ScoreChanged?.Invoke(score);

            var animationSystem = new AnimationSystem(gemViewsContainer, _settings, animationDurationSettings);
            var generationSystem = new GenerationSystem(gemFactory, animationSystem);
            var sheddingSystem = new SheddingSystem(animationSystem);
            var matchSystem = new MatchSystem(animationSystem, scoreSystem);
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
                ScoreSystem = scoreSystem
            };
            return dependencies;
        }
    }
}