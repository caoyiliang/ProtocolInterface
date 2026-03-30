using Communication;

namespace ProtocolInterface.Device;

/// <summary>
/// 客户端配置步骤接口（泛型）
/// </summary>
/// <typeparam name="TDevice">设备类型</typeparam>
public interface IClientConfigStep<TDevice> where TDevice : ProtocolDeviceBase
{
    /// <summary>
    /// 设置超时时间
    /// </summary>
    IClientConfigStep<TDevice> Timeout(int timeoutMs);

    /// <summary>
    /// 设置连接事件
    /// </summary>
    IClientConfigStep<TDevice> OnConnect(ConnectEventHandler handler);

    /// <summary>
    /// 设置断开连接事件
    /// </summary>
    IClientConfigStep<TDevice> OnDisconnect(DisconnectEventHandler handler);

    /// <summary>
    /// 构建设备实例
    /// </summary>
    TDevice Build();
}

/// <summary>
/// 服务端配置步骤接口（泛型）
/// </summary>
/// <typeparam name="TDevice">设备类型</typeparam>
public interface IServerConfigStep<TDevice> where TDevice : ProtocolServerDeviceBase
{
    /// <summary>
    /// 设置超时时间
    /// </summary>
    IServerConfigStep<TDevice> Timeout(int timeoutMs);

    /// <summary>
    /// 设置客户端连接事件
    /// </summary>
    IServerConfigStep<TDevice> OnClientConnected(Func<string, Task> handler);

    /// <summary>
    /// 设置客户端断开事件
    /// </summary>
    IServerConfigStep<TDevice> OnClientDisconnected(Func<string, Task> handler);

    /// <summary>
    /// 构建设备实例
    /// </summary>
    TDevice Build();
}
