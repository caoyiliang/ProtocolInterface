namespace ProtocolInterface
{
    public interface ICollectionProtocol : IProtocol
    {
        string Name { get; }
        Task<Dictionary<string, decimal>?> ReadValueAsync(CancellationTokenSource? cancelToken = null);
    }
}
