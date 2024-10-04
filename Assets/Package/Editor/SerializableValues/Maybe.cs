namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class Maybe<T>
    {
        public static Maybe<T> None() => new Maybe<T>(false, default);
        public static Maybe<T> Something(T value) => new Maybe<T>(true, value);

        public bool HasValue { get; }
        public T Value { get; }

        private Maybe(bool hasValue, T value)
        {
            HasValue = hasValue;
            Value = value;
        }
    }
}
