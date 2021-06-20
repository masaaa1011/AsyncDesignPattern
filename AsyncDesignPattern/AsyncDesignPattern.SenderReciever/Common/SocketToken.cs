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
        public SocketToken(Guid id, DesingPatternType desingPatternType, StatusCode statusCode, string body ,string eof = "<EOF>")
        {
            Id = id;
            DesingPatternType = desingPatternType;
            StatusCode = statusCode;
            Payload = body;
            EOF = eof;
        }

        public Guid? Id { get; set; }
        public DesingPatternType? DesingPatternType { get; set; }
        public StatusCode? StatusCode { get; set; }
        public string? Payload { get; set; }
        public string EOF { get; set; }
    }
}
