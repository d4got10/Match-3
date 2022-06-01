using Match_3.Core.Utils;
using System;

namespace Match_3.Realization.Animation
{
    public class SuccessfulSwap : AnimationBehaviour
    {
        public SuccessfulSwap(GemView first, GemView second, float duration, Action<AnimationBehaviour> onEnd) : base(duration, onEnd)
        {
            First = first;
            Second = second;
            FirstPosition = first.Position;
            SecondPosition = second.Position;
        }

        protected GemView First { get; }
        protected GemView Second { get; }
        protected Vector2 FirstPosition { get; }
        protected Vector2 SecondPosition { get; }


        protected override void OnTick()
        {
            base.OnTick();

            float t = ElapsedTime / Duration;
            var firstPosition = Vector2.Lerp(FirstPosition, SecondPosition, t);
            var secondPosition = Vector2.Lerp(SecondPosition, FirstPosition, t);

            First.SetPosition(firstPosition);
            Second.SetPosition(secondPosition);
        }

        protected override void OnBeforeEnd()
        {
            base.OnBeforeEnd();
            First.SetPosition(SecondPosition);
            Second.SetPosition(FirstPosition);
        }
    }
}
