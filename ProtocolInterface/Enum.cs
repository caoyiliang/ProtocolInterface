namespace ProtocolInterface;

/// <summary>
/// 寄存器值类型
/// </summary>
public enum RegisterValueType
{
    Float,
    UInt16,
    UInt32,
    Int16,
    Int32,
    sbyteA,
    sbyteB,
    String
}

/// <summary>
/// 输入输出类型
/// </summary>
public enum IOType
{
    Input,
    Output,
    All
}

/// <summary>
/// 界面类型
/// </summary>
public enum UIType
{
    信号量,
    开关量,
    Modbus,
    Gb
}