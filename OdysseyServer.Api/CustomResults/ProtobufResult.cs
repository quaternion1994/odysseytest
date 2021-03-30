using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;

namespace OdysseyServer.Api.CustomResults
{
    public class ProtobufResult : FileContentResult
    {
        public ProtobufResult(IMessage message): base(message.ToByteArray(), "application/octet-stream")
        {
        }
    }
}
