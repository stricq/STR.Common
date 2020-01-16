using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Str.Common.Extensions {

  [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
  [SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This is a library.")]
  public static class HttpContentExtensions {

    public static async Task<Stream> GetResponseStreamWithDecompressionAsync(this HttpContent response) {
      Stream responseStream = await response.ReadAsStreamAsync().Fire();

      if (response.Headers.ContentEncoding.Any(h => h.ToLower() == "gzip")) responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
      else if (response.Headers.ContentEncoding.Any(h => h.ToLower() == "deflate")) responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

      return responseStream;
    }

  }

}
