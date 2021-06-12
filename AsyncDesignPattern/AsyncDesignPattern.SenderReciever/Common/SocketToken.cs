using AsyncDesignPattern.Common.Enum;
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
        
        public SocketToken() { }
        public SocketToken(Guid id, string body, DesingPatternType desingPatternType)
        {
            Id = id;
            Body = body;
            DesingPatternType = desingPatternType;
        }

        public Guid Id { get; set; }
        public DesingPatternType DesingPatternType { get; set; }
        public StatusCode StatusType { get; set; }
        public string Body { get; set; }
    }
}
