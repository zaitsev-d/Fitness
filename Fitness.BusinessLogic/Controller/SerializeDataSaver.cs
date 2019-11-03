using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BusinessLogic.Controller
{
    class SerializeDataSaver : IDataSaver
    {
        public T Load<T>(string fileName)
        {
            var formatter = new BinaryFormatter();
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0 && formatter.Deserialize(fileStream) is T items)
                {
                    return items;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public void Save(string fileName, object item)
        {
            var formatter = new BinaryFormatter();
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fileStream, item);
            }
        }
    }
}
