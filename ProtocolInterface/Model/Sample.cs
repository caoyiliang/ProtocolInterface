namespace ProtocolInterface.Model
{
    /// <summary>
    /// 数据样品
    /// </summary>
    /// <param name="sampleTime">样品时间</param>
    /// <param name="sampleName">样品名称 或 标识</param>
    /// <param name="value">样品值</param>
    /// <param name="state">样品状态</param>
    /// <param name="persistenceStrategy">持久化策略</param>
    public class Sample(DateTime sampleTime, string sampleName, decimal value, string state, PersistenceStrategy persistenceStrategy = PersistenceStrategy.BATCH)
    {
        /// <summary>
        /// 样品时间
        /// </summary>
        public DateTime SampleTime { get; set; } = sampleTime;

        /// <summary>
        /// 持久化策略
        /// </summary>
        public PersistenceStrategy PersistenceStrategy { get; set; } = persistenceStrategy;

        /// <summary>
        /// 样品名称 或 标识
        /// </summary>
        public string SampleName { get; set; } = sampleName;

        /// <summary>
        /// 样品值
        /// </summary>
        public decimal Value { get; set; } = value;

        /// <summary>
        /// 样品状态
        /// </summary>
        public string State { get; set; } = state;
    }
}
