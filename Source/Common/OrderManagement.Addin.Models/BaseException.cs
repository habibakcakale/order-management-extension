namespace OrderManagement.Addin.Models {
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class BaseException : Exception {
        public string Code { get; set; }

        public BaseException(string code) {
            this.Code = code;
        }

        public BaseException(string code, string message) : base(message) {
            this.Code = code;
        }

        public BaseException(string code, string message, Exception inner) : base(message, inner) {
            this.Code = code;
        }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}