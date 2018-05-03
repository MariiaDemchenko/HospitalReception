using System.Collections.Generic;

namespace PhotoManager.Common
{
    public class CollectionModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}