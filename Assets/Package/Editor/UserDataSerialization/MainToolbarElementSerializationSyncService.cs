using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementSerializationSyncService
    {
        private const float UPDATE_TIME_IN_SECONDS = 0.5f;

        private readonly IMainToolbarElementSerializedDataRepository _repository;
        private readonly IMainToolbarElementDataSerializer _serializer;
        private readonly IClonator _clonator;
        private MainToolbarElementWithSerializableVariables[] _elementsWithSerializedVariables = 
            new MainToolbarElementWithSerializableVariables[0];

        private double _nextUpdateTime;

        public MainToolbarElementSerializationSyncService(IMainToolbarElementSerializedDataRepository repository,
            IMainToolbarElementDataSerializer serializer, IClonator clonator)
        {
            _repository = repository;
            _serializer = serializer;
            _clonator = clonator;
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
                .Select(m => new MainToolbarElementWithSerializableVariables(m, _serializer, _clonator))
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
            if(EditorApplication.timeSinceStartup > _nextUpdateTime)
            {
                CheckAndSaveChangedElements();
                _nextUpdateTime = EditorApplication.timeSinceStartup + UPDATE_TIME_IN_SECONDS;
            }
        }

        private void CheckAndSaveChangedElements()
        {
            List<MainToolbarElementSerializedData> changedElements = null;

            foreach (var element in _elementsWithSerializedVariables)
            {
                if (element.CheckAndUpdateModifiedValues())
                {
                    if (changedElements == null)
                        changedElements = new List<MainToolbarElementSerializedData>();

                    changedElements.Add(element.GetSerializedData());
                }
            }

            if (changedElements != null)
                _repository.Set(changedElements.ToArray());
        }
    }
}
