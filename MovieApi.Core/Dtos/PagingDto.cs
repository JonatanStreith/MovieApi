using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Dtos
{
    public class PagingDto
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
    }
}
