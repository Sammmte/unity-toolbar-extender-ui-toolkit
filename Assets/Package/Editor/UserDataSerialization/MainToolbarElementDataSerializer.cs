using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementDataSerializer
    {
        private readonly IMainToolbarElementSerializedDataRepository _repository;
        private MainToolbarElementWithSerializableVariables[] _elementsWithSerializedVariables = 
            new MainToolbarElementWithSerializableVariables[0];

        public MainToolbarElementDataSerializer(IMainToolbarElementSerializedDataRepository repository)
        {
            _repository = repository;
        }

        public void Start(MainToolbarElement[] mainToolbarElements)
        {
            LoadElementsWithSerializedVariables(mainToolbarElements);
            Reconstruct();
        }

        private void LoadElementsWithSerializedVariables(MainToolbarElement[] mainToolbarElements)
        {
            _elementsWithSerializedVariables = mainToolbarElements
                .Select(m => new MainToolbarElementWithSerializableVariables(m))
                .Where(m => m.VariableCount > 0)
                .ToArray();
        }

        private void Reconstruct()
        {
            var elementsSerializedDataById = _repository.GetAll()
                .Where(e => _elementsWithSerializedVariables.Any(element => element.MainToolbarElement.Id == e.Id))
                .ToDictionary(e => e.Id);

            foreach(var element in _elementsWithSerializedVariables)
            {
                var serializedData = elementsSerializedDataById[element.MainToolbarElement.Id];

                element.LoadFromSerializedData(serializedData);
            }
        }
    }
}
