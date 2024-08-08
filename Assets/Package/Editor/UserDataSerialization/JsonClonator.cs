using Newtonsoft.Json;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class JsonClonator : IClonator
    {
        public object Clone(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject(json, obj.GetType());
        }
    }
}
