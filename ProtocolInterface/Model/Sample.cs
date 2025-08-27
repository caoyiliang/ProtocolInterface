namespace ProtocolInterface.Model
{
    /// <summary>
    /// 数据样品
    /// </summary>
    /// <param name="sampleName">样品名称 或 标识</param>
    /// <param name="value">样品值</param>
    /// <param name="state">样品状态</param>
    public class Sample(string sampleName, decimal value, string state)
    {
        /// <summary>
        /// 样品时间
        /// </summary>
        public DateTime? SampleTime { get; set; }

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
