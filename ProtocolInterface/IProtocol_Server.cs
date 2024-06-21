using Communication;
using TopPortLib;

namespace ProtocolInterface;

public interface IProtocol_Server
{
    /// <summary>
    /// 设备是否监听
    /// </summary>
    public bool IsListened { get; }

    /// <summary>
    /// 打开监听
    /// </summary>
    Task StartAsync();

    /// <summary>
    /// 关闭监听
    /// </summary>
    Task StopAsync();

    /// <summary>
    /// 客户端断连事件
    /// </summary>
    event ClientDisconnectEventHandler OnClientDisconnect;

    /// <summary>
    /// 客户端连接事件
    /// </summary>
    event ClientConnectEventHandler OnClientConnect;

    /// <summary>
    /// 发送数据事件
    /// </summary>
    event RequestedLogServerEventHandler OnSentData;

    /// <summary>
    /// 收到数据事件
    /// </summary>
    event RespondedLogServerEventHandler OnReceivedData;
}
