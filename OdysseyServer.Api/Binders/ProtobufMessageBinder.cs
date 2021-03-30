using Google.Protobuf;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace OdysseyServer.Api.Binders
{
    internal class ProtobufMessageBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            PropertyInfo? parserProp = bindingContext.ModelType.GetProperty("Parser", BindingFlags.Static | BindingFlags.Public);
            if (parserProp == null) {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            else
            {
                object? parser = parserProp.GetValue(null);
                if (parser == null)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return Task.CompletedTask;
                }

                object? parsedObject = parser.GetType()
                    .GetMethod(nameof(MessageParser.ParseFrom), new Type[] { typeof(Stream) })
                    .Invoke(parser, new object[] {
                    bindingContext.HttpContext.Request.BodyReader.AsStream()
                });
                bindingContext.Result = ModelBindingResult.Success(parsedObject);                
            }
            return Task.CompletedTask;
        }
    }
}
