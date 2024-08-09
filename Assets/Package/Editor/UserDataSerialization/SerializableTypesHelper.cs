using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

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
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>))
                return false;

            var typeArg = type.GetGenericArguments()[0];
            
            return IsPrimitive(typeArg);
        }

        private static bool IsValidDictionaryType(Type type)
        {
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                return false;

            var typeArg1 = type.GetGenericArguments()[0];
            var typeArg2 = type.GetGenericArguments()[1];

            return IsPrimitive(typeArg1) &&
                IsPrimitive(typeArg2);
        }

        public static bool AreEqual(object obj1, object obj2)
        {
            var obj1Type = obj1.GetType();
            var obj2Type = obj2.GetType();

            if (obj1Type != obj2Type)
                return false;

            if (IsPrimitive(obj1Type))
                return obj1.Equals(obj2);
            else
            {
                var elementType = GetElementTypeOf(obj1Type);
                return AreColletionsEqual(obj1, obj2, elementType);
            }
        }

        private static Type GetElementTypeOf(Type type)
        {
            if (type.IsArray)
                type.GetElementType();
            else if (type.IsGenericType)
                if (type.GetGenericTypeDefinition() == typeof(List<>))
                    return type.GetGenericArguments()[0];
                else if (type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    var genericArguments = type.GetGenericArguments();
                    return CreateTypeForKeyValuePair(genericArguments[0], genericArguments[1]);
                }

            return null;
        }

        private static Type CreateTypeForKeyValuePair(Type keyType, Type valueType)
        {
            return typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);
        }

        private static bool AreColletionsEqual(object obj1, object obj2, Type elementType)
        {
            var comparisonMethod = typeof(SerializableTypesHelper).GetMethod(nameof(SequenceEqual),
                BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(elementType);

            var areEqual = (bool)comparisonMethod.Invoke(null, new object[] { obj1, obj2 });

            return areEqual;
        }

        private static bool SequenceEqual<T>(IEnumerable<T> enumerable1, IEnumerable<T> enumerable2)
        {
            return Enumerable.SequenceEqual<T>(enumerable1, enumerable2);
        }
    }
}
