using Match_3.Core.Gems;

namespace Match_3.Realization
{
    public interface IGemViewsContainer
    {
        void Add(GemView view);
        void Remove(GemView view);
        GemView GetView(Gem gem);
    }
}
