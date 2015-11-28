using System;
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
    }
}
