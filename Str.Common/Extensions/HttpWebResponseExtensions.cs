using System.IO.Compression;
using System.Net;

using JetBrains.Annotations;


namespace Str.Common.Extensions;


[UsedImplicitly]
public static class HttpWebResponseExtensions {

    [UsedImplicitly]
    public static Stream GetResponseStreamWithDecompression(this HttpWebResponse response) {
        Stream responseStream = response.GetResponseStream() ?? throw new Exception("Unable to get Response Stream.");

        if (response.ContentEncoding.Contains("gzip", StringComparison.InvariantCultureIgnoreCase)) responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
        else if (response.ContentEncoding.Contains("deflate", StringComparison.InvariantCultureIgnoreCase)) responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

        return responseStream;
    }

}
