using System.Collections.Generic;
using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class InMemoryMainToolbarElementVariableRepository : IMainToolbarElementVariableRepository
    {
        private Dictionary<string, SerializableElement> _serializableElements = new Dictionary<string, SerializableElement>();

        public SerializableElement[] GetAll()
        {
            return _serializableElements.Values.ToArray();
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
