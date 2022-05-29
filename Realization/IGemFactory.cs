using Match_3.Core.Gems;

namespace Match_3.Realization
{
    public interface IGemFactory
    {
        Gem Create(GemType type);
    }
}
