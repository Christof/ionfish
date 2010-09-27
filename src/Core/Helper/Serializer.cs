using System.IO;
using System.Xml.Serialization;

namespace Core.Helper
{
    public class Serializer
    {
        public static void SerializeXml<T>(string filename, T toSerialize)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, toSerialize);
            }
        }

        public static T DeserializeXml<T>(string filename)
        {
            var deserializer = new XmlSerializer(typeof(T));

            using (var reader = new StreamReader(filename))
            {
                return (T)deserializer.Deserialize(reader);
            }
        }
    }
}