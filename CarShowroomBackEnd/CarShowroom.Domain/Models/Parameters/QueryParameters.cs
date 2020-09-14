﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Domain.Models.Parameters
{
    public class QueryParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }

    }
}
