using System.Collections.Generic;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class InMemoryMainToolbarElementVariableRepository : IMainToolbarElementVariableRepository
    {
        private Dictionary<string, SerializableElement> _serializableElements = new Dictionary<string, SerializableElement>();

        public SerializableElement? Get(MainToolbarElement element)
        {
            if(_serializableElements.ContainsKey(element.GetType().FullName))
                return _serializableElements[element.GetType().FullName];

            return null;
        }

        public void Save()
        {
            
        }

        public void Set(SerializableElement serializableElement)
        {
            _serializableElements[serializableElement.ElementFullTypeName] = serializableElement;
        }
    }
}
