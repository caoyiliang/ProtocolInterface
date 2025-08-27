namespace ProtocolInterface.Model
{
    /// <summary>
    /// 数据实体
    /// </summary>
    /// <param name="dateTime">采集时间</param>
    public class Data(DateTime dateTime)
    {
        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime DateTime { get; set; } = dateTime;

        /// <summary>
        /// 持久化策略
        /// </summary>
        public PersistenceStrategy PersistenceStrategy { get; set; }

        /// <summary>
        /// 样品信息
        /// </summary>
        public List<Sample> Samples { get; set; } = [];
    }
}
