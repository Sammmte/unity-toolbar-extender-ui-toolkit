using System.Collections.Generic;
using System.Linq;
using UnityEditor;

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
            OverrideRepositoryState();
            EditorApplication.update += Update;
        }

        public void Stop()
        {
            EditorApplication.update -= Update;
            _elementsWithSerializedVariables = new MainToolbarElementWithSerializableVariables[0];
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
                if (!elementsSerializedDataById.ContainsKey(element.MainToolbarElement.Id))
                    continue;

                var serializedData = elementsSerializedDataById[element.MainToolbarElement.Id];

                element.LoadFromSerializedData(serializedData);
            }
        }

        private void OverrideRepositoryState()
        {
            _repository.Clear();

            _repository.Set(_elementsWithSerializedVariables
                .Select(e => e.GetSerializedData())
                .ToArray());
        }
        
        private void Update()
        {
            List<MainToolbarElementSerializedData> changedElements = null;

            foreach(var element in _elementsWithSerializedVariables)
            {
                if (element.CheckAndUpdateModifiedValues())
                {
                    if (changedElements == null)
                        changedElements = new List<MainToolbarElementSerializedData>();

                    changedElements.Add(element.GetSerializedData());
                }
            }

            if(changedElements != null)
            {
                _repository.Set(changedElements.ToArray());
            }
        }
    }
}
