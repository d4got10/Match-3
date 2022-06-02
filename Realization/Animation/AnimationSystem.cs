using Match_3.Core;
using Match_3.Core.Gems;
using Match_3.Core.Utils;
using Match_3.Utils;
using System;
using System.Collections.Generic;

namespace Match_3.Realization.Animation
{
    public class AnimationSystem : IAnimationSystem
    {
        public AnimationSystem(IGemViewsContainer viewsContainer, Settings settings, DurationSettings durationSettings)
        {
            ViewsContainer = viewsContainer;
            Settings = settings;
            DurationSettings = durationSettings;
        }


        public event Action AnimationStarted;
        public event Action AnimationEnded;


        public DurationSettings DurationSettings { get; }
        public float RemainingAnimationsDuration { get; private set; }


        private IGemViewsContainer ViewsContainer { get; }
        private Settings Settings { get; }


        private LinkedList<AnimationBehaviour> _animations = new LinkedList<AnimationBehaviour>();
        private LinkedList<AnimationBehaviour> _endedAnimations = new LinkedList<AnimationBehaviour>();


        public void AnimateAppearence(Gem target, Vector2Int position)
        {
            AddAnimationDuration(DurationSettings.Appearence);

            var view = ViewsContainer.GetView(target);
            view.SetPosition(PositionUtils.ConvertGridToWorldPosition(position, Settings));
        }

        public void AnimateMovement(Gem target, Vector2Int from, Vector2Int to)
        {
            var duration = DurationSettings.Movement;
            AddAnimationDuration(duration);

            var fromWorld = PositionUtils.ConvertGridToWorldPosition(from, Settings);
            var toWorld = PositionUtils.ConvertGridToWorldPosition(to, Settings);

            var view = ViewsContainer.GetView(target);
            var animation = new Movement(view, fromWorld, toWorld, duration, OnAnimationEnded);
            AddAnimation(animation);
        }

        public void AnimateSelection(Gem target)
        {
            AddAnimationDuration(float.MaxValue);
            var view = ViewsContainer.GetView(target);
            var animation = new Selection(view, float.MaxValue, OnAnimationEnded);
            AddAnimation(animation);
        }

        public void AnimateSuccessfulSwap(Gem first, Gem second)
        {
            var duration = DurationSettings.Swap;
            AddAnimationDuration(duration);

            var firstView = ViewsContainer.GetView(first);
            var secondView = ViewsContainer.GetView(second);

            var animation = new SuccessfulSwap(firstView, secondView, duration, OnAnimationEnded);
            AddAnimation(animation);
        }

        public void AnimateFailedSwap(Gem first, Gem second)
        {
            var duration = DurationSettings.Swap;
            AddAnimationDuration(duration);

            var firstView = ViewsContainer.GetView(first);
            var secondView = ViewsContainer.GetView(second);

            var animation = new FailedSwap(firstView, secondView, duration, OnAnimationEnded);
            AddAnimation(animation);
        }

        public void AnimateDestruction(Gem target)
        {
            var duration = DurationSettings.Destruction;
            AddAnimationDuration(duration);

            var view = ViewsContainer.GetView(target);
            var animation = new Destruction(view, duration, OnAnimationEnded);
            AddAnimation(animation);
        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Start()
        {
            if (_animations.Count == 0) return;

            foreach (var animation in _animations)
                animation.Start();

            AnimationStarted?.Invoke();
        }

        public void End()
        {
            foreach (var animation in _animations)
                animation.End();

            RemoveEndedAnimations();

            RemainingAnimationsDuration = 0;

            AnimationEnded?.Invoke();
        }

       

        public void Tick(ITimeProvider timeProvider)
        {
            float deltaTime = timeProvider.DeltaTime;
            RemainingAnimationsDuration -= deltaTime;

            foreach(var animation in _animations)
                animation.Tick(deltaTime);

            RemoveEndedAnimations();

            if (RemainingAnimationsDuration < 0)
            {
                RemainingAnimationsDuration = 0;
                End();
            }
        }


        private void AddAnimation(AnimationBehaviour animation)
        {
            _animations.AddLast(animation);
        }

        private void AddAnimationDuration(float duration)
        {
            RemainingAnimationsDuration = Math.Max(RemainingAnimationsDuration, duration);
        }

        private void OnAnimationEnded(AnimationBehaviour behaviour)
        {
            _endedAnimations.AddLast(behaviour);
        }

        private void RemoveEndedAnimations()
        {
            foreach (var endedAnimation in _endedAnimations)
                _animations.Remove(endedAnimation);
            _endedAnimations.Clear();
        }
    }
}
