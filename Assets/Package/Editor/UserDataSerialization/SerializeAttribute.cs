using System;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SerializeAttribute : Attribute
    {
        public string Id { get; private set; }

        public SerializeAttribute(string id = null)
        {
            SetId(id);
        }

        internal void SetId(string id)
        {
            Id = id;
        }
    }
}
