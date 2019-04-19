using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Str.Common.Extensions {

  public static class HttpContentExtensions {

    public static async Task<Stream> GetResponseStreamWithDecompressionAsync(this HttpContent response) {
      Stream responseStream = await response.ReadAsStreamAsync();

      if (response.Headers.ContentEncoding.Any(h => h.ToLower() == "gzip")) responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
      else if (response.Headers.ContentEncoding.Any(h => h.ToLower() == "deflate")) responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

      return responseStream;
    }

  }

}
