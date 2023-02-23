using Communication;

namespace ProtocolInterface;

public interface ICollectionProtocol : IProtocol
{
    string Name { get; }
    bool Active { get; }

    event ActivelyPushDataEventHandler<Dictionary<string, decimal>>? OnDevicePushData;
}
