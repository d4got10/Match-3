using Match_3.Core.Utils;
using System;

namespace Match_3.Realization.Animation
{
    public class Destruction : AnimationBehaviour
    {
        public Destruction(GemView view, float duration, Action<AnimationBehaviour> onEnd) : base(duration, onEnd)
        {
            View = view;
            NormalScale = view.Scale;
            CenterPoint = view.Position + 0.5f * view.Scale;
            CornerPoint = view.Position;
        }

        public GemView View { get; }
        private Vector2 NormalScale { get; }
        private Vector2 CenterPoint { get; }
        private Vector2 CornerPoint { get; }


        protected override void OnTick()
        {
            base.OnTick();
            float t = ElapsedTime / Duration;
            var scale = Vector2.Lerp(NormalScale, Vector2.Zero, t);
            var position = Vector2.Lerp(CornerPoint, CenterPoint, t);
            View.SetScale(scale);
            View.SetPosition(position);
        }

        protected override void OnBeforeEnd()
        {
            base.OnBeforeEnd();
            View.Unload();
        }
    }
}
