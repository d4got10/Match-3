using Match_3.Core.Utils;

namespace Match_3.Core
{
    public class GameCore : ITimeProvider
    {
        public GameCore(Vector2Int gridSize, ISheddingSystem sheddingSystem, IMatchSystem matchSystem, IGenerationSystem generationSystem, IAnimationSystem animationSystem)
        {
            GridSize = gridSize;
            SheddingSystem = sheddingSystem;
            MatchSystem = matchSystem;
            GenerationSystem = generationSystem;
            AnimationSystem = animationSystem;
            IsRunning = false;
        }

        public ISheddingSystem SheddingSystem { get; }
        public IMatchSystem MatchSystem { get; }
        public IGenerationSystem GenerationSystem { get; }
        public IAnimationSystem AnimationSystem { get; }
        public float Time { get; private set; }
        public float DeltaTime { get; private set; }
        public Vector2Int GridSize { get; }
        public bool IsRunning { get; private set; }


        private GameGrid _grid;


        public void Tick(float time)
        {
            if (IsRunning) return;

            DeltaTime = time - Time;
            Time = time;

            while (MatchSystem.DestroyMatches(_grid))
            {
                GenerationSystem.Generate(_grid);
                SheddingSystem.Move(_grid);
                AnimationSystem.Tick(this);
            }
        }

        public void Start()
        {
            _grid = new GameGrid(GridSize);

            IsRunning = true;

            for (int y = 0; y < GridSize.Y; y++)
            {
                AnimationSystem.Start();
                GenerationSystem.Generate(_grid);
                SheddingSystem.Move(_grid);
            }
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
            AnimationSystem.Unload();
        }
    }
}