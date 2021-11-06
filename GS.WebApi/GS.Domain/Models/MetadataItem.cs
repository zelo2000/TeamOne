using System;

namespace GS.Domain.Models
{
    public class MetadataItem<T> where T : Enum
    {
        public string Name { get; set; }

        public T Value { get; set; }

        public int SortingOrder { get; set; }
    }
}
