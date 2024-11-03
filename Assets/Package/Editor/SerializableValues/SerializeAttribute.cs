using System;
using System.Collections;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class SerializeAttribute : Attribute
    {
        private readonly Type _equalityComparerType;

        public SerializeAttribute()
        {

        }

        public SerializeAttribute(Type equalityComparerType)
        {
            _equalityComparerType = equalityComparerType;
        }

        internal bool ContainsEqualityComparerType()
        {
            return _equalityComparerType != null;
        }

        internal IEqualityComparer CreateEqualityComparer()
        {
            return Activator.CreateInstance(_equalityComparerType) as IEqualityComparer;
        }
    }
}
