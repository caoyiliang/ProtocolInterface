using Communication;

namespace ProtocolInterface.Device;

/// <summary>
/// 通用协议设备入口
/// </summary>
public static class ProtocolDevice
{
    /// <summary>
    /// 使用串口
    /// </summary>
    /// <typeparam name="TDevice">设备类型</typeparam>
    /// <param name="portName">串口名称</param>
    /// <param name="baudRate">波特率</param>
    /// <param name="parser">帧解析器</param>
    /// <param name="deviceFactory">设备工厂函数（接收超时时间）</param>
    public static IClientConfigStep<TDevice> UseSerial<TDevice>(Func<int, TDevice> deviceFactory)
        where TDevice : ProtocolDeviceBase
        => new ClientBuilder<TDevice>(deviceFactory);

    /// <summary>
    /// 使用 TCP 客户端
    /// </summary>
    /// <typeparam name="TDevice">设备类型</typeparam>
    /// <param name="host">服务器地址</param>
    /// <param name="port">服务器端口</param>
    /// <param name="parser">帧解析器</param>
    public static IClientConfigStep<TDevice> UseTcpClient<TDevice>(Func<int, TDevice> deviceFactory)
        where TDevice : ProtocolDeviceBase
        => new ClientBuilder<TDevice>(deviceFactory);

    /// <summary>
    /// 使用 TCP 服务端
    /// </summary>
    /// <typeparam name="TDevice">设备类型</typeparam>
    /// <param name="host">绑定地址</param>
    /// <param name="port">监听端口</param>
    /// <param name="foot">帧尾字节</param>
    /// <param name="deviceFactory">设备工厂函数（接收 host, port, 超时时间）</param>
    /// <param name="instance">用于获取正确程序集的实例（通常是设备类型所在程序集中的某个类的实例）</param>
    public static IServerConfigStep<TDevice> UseTcpServer<TDevice>(string host, int port, Func<string, int, int, TDevice> deviceFactory)
        where TDevice : ProtocolServerDeviceBase
        => new ServerBuilder<TDevice>(host, port, deviceFactory);

    private enum PortType { None, Serial, TcpClient }

    /// <summary>
    /// 通用客户端 Builder
    /// </summary>
    private class ClientBuilder<TDevice> : IClientConfigStep<TDevice> where TDevice : ProtocolDeviceBase
    {
        private readonly Func<int, TDevice> _deviceFactory;
        private int _timeoutMs = 5000;
        private ConnectEventHandler? _onConnect;
        private DisconnectEventHandler? _onDisconnect;

        public ClientBuilder(Func<int, TDevice> deviceFactory)
        {
            _deviceFactory = deviceFactory;
        }

        public IClientConfigStep<TDevice> Timeout(int timeoutMs)
        {
            _timeoutMs = timeoutMs;
            return this;
        }

        public IClientConfigStep<TDevice> OnConnect(ConnectEventHandler handler)
        {
            _onConnect = handler;
            return this;
        }

        public IClientConfigStep<TDevice> OnDisconnect(DisconnectEventHandler handler)
        {
            _onDisconnect = handler;
            return this;
        }

        public TDevice Build()
        {
            var device = _deviceFactory(_timeoutMs);

            if (_onConnect != null)
                device.OnConnect += _onConnect;
            if (_onDisconnect != null)
                device.OnDisconnect += _onDisconnect;

            return device;
        }
    }

    /// <summary>
    /// 通用服务端 Builder
    /// </summary>
    internal class ServerBuilder<TDevice> : IServerConfigStep<TDevice> where TDevice : ProtocolServerDeviceBase
    {
        private readonly string _host;
        private readonly int _port;
        private readonly Func<string, int, int, TDevice> _deviceFactory;
        private int _timeoutMs = 5000;
        private Func<string, Task>? _onClientConnected;
        private Func<string, Task>? _onClientDisconnected;

        public ServerBuilder(string host, int port, Func<string, int, int, TDevice> deviceFactory)
        {
            _host = host;
            _port = port;
            _deviceFactory = deviceFactory;
        }

        public IServerConfigStep<TDevice> Timeout(int timeoutMs)
        {
            _timeoutMs = timeoutMs;
            return this;
        }

        public IServerConfigStep<TDevice> OnClientConnected(Func<string, Task> handler)
        {
            _onClientConnected = handler;
            return this;
        }

        public IServerConfigStep<TDevice> OnClientDisconnected(Func<string, Task> handler)
        {
            _onClientDisconnected = handler;
            return this;
        }

        public TDevice Build()
        {
            var device = _deviceFactory(_host, _port, _timeoutMs);

            if (_onClientConnected != null)
                device.OnClientConnect += clientId => _onClientConnected(clientId.ToString());
            if (_onClientDisconnected != null)
                device.OnClientDisconnect += clientId => _onClientDisconnected(clientId.ToString());

            return device;
        }
    }
}
