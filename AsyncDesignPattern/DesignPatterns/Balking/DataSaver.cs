using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balking
{
    public interface IDataSavable
    {
        void Save(IData<string> data, SaveClassification user);
    }
    public class FileDataSaver : IDataSavable
    {
        private string m_directory;
        private object _lock = new object();
        public FileDataSaver(string directory)
        {
            m_directory = directory;
        }
        public void Save(IData<string> data, SaveClassification user)
        {
            if (!data.IsChanged) return;
            try
            {
                lock (_lock)
                {
                    if (!Directory.Exists(m_directory)) Directory.CreateDirectory(m_directory);

                    System.IO.File.WriteAllText(Path.Combine(m_directory, data.Title), data.Content);
                    data.Save();
                    Console.WriteLine($"{user}で追記された内容: {data.Content}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}{Environment.NewLine}{e.StackTrace}");
                throw;
            }
        }
    }
}
