using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OrderManagement.Addin.Utilities.Serialization {
    public class XmlConverter : ISerializer {
        /// <summary>
        /// Serialize object into XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public string Serialize<T>(T graph) {
            var serializer = new XmlSerializer(typeof(T));
            using (var textWriter = new StringWriter()) {
                using (var xmlWriter = XmlWriter.Create(textWriter)) {
                    serializer.Serialize(xmlWriter, graph);
                }

                return textWriter.ToString();
            }
        }
        /// <summary>
        /// Deserialize string xml 
        /// </summary>
        /// <typeparam name="T">Object type of the serialized xml</typeparam>
        /// <param name="xml">Xml value</param>
        /// <returns>Deserialized object value</returns>
        public T Deserialize<T>(string xml) {
            using (var stringReader = new StringReader(xml))
            using (var xmlReader = new XmlTextReader(stringReader))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var obj = xmlSerializer.Deserialize(xmlReader);
                return (T)obj;
            }
        }
    }
}