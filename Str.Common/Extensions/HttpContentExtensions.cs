using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;


namespace Str.Common.Extensions;


[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedType.Global",   Justification = "This is a library.")]
public static class HttpContentExtensions {

    public static async Task<Stream> GetResponseStreamWithDecompressionAsync(this HttpContent response) {
        Stream responseStream = await response.ReadAsStreamAsync().Fire();

        if (response.Headers.ContentEncoding.Any(h => String.Equals(h, "gzip", StringComparison.InvariantCultureIgnoreCase))) responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
        else if (response.Headers.ContentEncoding.Any(h => String.Equals(h, "deflate", StringComparison.InvariantCultureIgnoreCase))) responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

        return responseStream;
    }

}
