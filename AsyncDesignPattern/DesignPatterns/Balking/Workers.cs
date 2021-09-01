using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balking
{
    public interface IClientWorker
    {
        void Start();
    }

    public class ManualFileSaveClientWorkers : IClientWorker
    {
        private string m_directory;
        private IData<string> m_data;
        private IDataSavable<string> m_saver;
        public ManualFileSaveClientWorkers(string directory, IData<string> data, IDataSavable<string> saver)
        {
            m_directory = directory;
            m_data = data;
            m_saver = saver;
        }
        public async void Start()
        {
            var counter = 0;
            while (true)
            {
                try
                {
                    var content = string.Join(string.Empty, "qawzsexdrcftvgybhunjimkolp".OrderBy(o => Guid.NewGuid()));
                    m_data.Change($"Count:{counter}回目 - {content}");

                    await Task.Delay(2000);

                    m_saver.Save(m_data, SaveClassification.Manual);
                    counter++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}{Environment.NewLine}{e.StackTrace}");
                }
            }
        }
    }
    public interface IServerWorker
    {
        void Start();
    }
    public class AutoSaveServerWorkers : IServerWorker
    {
        private string m_directory;
        private IData<string> m_data;
        private IDataSavable<string> m_saver;

        public AutoSaveServerWorkers(string directory, IData<string> data, IDataSavable<string> saver)
        {
            m_directory = directory;
            m_data = data;
            m_saver = saver;
        }
        public async void Start()
        {
            while (true)
            {
                try
                {
                    m_saver.Save(m_data, SaveClassification.AutoSaved);
                    await Task.Delay(5000);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}{Environment.NewLine}{e.StackTrace}");
                }
            }
        }
    }
}
