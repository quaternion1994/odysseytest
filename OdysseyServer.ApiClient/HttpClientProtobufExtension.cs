using Google.Protobuf;
using System.Net.Http;
using System.Threading.Tasks;

namespace OdysseyServer.ApiClient
{
    public static class HttpClientProtobufExtension
    {
        public static async Task<T> GetFromProtobufAsync<T>(this HttpClient httpClient, MessageParser<T> messageParser, string requestUri) where T : Google.Protobuf.IMessage<T>
        {
            return await Task.FromResult<T>(messageParser.ParseDelimitedFrom(await httpClient.GetStreamAsync(requestUri)));
        }
    }
}
