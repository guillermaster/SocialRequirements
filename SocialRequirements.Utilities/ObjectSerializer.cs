using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SocialRequirements.Utilities
{
    public class ObjectSerializer<T> where T : class, new()
    {
        private T ObjectToSerialize { get; set; }

        public ObjectSerializer(T objectToSerialize)
        {
            ObjectToSerialize = objectToSerialize;
        }

        public ObjectSerializer()
        {
        }

        public string ToXmlString()
        {
            try
            {
                var ser = new XmlSerializer(typeof(T));
                var writer = new StringWriter();
                ser.Serialize(writer, ObjectToSerialize);
                return writer.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public object Deserialize(string serializedXml)
        {
            try
            {
                TextReader reader = new StringReader(serializedXml);
                var ser = new XmlSerializer(typeof(T));
                var obj = ser.Deserialize(reader);
                reader.Close();
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
