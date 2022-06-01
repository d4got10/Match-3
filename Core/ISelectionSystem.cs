namespace Match_3.Core
{
    public interface ISelectionSystem
    {
        Cell Selected { get; }
        void Select(Cell cell);
        void Deselect();
    }
}