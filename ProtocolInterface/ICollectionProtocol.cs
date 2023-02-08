namespace ProtocolInterface
{
    public interface ICollectionProtocol
    {
        string Name { get; }
        Task<List<decimal>?> ReadValueAsync(CancellationTokenSource? cancelToken = null);
    }
}
