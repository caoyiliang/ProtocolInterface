using Communication;
using Parser.Interfaces;
using ProtocolInterface.Model;

namespace ProtocolInterface;

/// <summary>
/// 设备协议接口
/// </summary>
public interface IDeviceProtocol : IProtocol
{
    /// <summary>
    /// 设备名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 解析器
    /// </summary>
    IParser Parser { get; }

    /// <summary>
    /// 设备数据推送事件
    /// </summary>
    event ActivelyPushDataEventHandler<Data> OnDevicePushData;

    /// <summary>
    /// 对外输出通道
    /// </summary>
    Task SendData(Data channelsDatas);
}
