namespace OrderManagement.Addin.Utilities.Serialization {
    public interface ISerializer {
        string Serialize<T>(T graph);
        T Deserialize<T>(string value);
    }
}