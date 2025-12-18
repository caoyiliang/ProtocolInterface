namespace ProtocolInterface;

/// <summary>
/// 协议配置接口
/// </summary>
public interface IProtocolConfig
{
    /// <summary>
    /// 协议
    /// </summary>
    string Protocol { get; }

    /// <summary>
    /// 协议名称
    /// </summary>
    string ProtocolName { get; }

    /// <summary>
    /// 输入输出类型
    /// </summary>
    IOType IOType { get; }

    /// <summary>
    /// 界面类型
    /// </summary>
    UIType UIType { get; }
}
