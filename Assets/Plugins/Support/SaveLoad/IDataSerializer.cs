namespace Support
{
    public interface IDataSerializer
    {
        public string SerializeData<T>(T saveData);
        public T DeserializeData<T>(string serializedSaveData);
    }
}