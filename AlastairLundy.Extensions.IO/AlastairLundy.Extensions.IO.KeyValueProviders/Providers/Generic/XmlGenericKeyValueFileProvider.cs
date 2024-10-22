using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AlastairLundy.Extensions.IO.KeyValueProviders.Providers.Generic
{
    public class XmlGenericKeyValueFileProvider<TKey, TValue> : IGenericKeyValueFileProvider<TKey, TValue>
    {
        public KeyValuePair<TKey, TValue>[] Get(string pathToFile)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KeyValuePair<TKey, TValue>>));

#if NETSTANDARD2_1 || NET6_0_OR_GREATER
            KeyValuePair<TKey, TValue>[]? data;
#elif NETSTANDARD2_0
                KeyValuePair<TKey, TValue>[] data;            
#endif
            
                using (Stream reader = new FileStream(pathToFile, FileMode.Open, FileAccess.Read))
                {
                    // Call the Deserialize method to restore the object's state.
                    data = (KeyValuePair<TKey, TValue>[])xmlSerializer.Deserialize(reader);
                }

                if (data == null)
                {
                    throw new NullReferenceException($"{nameof(data)} was null");
                }

                return data;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw;
            }
        }

        public void WriteToFile(KeyValuePair<TKey, TValue>[] data, string pathToFile)
        {
            try
            {
                FileStream fileStream = new FileStream(pathToFile, FileMode.Create);
            
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<KeyValuePair<TKey, TValue>>));
                xmlSerializer.Serialize(fileStream, data);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw;
            }
        }
    }
}