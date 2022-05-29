namespace Match_3.Core
{
    public interface ITimeProvider
    {
        float Time { get; }
        float DeltaTime { get; }
    }
}