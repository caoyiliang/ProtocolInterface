using Communication;

namespace ProtocolInterface;

public interface IDeviceProtocol : IProtocol
{
    string Name { get; }

    event ActivelyPushDataEventHandler<(DateTime time, Dictionary<string, (decimal value, string state)> dic)>? OnDevicePushData;

    /// <summary>
    /// 对外输出通道
    /// </summary>
    /// <param name="channelsDatas">(时间,<通道名,(值,状态)>)</param>
    /// <returns></returns>
    Task SendData((DateTime time, Dictionary<string, (decimal value, string state)> dic) channelsDatas);
}
