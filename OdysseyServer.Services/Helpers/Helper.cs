using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OdysseyServer.Services.Helpers
{
    public static class Helper
    {
        public static ByteString ConvertByteArryyToByteString(byte[] array)
        {
            Stream stream = new MemoryStream(array);
            var byteString = ByteString.FromStream(stream);
            return byteString;
        }
    }
}
