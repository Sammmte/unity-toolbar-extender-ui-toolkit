using System.Collections.Generic;
using System.Linq;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class InMemoryMainToolbarElementOverrideRepository : IMainToolbarElementOverrideRepository
    {
        private Dictionary<string, MainToolbarElementOverride> _overrides = new Dictionary<string, MainToolbarElementOverride>();

        public MainToolbarElementOverride? Get(string elementId)
        {
            if (!_overrides.ContainsKey(elementId))
                return null;

            return _overrides[elementId];
        }

        public MainToolbarElementOverride[] GetAll()
        {
            return _overrides.Values.ToArray();
        }

        public void Save(MainToolbarElementOverride elementOverride)
        {
            _overrides[elementOverride.ElementId] = elementOverride;
        }

        public void Clear()
        {
            _overrides.Clear();
        }
    }
}