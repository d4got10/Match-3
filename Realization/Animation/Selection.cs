using Match_3.Core.Utils;
using System;

namespace Match_3.Realization.Animation
{
    public class Selection : AnimationBehaviour
    {
        public Selection(GemView view, float duration, Action<AnimationBehaviour> onEnd) : base(duration, onEnd)
        {
            View = view;
            NormalScale = View.Scale;
            CenterPoint = view.Position + 0.5f * view.Scale;
            CornerPoint = view.Position;
        }

        private Vector2 CenterPoint { get; }
        private Vector2 CornerPoint { get; }
        protected Vector2 NormalScale { get; }
        protected GemView View { get; }


        protected override void OnTick()
        {
            base.OnTick();
            float t = 1f + (float)Math.Sin(ElapsedTime * 5) * 0.25f;

            var scale = Vector2.Lerp(Vector2.Zero, NormalScale, t);
            var position = Vector2.Lerp(CenterPoint, CornerPoint, t);
            View.SetScale(scale);
            View.SetPosition(position);
            View.Focus();
        }

        protected override void OnBeforeEnd()
        {
            base.OnBeforeEnd();
            View.SetScale(NormalScale);
            View.SetPosition(CornerPoint);
            View.Unfocus();
        }
    }
}
