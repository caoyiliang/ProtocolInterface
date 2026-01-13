using Communication;

namespace ProtocolInterface;

/// <summary>
/// 协议接口
/// </summary>
public interface IProtocol
{
    /// <summary>
    /// 设备是否连接
    /// </summary>
    public bool IsConnect { get; }

    /// <summary>
    /// 打开串口
    /// </summary>
    Task OpenAsync();

    /// <summary>
    /// 关闭串口
    /// </summary>
    Task CloseAsync(bool closePhysicalPort = true);

    /// <summary>
    /// 对端掉线
    /// </summary>
    event DisconnectEventHandler OnDisconnect;

    /// <summary>
    /// 对端连接成功
    /// </summary>
    event ConnectEventHandler OnConnect;
}
