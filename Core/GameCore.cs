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


        public void Tick(float time)
        {
            if (IsRunning == false) return;

            DeltaTime = time - Time;
            Time = time;

            if (_animationEnded)
            {
                GenerationSystem.Generate(_grid);
                if(SheddingSystem.Shed(_grid) == false)
                    MatchSystem.DestroyMatches(_grid);

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

            //for (int y = 0; y < GridSize.Y; y++)
            //{
            //    GenerationSystem.Generate(_grid);
            //    SheddingSystem.Move(_grid);
            //}

            //AnimationSystem.Start();
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
            if (_grid.PositionIsInsidePlayableGrid(cellPosition))
            {
                var cell = _grid[cellPosition];
                if (SelectionSystem.Selected == cell) return;

                if (SelectionSystem.Selected == null)
                {
                    SelectionSystem.Select(_grid[cellPosition]);
                }
                else
                {
                    SwappingSystem.Swap(_grid, SelectionSystem.Selected.Position, cellPosition);
                    AnimationSystem.End();
                    SelectionSystem.Deselect();
                    AnimationSystem.Start();
                }
            }
            else
            {
                SelectionSystem.Deselect();
                AnimationSystem.End();
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