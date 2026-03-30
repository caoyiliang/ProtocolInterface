using Communication;
using LogInterface;
using TopPortLib.Interfaces;
using Utils;

namespace ProtocolInterface.Device;

/// <summary>
/// 通用协议设备基类
/// 用于客户端模式（串口、TCP客户端等）
/// </summary>
public abstract class ProtocolDeviceBase : IProtocol
{
    protected static readonly ILogger Logger = Logs.LogFactory.GetLogger<ProtocolDeviceBase>();
    protected readonly ICrowPort CrowPort;
    protected bool IsConnected = false;

    public bool IsConnect => IsConnected;

    public event DisconnectEventHandler? OnDisconnect { add => CrowPort.OnDisconnect += value; remove => CrowPort.OnDisconnect -= value; }
    public event ConnectEventHandler? OnConnect { add => CrowPort.OnConnect += value; remove => CrowPort.OnConnect -= value; }

    protected ProtocolDeviceBase(ICrowPort crowPort)
    {
        CrowPort = crowPort;
        CrowPort.OnConnect += CrowPort_OnConnect;
        CrowPort.OnDisconnect += CrowPort_OnDisconnect;
        CrowPort.OnSentData += CrowPort_OnSentData;
        CrowPort.OnReceivedData += CrowPort_OnReceivedData;
    }

    private async Task CrowPort_OnConnect()
    {
        IsConnected = true;
        await Task.CompletedTask;
    }

    private async Task CrowPort_OnDisconnect()
    {
        IsConnected = false;
        await Task.CompletedTask;
    }

    protected virtual async Task CrowPort_OnSentData(byte[] data)
    {
        Logger.Trace($"Send:--> {StringByteUtils.BytesToString(data)}");
        await Task.CompletedTask;
    }

    protected virtual async Task CrowPort_OnReceivedData(byte[] data)
    {
        Logger.Trace($"Rec:<-- {StringByteUtils.BytesToString(data)}");
        await Task.CompletedTask;
    }

    public virtual Task OpenAsync()
    {
        IsConnected = CrowPort.PhysicalPort.IsOpen;
        return CrowPort.OpenAsync();
    }

    public virtual async Task CloseAsync(bool closePhysicalPort = true)
    {
        if (closePhysicalPort) await CrowPort.CloseAsync();
    }
}
