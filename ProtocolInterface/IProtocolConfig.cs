namespace ProtocolInterface;

public interface IProtocolConfig
{
    string ProtocolName { get; }
    CollectionType CollectionType { get; set; }
}
