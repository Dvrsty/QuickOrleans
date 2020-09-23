using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Interface
{
    public interface IPagedOutput<T>
    {
        int TotalCount { get; set; }

        IList<T> Items { get; set; }
    }
}
