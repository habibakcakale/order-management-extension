namespace OrderManagement.Addin.Utilities {
    using System.IO;
    using System.Text;
    using Models;
    using OrderManagement.Addin.Utilities.Serialization;

    public static class Extensions {
        const string InvalidPathRead = "InvalidPathRead", InvalidPathWrite = "InvalidPathWrite";
        /// <summary>
        /// Read Xml file and deserialize to given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath">Path to xml file</param>
        /// <returns></returns>
        public static T ReadXml<T>(string xmlPath) {
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new AddinConfigurationException(InvalidPathRead, $"Invalid Path {xmlPath}");

            //file access read only, will not lock or will not throw an exception if another process already has locked.
            using (var fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs, Encoding.UTF8)) {
                var xml = reader.ReadToEnd();
                ISerializer serializer = new XmlConverter();
                return serializer.Deserialize<T>(xml);
            }
        }

        /// <summary>
        /// Write given object to specified path
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="graph"></param>
        public static void WriteXml<T>(string xmlPath, T graph) {
            if (string.IsNullOrWhiteSpace(xmlPath))
                throw new AddinConfigurationException(InvalidPathWrite, $"Invalid Path {xmlPath}");

            var directory = Path.GetDirectoryName(xmlPath);
            if (string.IsNullOrWhiteSpace(directory))
                throw new AddinConfigurationException(InvalidPathWrite, $"Invalid Path {xmlPath}");

            ISerializer serializer = new XmlConverter();
            File.WriteAllText(xmlPath, serializer.Serialize(graph), Encoding.UTF8);
        }
    }
}