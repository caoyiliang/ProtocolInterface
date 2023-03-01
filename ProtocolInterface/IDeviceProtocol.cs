using Communication;

namespace ProtocolInterface;

public interface IDeviceProtocol : IProtocol
{
    string Name { get; }

    event ActivelyPushDataEventHandler<(DateTime, Dictionary<string, decimal>)>? OnDevicePushData;

    Task SendData((DateTime, Dictionary<string, decimal> data) channelsDatas);
}
