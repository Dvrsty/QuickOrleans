using System;

namespace Quick.Common.Dtos
{
    /// <summary>
    ///  api返回结构DTO
    /// </summary>
    public class ApiOutput
    {
        /// <summary>
        ///  成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///  状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        ///  错误消息
        /// </summary>
        public string Message { get; set; }
    }
}
