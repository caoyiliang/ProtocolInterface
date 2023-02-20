namespace ProtocolInterface
{
    public interface ICollectionProtocol
    {
        string Name { get; }
        Task<Dictionary<string, decimal>?> ReadValueAsync(CancellationTokenSource? cancelToken = null);
    }
}
