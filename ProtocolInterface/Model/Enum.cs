namespace ProtocolInterface.Model
{
    /// <summary>
    /// 持久化策略
    /// </summary>
    public enum PersistenceStrategy
    {
        /// <summary>直接入数据库</summary>
        IMMEDIATE,
        /// <summary>先缓存处理后入库</summary>
        BATCH
    }
}
