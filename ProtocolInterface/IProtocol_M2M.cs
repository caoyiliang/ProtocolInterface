using Communication;
using TopPortLib;

namespace ProtocolInterface;

/// <summary>
/// 多对多协议接口
/// </summary>
public interface IProtocol_M2M
{
    /// <summary>
    /// 设备是否监听
    /// </summary>
    public bool IsListened { get; }

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

    /// <summary>
    /// 打开监听
    /// </summary>
    Task StartAsync();

    /// <summary>
    /// 关闭监听
    /// </summary>
    Task StopAsync();

    /// <summary>
    /// 获取客户端信息
    /// </summary>
    /// <param name="clientId">客户端ID</param>
    /// <returns>客户端信息</returns>
    Task<string?> GetClientInfos(Guid clientId);

    /// <summary>
    /// 移除客户端
    /// </summary>
    /// <param name="clientId">客户端ID</param>
    Task RemoveClientAsync(Guid clientId);

    /// <summary>
    /// 添加客户端
    /// </summary>
    /// <param name="hostName">目标地址</param>
    /// <param name="port">目标端口</param>
    /// <returns>客户端ID</returns>
    Task<Guid> AddClientAsync(string hostName, int port);
}
