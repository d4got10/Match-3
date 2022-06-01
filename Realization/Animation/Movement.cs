using Match_3.Core.Utils;
using System;

namespace Match_3.Realization.Animation
{
    public class Movement : AnimationBehaviour
    {
        public Movement(GemView view, Vector2 from, Vector2 to, float duration, Action<AnimationBehaviour> onEnd) : base(duration, onEnd)
        {
            View = view;
            From = from;
            To = to;
        }


        protected GemView View { get; }
        protected Vector2 From { get; }
        protected Vector2 To { get; }


        protected override void OnTick()
        {
            base.OnTick();
            var position = Vector2.Lerp(From, To, ElapsedTime / Duration);
            View.SetPosition(position);
        }
    }
}
