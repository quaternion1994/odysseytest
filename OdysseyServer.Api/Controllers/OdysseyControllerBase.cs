using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using OdysseyServer.Api.CustomResults;

namespace OdysseyServer.Api.Controllers
{
    public abstract class OdysseyControllerBase : ControllerBase
    {
        [NonAction]
        protected virtual ProtobufResult Protobuf(IMessage message)
        {
            return new ProtobufResult(message);
        }
    }
}
