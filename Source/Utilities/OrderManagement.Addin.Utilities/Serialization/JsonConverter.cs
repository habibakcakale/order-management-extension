namespace OrderManagement.Addin.Utilities.Serialization {
    using System;

    public class JsonConverter : ISerializer {
        public string Serialize<T>(T graph) {
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string value) {
            throw new NotImplementedException();
        }
    }
}