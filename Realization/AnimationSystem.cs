using Match_3.Core;
using Match_3.Core.Gems;
using Match_3.Core.Utils;

namespace Match_3.Realization
{
    public struct Settings
    {
        public int CellSize { get; set; }
    }

    public class AnimationSystem : IAnimationSystem
    {
        public AnimationSystem(IGemViewsContainer viewsContainer, Settings settings)
        {
            ViewsContainer = viewsContainer;
            Settings = settings;
        }


        private IGemViewsContainer ViewsContainer { get; }
        private Settings Settings { get; }


        public void AnimateMovement(Gem target, Vector2Int from, Vector2Int to)
        {
            var view = ViewsContainer.GetView(target);
            view.SetPosition(to * Settings.CellSize * 1.5f);
        }

        public void AnimateSelection(Gem target)
        {
        }

        public void AnimateSwap(Gem first, Gem second)
        {
        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Start()
        {
        }

        public void Tick(ITimeProvider timeProvider)
        {
        }

        public void Unload()
        {
        }
    }
}
