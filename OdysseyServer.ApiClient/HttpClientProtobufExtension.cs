using Google.Protobuf;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OdysseyServer.ApiClient
{
    public static class HttpClientProtobufExtension
    {
        public static async Task<T> GetFromProtobufAsync<T>(this HttpClient httpClient, MessageParser<T> messageParser, string requestUri) where T : Google.Protobuf.IMessage<T>
        {
            return await Task.FromResult<T>(messageParser.ParseFrom(await httpClient.GetStreamAsync(requestUri)));
        }

        public static async Task<HttpResponseMessage> PostProtobufAsync<T>(this HttpClient httpClient, string requestUri, IMessage<T> message) where T : Google.Protobuf.IMessage<T>
        {
            ByteArrayContent messageBytes = new ByteArrayContent(message.ToByteArray());
            messageBytes.Headers.ContentType = new MediaTypeHeaderValue("application/x-protobuf");

            return await httpClient.PostAsync(requestUri, messageBytes);
        }

        public static async Task<HttpResponseMessage> PutProtobufAsync<T>(this HttpClient httpClient, string requestUri, IMessage<T> message) where T : Google.Protobuf.IMessage<T>
        {
            ByteArrayContent messageBytes = new ByteArrayContent(message.ToByteArray());
            messageBytes.Headers.ContentType = new MediaTypeHeaderValue("application/x-protobuf");

            return await httpClient.PutAsync(requestUri, messageBytes);
        }
    }
}
