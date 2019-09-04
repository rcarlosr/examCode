using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchFightOOP
{
        public class Serializer
        {
            public T Deserialize<T>(string input) where T : class
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

                    using (StringReader sr = new StringReader(input))
                    {
                        return (T)ser.Deserialize(sr);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            }
        }
}
