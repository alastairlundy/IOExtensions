using System.Collections.Generic;

namespace AlastairLundy.Extensions.IO.KeyValueProviders.Providers.Generic
{
    public class TextGenericKeyValueFileProvider<TKey, TValue> : IGenericKeyValueFileProvider<TKey, TValue>
    {
        public KeyValuePair<TKey, TValue>[] Get(string pathToFile)
        {
            throw new System.NotImplementedException();
        }

        public void WriteToFile(KeyValuePair<TKey, TValue>[] data, string pathToFile)
        {
            throw new System.NotImplementedException();
        }
    }
}