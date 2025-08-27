using Communication;
using Parser.Interfaces;
using ProtocolInterface.Model;

namespace ProtocolInterface;

public interface IDeviceProtocol : IProtocol
{
    string Name { get; }

    IParser Parser { get; }

    event ActivelyPushDataEventHandler<Data> OnDevicePushData;

    /// <summary>
    /// 对外输出通道
    /// </summary>
    Task SendData(Data channelsDatas);
}
