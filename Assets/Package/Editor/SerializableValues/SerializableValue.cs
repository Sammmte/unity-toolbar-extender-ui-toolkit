namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class SerializableValue<T>
    {
        public static SerializableValue<T> Create(string saveKey) => Create(saveKey, default);
        public static SerializableValue<T> Create(string saveKey, T defaultValue) => Create(saveKey, defaultValue, defaultValue);
        public static SerializableValue<T> Create(string saveKey, T defaultValue, T initialValue)
        {
            var serializableValue = new SerializableValue<T>(ServicesAndRepositories.SerializableValuesRepository, saveKey, defaultValue);

            if(!serializableValue.HasValue())
                serializableValue.Value = initialValue;

            return serializableValue;
        }

        private readonly ISerializableValuesRepository _repository;
        private string _saveKey;
        private T _defaultValue;

        public T Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        internal SerializableValue(ISerializableValuesRepository repository, string saveKey, T defaultValue)
        {
            _repository = repository;
            _saveKey = saveKey;
            _defaultValue = defaultValue;
        }

        private T GetValue()
        {
            var maybeValue = _repository.Get<T>(_saveKey);

            if (!maybeValue.HasValue)
                return _defaultValue;

            return maybeValue.Value;
        }

        public bool HasValue()
        {
            var maybeValue = _repository.Get<T>(_saveKey);

            return maybeValue.HasValue;
        }

        private void SetValue(T value)
        {
            _repository.Save(_saveKey, value);
        }
    }
}
