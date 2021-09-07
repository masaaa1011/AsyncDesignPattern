using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balking
{
    public interface IWorker
    {
        void Start();
    }
    public class ManualFileSaveClientWorkers : IWorker
    {
        private IData<string> m_data;
        public ManualFileSaveClientWorkers(IData<string> data)
        {
            m_data = data;
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

                    m_data.Save(SaveClassification.Manual);
                    counter++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}{Environment.NewLine}{e.StackTrace}");
                }
            }
        }
    }

    public class AutoSaveServerWorkers : IWorker
    {
        private IData<string> m_data;
        public AutoSaveServerWorkers(IData<string> data)
        {
            m_data = data;
        }
        public async void Start()
        {
            while (true)
            {
                try
                {
                    m_data.Save(SaveClassification.AutoSaved);
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
