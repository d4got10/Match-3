namespace Match_3.Core
{
    public class GameCore
    {
        public float Time { get; private set; }
        public float DeltaTime { get; private set; }


        public void Tick(float time)
        {
            DeltaTime = time - Time;
            Time = time;


        }

        public void Start()
        {
        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }
    }
}