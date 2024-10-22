using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace AlastairLundy.Extensions.IO.KeyValueProviders.Providers.Generic
{
    public class JsonGenericKeyValueFileProvider<TKey, TValue> : IGenericKeyValueFileProvider<TKey, TValue>
    {
        public KeyValuePair<TKey, TValue>[] Get(string pathToFile)
        {
            string json = File.ReadAllText(pathToFile);

            if (json.Contains("key:"))
            {
                json = json.Replace("key:", "Key:");
            }

            if (json.Contains("value:"))
            {
                json = json.Replace("value:", "Value:");
            }

            KeyValuePair<TKey, TValue>[] data  = JsonSerializer.Deserialize<>(json);

            return data;
        }

        public void WriteToFile(KeyValuePair<TKey, TValue>[] data, string pathToFile)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (KeyValuePair<TKey, TValue> keyValuePair in data)
                {
                    JsonElement jsonElement = JsonElement.
                }

                File.WriteAllText(pathToFile, contents);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                throw;
            }
        }
    }
}