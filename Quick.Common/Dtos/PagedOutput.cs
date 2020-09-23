using Quick.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Dtos
{
    public class PagedOutput<T> : IPagedOutput<T>
    {

        public PagedOutput()
        {
            Items = new List<T>();
        }

        public int TotalCount { get; set; }

        public IList<T> Items { get; set; }
    }
}
