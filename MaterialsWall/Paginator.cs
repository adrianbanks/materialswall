﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Granta.MaterialsWall
{
    public interface IPaginator<T>
    {
        IEnumerable<T> GetPage(int pageSize, int pageNumber, IEnumerable<T> items);
    }

    public sealed class Paginator<T> : IPaginator<T>
    {
        public IEnumerable<T> GetPage(int pageSize, int pageNumber, IEnumerable<T> items)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("Page size must be a positive integer", "pageSize");
            }
            
            if (pageNumber < 1)
            {
                throw new ArgumentException("Page number must be a positive integer", "pageNumber");
            }
            
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            
            int numberOfItemsToSkip = (pageNumber - 1) * pageSize;
            var pagedItems = items.Skip(numberOfItemsToSkip).Take(pageSize);
            return pagedItems;
        }
    }
}
