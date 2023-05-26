using Newtonsoft.Json;

namespace Ingame
{
    public sealed class JsonSerializer : IDataSerializer
    {
        public string SerializeData<T>(T saveData)
        {
            return JsonConvert.SerializeObject(saveData,Formatting.Indented);
        }

        public T DeserializeData<T>(string serializedSaveData)
        {
            return JsonConvert.DeserializeObject<T>(serializedSaveData);
        }
    }
}