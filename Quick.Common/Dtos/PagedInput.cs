using Quick.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Dtos
{
    public class PagedInput : IPagedInput
    {
        /// <summary>
        /// 获取行数
        /// </summary>
        public int PageSize { get; set; } = 30;
        /// <summary>
        /// 页码,页码从第一页开始
        /// </summary>
        public int PageIndex { get; set; } = 1;
    }
}
