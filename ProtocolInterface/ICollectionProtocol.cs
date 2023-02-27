using Communication;

namespace ProtocolInterface;

public interface ICollectionProtocol : IProtocol
{
    string Name { get; }

    event ActivelyPushDataEventHandler<Dictionary<string, decimal>>? OnDevicePushData;
}
