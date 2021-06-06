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
        public static SocketToken CreateToken(Guid id, object payload, CommunicationType communicationType)
            => new SocketToken(
                    id: id,
                    body: JsonSerializer.Serialize(payload),
                    communicationType: communicationType
                );
    }
}
