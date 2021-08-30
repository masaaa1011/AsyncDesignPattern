using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balking
{
    public interface IDataSavable<TType>
    {
        void Save(IData<TType> data, SaveClassification user);
    }
    public class FileDataSaver : IDataSavable<string>
    {
        private string m_directory;
        private object _lock = new object();
        public FileDataSaver(string directory)
        {
            m_directory = directory;
        }
        /// <summary>
        /// if you wanna watch, using terminal with command, "while ($true -eq $true) { Get-Content .\sharedFile.txt ; sleep 1 ; clear}"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public void Save(IData<string> data, SaveClassification user)
        {
            if (!data.IsChanged) return;
            try
            {
                lock (_lock)
                {
                    if (!Directory.Exists(m_directory)) Directory.CreateDirectory(m_directory);
                    System.IO.File.WriteAllText(Path.Combine(m_directory, data.Title), data.Content);
                    data.SendSavedSignal();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}{Environment.NewLine}{e.StackTrace}");
                throw;
            }
        }
    }
    public class ConsoleDataSaver : IDataSavable<string>
    {
        private string m_directory;
        private object _lock = new object();
        public ConsoleDataSaver(string directory)
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
                    data.SendSavedSignal();
                    Console.WriteLine($"{user}で保存された内容: {data.Content}");
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
