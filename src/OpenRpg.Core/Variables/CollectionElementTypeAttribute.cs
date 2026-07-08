using System;

namespace OpenRpg.Core.Variables
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CollectionElementTypeAttribute : Attribute
    {
        public Type ElementType { get; }
        public CollectionElementTypeAttribute(Type elementType) => ElementType = elementType;
    }
}
