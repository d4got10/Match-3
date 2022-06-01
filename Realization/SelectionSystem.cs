using Match_3.Core;

namespace Match_3.Realization
{
    public class SelectionSystem : ISelectionSystem
    {
        public SelectionSystem(IAnimationSystem animationSystem)
        {
            AnimationSystem = animationSystem;
        }


        public Cell Selected { get; private set; }

        protected IAnimationSystem AnimationSystem { get; }


        public void Select(Cell cell)
        {
            Selected = cell;
            AnimationSystem.AnimateSelection(cell.ContainedGem);
        }

        public void Deselect()
        {
            Selected = null;
        }
    }
}
