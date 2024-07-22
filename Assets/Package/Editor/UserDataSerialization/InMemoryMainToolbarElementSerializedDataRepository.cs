using System.Collections.Generic;
using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class InMemoryMainToolbarElementSerializedDataRepository : IMainToolbarElementSerializedDataRepository
    {
        private Dictionary<string, MainToolbarElementSerializedData> _serializedData = 
            new Dictionary<string, MainToolbarElementSerializedData>();

        public void Clear()
        {
            _serializedData.Clear();
        }

        public MainToolbarElementSerializedData[] GetAll()
        {
            return _serializedData.Values.ToArray();
        }

        public void Set(MainToolbarElementSerializedData[] serializedData)
        {
            foreach(var elementData in serializedData)
            {
                if (_serializedData.ContainsKey(elementData.Id))
                    _serializedData[elementData.Id] = elementData;
                else
                    _serializedData.Add(elementData.Id, elementData);
            }
        }
    }
}
