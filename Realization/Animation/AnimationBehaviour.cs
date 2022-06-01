using System;


namespace Match_3.Realization.Animation
{
    public abstract class AnimationBehaviour
    {
        public AnimationBehaviour(float duration, Action<AnimationBehaviour> onEnd)
        {
            Duration = duration;
            OnEnd = onEnd;
        }


        protected float Duration { get; }
        protected float ElapsedTime { get; private set; }


        private Action<AnimationBehaviour> OnEnd { get; }


        public void Start()
        {
            ElapsedTime = 0;
        }
        public void Tick(float deltaTime)
        {
            ElapsedTime += deltaTime;
            if (ElapsedTime >= Duration)
            {
                ElapsedTime = Duration;
                OnTick();
                End();
            }
            else
            {
                OnTick();
            }
        }
        public void End()
        {
            OnBeforeEnd();
            OnEnd?.Invoke(this);
        }


        protected virtual void OnTick()
        {
        }
        protected virtual void OnBeforeEnd()
        {
        }
    }
}
