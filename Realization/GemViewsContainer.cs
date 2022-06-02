using Match_3.Core.Gems;
using System.Collections.Generic;

namespace Match_3.Realization
{
    public class GemViewsContainer : IGemViewsContainer
    {
        private Dictionary<Gem, GemView> _viewsMap = new Dictionary<Gem, GemView>();

        public void Add(GemView view)
        {
            _viewsMap.Add(view.Gem, view);
        }

        public GemView GetView(Gem gem)
        {
            return _viewsMap[gem];
        }

        public void Remove(GemView view)
        {
            _viewsMap.Remove(view.Gem);
        }

        public void Unload()
        {
            foreach(var view in _viewsMap.Values)
            {
                view.Unload();
            }
            _viewsMap.Clear();
        }
    }
}
