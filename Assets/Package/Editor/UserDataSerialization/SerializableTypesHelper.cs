using System;
using System.Collections.Generic;
using System.Linq;

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

        public static bool TryChangeType(object obj, Type newType, out object result)
        {
            result = null;

            if (obj == null) 
                return false;

            var objType = obj.GetType();

            if(IsPrimitive(objType) && IsPrimitive(newType))
            {
                try
                {
                    result = Convert.ChangeType(obj, newType);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            if((IsValidArrayType(objType) || IsValidListType(objType)) && 
                (IsValidArrayType(newType) || IsValidListType(newType)))
            {
                var arrayToCast = (object[])obj;
                var castedArray = new object[arrayToCast.Length];

                var typeOfNewArrayType = newType.GetElementType();

                for(int i = 0; i < arrayToCast.Length; i++)
                {
                    var element = arrayToCast[i];

                    if (!TryChangeType(element, typeOfNewArrayType, out element))
                        return false;

                    castedArray[i] = element;
                }

                if (newType.IsArray)
                    result = castedArray;
                else
                    result = castedArray.ToList();

                return true;
            }

            return false;
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
    }
}
