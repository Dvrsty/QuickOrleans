using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Interface
{
    public interface IPagedInput
    {
        /// <summary>
        /// 返回行数
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 页码,页码从第一页开始
        /// </summary>
        int PageIndex { get; set; }
    }
}
