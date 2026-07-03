using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Dtos
{
    public class MetaDataDto
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }

        public static MetaDataDto GetMeta(PagingDto paging, int count)
        {
            return new MetaDataDto
            {
                TotalCount = count,
                PageSize = paging.PageSize,
                Page = paging.Page
            };

        }

    }
}
