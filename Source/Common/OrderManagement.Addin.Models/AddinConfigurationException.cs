namespace OrderManagement.Addin.Models {
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AddinConfigurationException : BaseException {
        public AddinConfigurationException(string code) : base(code) { }
        public AddinConfigurationException(string code, string message) : base(code, message) { }

        public AddinConfigurationException(string code, string message, Exception inner) :
            base(code, message, inner) { }

        protected AddinConfigurationException(SerializationInfo info, StreamingContext context) :
            base(info, context) { }
    }
}