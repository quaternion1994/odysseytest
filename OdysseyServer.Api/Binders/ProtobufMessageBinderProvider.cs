using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace OdysseyServer.Api.Binders
{
    internal class ProtobufMessageBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return typeof(IBufferMessage).IsAssignableFrom(context.Metadata.ModelType) ? new ProtobufMessageBinder() : null;
        }
    }
}
