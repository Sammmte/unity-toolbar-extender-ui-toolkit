using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEditor;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementSerializationSyncService
    {
        private readonly IMainToolbarElementSerializedDataRepository _repository;
        private readonly IMainToolbarElementDataSerializer _serializer;
        private MainToolbarElementWithSerializableVariables[] _elementsWithSerializedVariables = 
            new MainToolbarElementWithSerializableVariables[0];

        private List<MainToolbarElementSerializedData> _changedElements = new List<MainToolbarElementSerializedData>();

        private Timer _updateTimer;

        public MainToolbarElementSerializationSyncService(IMainToolbarElementSerializedDataRepository repository,
            IMainToolbarElementDataSerializer serializer)
        {
            _repository = repository;
            _serializer = serializer;
        }

        private Timer CreateUpdateTimer()
        {
            var timer = new Timer(500);

            timer.AutoReset = true;
            timer.Elapsed += OnTimerUpdate;

            return timer;
        }

        private void OnTimerUpdate(object sender, ElapsedEventArgs e)
        {
            CacheChangedElementsIfAny();
        }

        private void SaveChangedElementsIfAny()
        {
            if(_changedElements.Count > 0)
            {
                _repository.Set(_changedElements.ToArray());
                _changedElements.Clear();
            }
        }

        public void Start(MainToolbarElement[] mainToolbarElements)
        {
            LoadElementsWithSerializedVariables(mainToolbarElements);
            Reconstruct();
            OverrideRepositoryState();
            EditorApplication.update += SaveChangedElementsIfAny;
            _updateTimer = CreateUpdateTimer();
            _updateTimer.Start();
        }

        public void Stop()
        {
            EditorApplication.update -= SaveChangedElementsIfAny;
            _updateTimer?.Stop();
            _updateTimer?.Dispose();
            _elementsWithSerializedVariables = new MainToolbarElementWithSerializableVariables[0];
        }

        private void LoadElementsWithSerializedVariables(MainToolbarElement[] mainToolbarElements)
        {
            _elementsWithSerializedVariables = mainToolbarElements
                .Select(m => new MainToolbarElementWithSerializableVariables(m, _serializer))
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
        
        private void CacheChangedElementsIfAny()
        {
            foreach(var element in _elementsWithSerializedVariables)
            {
                if (element.CheckAndUpdateModifiedValues())
                {
                    _changedElements.Add(element.GetSerializedData());
                }
            }
        }
    }
}
