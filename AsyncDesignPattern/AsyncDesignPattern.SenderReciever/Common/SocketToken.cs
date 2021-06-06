using AsyncDesignPattern.SenderReciever.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Common
{
    public class SocketToken : IToken
    {
        internal SocketToken(Guid id, string body, CommunicationType communicationType)
        {
            Id = id;
            Body = body;
            CommunicationType = communicationType;
        }

        public Guid Id { get; set; }
        public CommunicationType CommunicationType { get; set; }
        public StatusCode StatusType { get; set; }
        public string Body { get; set; }
    }
}
