using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class SerializableTypes
    {
        public static readonly Type[] SERIALIZABLE_TYPES = new[]
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
    }
}
