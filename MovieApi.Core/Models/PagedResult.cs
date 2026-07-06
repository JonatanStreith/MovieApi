using MovieApi.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Models
{
    public record class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }

        public MetaDataDto Meta { get; set; }
    }

    public PagedResult(IEnumerable<T> data, MetaDataDto meta)
        {
            Data = data;
            Meta = meta;
        }
    }
}
