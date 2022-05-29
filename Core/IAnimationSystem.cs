using Match_3.Core.Gems;
using Match_3.Core.Utils;

namespace Match_3.Core
{
    public interface IAnimationSystem
    {
        void Start();
        void Pause();
        void Tick(ITimeProvider timeProvider);
        void Resume();
        void Unload();
        void AnimateSelection(Gem target);
        void AnimateSwap(Gem first, Gem second);
        void AnimateMovement(Gem target, Vector2Int from, Vector2Int to);
    }
}