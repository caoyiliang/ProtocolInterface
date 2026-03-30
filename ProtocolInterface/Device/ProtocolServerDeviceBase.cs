using Communication;
using LogInterface;
using TopPortLib;
using TopPortLib.Interfaces;
using Utils;

namespace ProtocolInterface.Device;

/// <summary>
/// 通用协议设备服务端基类
/// 用于服务端模式（TCP服务端等）
/// </summary>
public abstract class ProtocolServerDeviceBase : IProtocol_Server
{
    protected static readonly ILogger Logger = Logs.LogFactory.GetLogger<ProtocolServerDeviceBase>();
    protected readonly ICondorPort CondorPort;
    protected bool IsRunning = false;
    protected readonly SynchronizedCollection<Guid> ConnectedClients = new();

    public bool IsListened => IsRunning;

    public event ClientDisconnectEventHandler? OnClientDisconnect;
    public event ClientConnectEventHandler? OnClientConnect;
    public event RequestedLogServerEventHandler? OnSentData;
    public event RespondedLogServerEventHandler? OnReceivedData;

    /// <summary>
    /// 创建 CondorPort 并初始化
    /// </summary>
    /// <param name="topPort_Server">顶层服务</param>
    /// <param name="instance">协议实例，用于获取正确的程序集</param>
    /// <param name="defaultTimeout">默认超时</param>
    protected ProtocolServerDeviceBase(object instance, ITopPort_Server topPort_Server, int defaultTimeout = 5000)
    {
        CondorPort = new CondorPort(instance, topPort_Server, defaultTimeout);
        SubscribeEvents();
    }

    /// <summary>
    /// 订阅 CondorPort 事件
    /// </summary>
    private void SubscribeEvents()
    {
        CondorPort.OnClientConnect += CondorPort_OnClientConnect;
        CondorPort.OnClientDisconnect += CondorPort_OnClientDisconnect;
        CondorPort.OnSentData += CondorPort_OnSentData;
        CondorPort.OnReceivedData += CondorPort_OnReceivedData;
    }

    private async Task CondorPort_OnClientConnect(Guid clientId)
    {
        if (!ConnectedClients.Contains(clientId))
            ConnectedClients.Add(clientId);
        Logger.Trace($"Client connected: {clientId}, total: {ConnectedClients.Count}");

        if (OnClientConnect != null)
            await OnClientConnect(clientId);
    }

    private async Task CondorPort_OnClientDisconnect(Guid clientId)
    {
        ConnectedClients.Remove(clientId);
        Logger.Trace($"Client disconnected: {clientId}, total: {ConnectedClients.Count}");

        if (OnClientDisconnect != null)
            await OnClientDisconnect(clientId);
    }

    private async Task CondorPort_OnSentData(Guid clientId, byte[] data)
    {
        Logger.Trace($"Send:--> {StringByteUtils.BytesToString(data)}");
        if (OnSentData != null)
            await OnSentData(clientId, data);
    }

    private async Task CondorPort_OnReceivedData(Guid clientId, byte[] data)
    {
        Logger.Trace($"Rec:<-- {StringByteUtils.BytesToString(data)}");
        if (OnReceivedData != null)
            await OnReceivedData(clientId, data);
    }

    public virtual async Task StartAsync()
    {
        IsRunning = true;
        await CondorPort.StartAsync();
        Logger.Trace("Server started");
    }

    public virtual async Task StopAsync()
    {
        IsRunning = false;
        ConnectedClients.Clear();
        await CondorPort.StopAsync();
        Logger.Trace("Server stopped");
    }

    public virtual async Task<string?> GetClientInfos(Guid clientId)
    {
        return await CondorPort.GetClientInfos(clientId);
    }

    /// <summary>
    /// 获取所有连接的客户端 ID
    /// </summary>
    public IEnumerable<string> GetConnectedClientIds()
    {
        return ConnectedClients.Select(id => id.ToString());
    }

    /// <summary>
    /// 检查客户端是否已连接
    /// </summary>
    public bool IsClientConnected(string clientId)
    {
        if (!Guid.TryParse(clientId, out var clientGuid))
            return false;
        return ConnectedClients.Contains(clientGuid);
    }

    /// <summary>
    /// 检查客户端是否已连接
    /// </summary>
    public bool IsClientConnected(Guid clientId)
    {
        return ConnectedClients.Contains(clientId);
    }

    /// <summary>
    /// 获取当前连接数
    /// </summary>
    public int ConnectedClientCount => ConnectedClients.Count;
}
