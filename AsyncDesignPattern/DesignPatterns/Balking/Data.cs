using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balking
{
    public interface IChangeable<TType>
    {
        void Change(TType value);
        bool IsChanged { get; }
    }
    public interface ISavable
    {
        void Save(SaveClassification user);
    }
    public interface IData<TType> : IChangeable<TType>, ISavable
    {
        string Title { get; }
        TType Content {  get; }
    }
    public class Data : IData<string>
    {
        private string m_title;
        private string m_content;
        private IDataSavable<string> m_saver;
        private bool m_isChanged;
        private object _lock = new object();
        public Data(string title, string content, IDataSavable<string> saver)
        {
            m_title = title;
            m_content = content;
            m_saver = saver;
            m_isChanged = false;
        }
        public string Title => m_title;
        public string Content => m_content;
        public bool IsChanged => m_isChanged;
        public void Change(string value)
        {
            lock (_lock)
            {
                m_content = value;
                m_isChanged = true;
            }
        }
        public void Save(SaveClassification user)
        {
            lock (_lock)
            {
                m_saver.Save(this, user);
                m_isChanged = false;
            }
        }
    }
    public enum SaveClassification
    {
        Manual = 1,
        AutoSaved = 2,
    }
}
