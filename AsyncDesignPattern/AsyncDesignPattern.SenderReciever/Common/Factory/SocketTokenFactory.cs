using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Common.Factory
{
    public static class SocketTokenFactory
    {
        public static SocketToken CreateToken(Guid id, object payload, DesingPatternType desingPatternType)
            => new SocketToken(
                    id: id,
                    body: JsonSerializer.Serialize(payload),
                    desingPatternType: desingPatternType
                );
    }
}
