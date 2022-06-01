using Match_3.Core.Gems;
using Match_3.Core.Utils;

namespace Match_3.Core
{
    public interface IAnimationSystem
    {
        event System.Action AnimationStarted;
        event System.Action AnimationEnded;
        void Start();
        void End();
        void Tick(ITimeProvider timeProvider);
        void Pause();
        void Resume();
        void AnimateSelection(Gem target);
        void AnimateSuccessfulSwap(Gem first, Gem second);
        void AnimateFailedSwap(Gem first, Gem second);
        void AnimateMovement(Gem target, Vector2Int from, Vector2Int to);
        void AnimateAppearence(Gem target, Vector2Int position);
        void AnimateDestruction(Gem target);
    }
}