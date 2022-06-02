using Match_3.Core.Utils;

namespace Match_3.Core
{
    public class GameCore : ITimeProvider
    {
        public GameCore(Vector2Int gridSize, GameCoreDependecies dependecies)
        {
            GridSize = gridSize;

            Dependecies = dependecies;

            IsRunning = false;

            AnimationSystem.AnimationStarted += OnAnimationStarted;
            AnimationSystem.AnimationEnded += OnAnimationEnded;
        }

        
        public float Time { get; private set; }
        public float DeltaTime { get; private set; }
        public Vector2Int GridSize { get; }
        public bool IsRunning { get; private set; }


        protected ISheddingSystem SheddingSystem => Dependecies.SheddingSystem;
        protected IMatchSystem MatchSystem => Dependecies.MatchSystem;
        protected IGenerationSystem GenerationSystem => Dependecies.GenerationSystem;
        protected IAnimationSystem AnimationSystem => Dependecies.AnimationSystem;
        protected ISelectionSystem SelectionSystem => Dependecies.SelectionSystem;
        protected ISwappingSystem SwappingSystem => Dependecies.SwappingSystem;

        protected GameCoreDependecies Dependecies { get; }


        private GameGrid _grid;
        private bool _animationEnded;
        private bool _swaped;


        public void Tick(float time)
        {
            if (IsRunning == false) return;

            DeltaTime = time - Time;
            Time = time;

            if (_animationEnded)
            {
                GenerationSystem.Generate(_grid);
                if (SheddingSystem.Shed(_grid) == false)
                {
                    if (MatchSystem.DestroyMatches(_grid) == false && _swaped)
                    {
                        SwappingSystem.FailLastSwap(_grid);
                    }
                    _swaped = false;
                }

                AnimationSystem.Start();
            }
            else
            {
                AnimationSystem.Tick(this);
            }
        }

        public void Start()
        {
            _grid = new GameGrid(GridSize);

            IsRunning = true;
        }

        public void Pause()
        {
            IsRunning = false;
            AnimationSystem.Pause();
        }

        public void Resume()
        {
            IsRunning = true;
            AnimationSystem.Resume();
        }

        public void Unload()
        {
            IsRunning = false;
            AnimationSystem.End();
        }

        public void OnClick(Vector2Int cellPosition)
        {
            if (IsRunning == false) return;
            if (SelectionSystem.Selected == null && _animationEnded == false) return;

            if (_grid.PositionIsInsidePlayableGrid(cellPosition))
            {
                var cell = _grid[cellPosition];
                if (SelectionSystem.Selected == cell) return;

                AnimationSystem.End();
                if (SelectionSystem.Selected == null)
                {
                    SelectionSystem.Select(_grid[cellPosition]);
                }
                else
                {
                    if (SwappingSystem.Swap(_grid, SelectionSystem.Selected.Position, cellPosition))
                    {
                        _swaped = true;
                        SelectionSystem.Deselect();
                        AnimationSystem.Start();
                    }
                    else
                    {
                        SelectionSystem.Deselect();
                    }
                }
            }
            else
            {
                AnimationSystem.End();
                SelectionSystem.Deselect();
            }
        }

        private void OnAnimationStarted()
        {
            _animationEnded = false;
        }
        private void OnAnimationEnded()
        {
            _animationEnded = true;
        }
    }
}