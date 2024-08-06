using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class SerializableTypesHelper
    {
        private static readonly Type[] PRIMITIVE_SERIALIZABLE_TYPES = new[]
        {
            typeof(string),
            typeof(bool),
            typeof(char),
            typeof(int),
            typeof(short),
            typeof(long),
            typeof(byte),
            typeof(uint),
            typeof(ushort),
            typeof(ulong),
            typeof(sbyte),
            typeof(float),
            typeof(double),
        };

        public static bool IsValidType(Type type)
        {
            return IsPrimitive(type) || 
                IsValidArrayType(type) ||
                IsValidListType(type) ||
                IsValidDictionaryType(type);
        }

        private static bool IsPrimitive(Type type)
        {
            return PRIMITIVE_SERIALIZABLE_TYPES.Contains(type);
        }

        private static bool IsValidArrayType(Type type)
        {
            return type.IsArray && IsPrimitive(type.GetElementType());
        }

        private static bool IsValidListType(Type type)
        {
            if (!(type == typeof(List<>)))
                return false;

            var typeArg = type.GetGenericArguments()[0];

            return IsPrimitive(typeArg);
        }

        private static bool IsValidDictionaryType(Type type)
        {
            if (!(type == typeof(Dictionary<,>)))
                return false;

            var typeArg1 = type.GetGenericArguments()[0];
            var typeArg2 = type.GetGenericArguments()[0];

            return IsPrimitive(typeArg1) &&
                IsPrimitive(typeArg2);
        }

        public static bool AreEqual(object obj1, object obj2)
        {
            var obj1Type = obj1.GetType();
            var obj2Type = obj2.GetType();

            if (obj1Type != obj2Type)
                return false;

            if (obj1Type.IsArray || obj1Type == typeof(List<>))
            {
                Type elementType = null;

                if(obj2Type.IsArray)
                    elementType = obj1Type.GetElementType();
                else
                    elementType = obj1Type.GetGenericArguments().First();

                var comparisonMethod = typeof(SerializableTypesHelper).GetMethod(nameof(AreArrayOrListEqual),
                    BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(elementType);

                var areEqual = (bool)comparisonMethod.Invoke(null, new object[] { obj1, obj2 });

                return areEqual;
            }

            return obj1.Equals(obj2);
        }

        private static bool AreArrayOrListEqual<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
        {
            return Enumerable.SequenceEqual<T>(enumerable1, enumerable2);
        }
    }
}
