namespace ProtocolInterface
{
    public interface IActiveCollectionProtocol : IProtocol
    {
        string Name { get; }
        Task<Dictionary<string, decimal>?> ReadValueAsync(CancellationTokenSource? cancelToken = null);
    }
}
