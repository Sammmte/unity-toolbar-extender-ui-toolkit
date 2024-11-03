using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal abstract class Variable
    {
        private object _lastValue;
        private IEqualityComparer _equalityComparer;
        private bool _isCollection;
        private MethodInfo _collectionsCompareMethod;

        public MainToolbarElement Element { get; }
        public Type ValueType { get; }

        public Variable(MainToolbarElement element, Type valueType, object initialValue, IEqualityComparer equalityComparer)
        {
            Element = element;
            ValueType = valueType;
            _lastValue = initialValue;
            _equalityComparer = equalityComparer;

            _isCollection = IsCollection();

            if (_isCollection)
            {
                _lastValue = initialValue == null ? null : CloneCollection(initialValue as ICollection);
                _collectionsCompareMethod = typeof(Variable).GetMethod(nameof(AreCollectionsEqualByValue), BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(ValueType);
            }
            else
            {
                _lastValue = initialValue;
            }
        }

        public abstract object Get();

        public abstract void Set(object value);

        public bool DidChange()
        {
            var currentValue = Get();

            if (_isCollection)
                return !((bool)_collectionsCompareMethod.Invoke(this, new object[] { currentValue, _lastValue }));
            else
                return !AreSingleObjectsEqual(currentValue, _lastValue);
        }

        private bool AreCollectionsEqualByValue<T>(object object1, object object2)
        {
            if(object1 == null)
                return object2 == null || (object2 as ICollection).Count == 0;
            else if(object2 == null)
                return object1 == null || (object1 as ICollection).Count == 0;

            var collection1 = object1 as ICollection;
            var collection2 = object2 as ICollection;

            if(collection1.Count != collection2.Count) 
                return false;

            var enumerator1 = collection1.GetEnumerator();
            var enumerator2 = collection2.GetEnumerator();

            while (enumerator1.MoveNext())
            {
                enumerator2.MoveNext();

                var current1 = enumerator1.Current;
                var current2 = enumerator2.Current;

                if (!AreSingleObjectsEqual(current1, current2))
                    return false;
            }

            return true;
        }

        private bool IsCollection()
        {
            return typeof(ICollection).IsAssignableFrom(ValueType);
        }

        public void UpdateValue()
        {
            if(IsCollection())
                _lastValue = CloneCollection(Get() as ICollection);
            else
                _lastValue = Get();
        }

        private List<object> CloneCollection(ICollection collection)
        {
            List<object> clone = null;

            foreach( var item in collection)
            {
                if(clone == null)
                    clone = new List<object>();

                clone.Add(item);
            }

            return clone;
        }

        private bool AreSingleObjectsEqual(object value1, object value2)
        {
            if (value1 == null || value2 == null)
                return value1 == value2;
            else if (_equalityComparer != null)
                return _equalityComparer.Equals(value1, value2);
            else
                return value1.Equals(value2);
        }
    }
}
