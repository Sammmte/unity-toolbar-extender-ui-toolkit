namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarElementWithSerializableVariables
    {
        public MainToolbarElement MainToolbarElement;
        public FieldSerializableVariable[] Fields;
        public PropertySerializableVariable[] Properties;

        public bool DidChange()
        {
            foreach (var field in Fields)
                if (field.DidChange())
                    return true;

            foreach (var property in Properties)
                if (property.DidChange()) 
                    return true;

            return false;
        }
    }
}
