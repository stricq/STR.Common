using System;
using System.IO;
using System.IO.Compression;
using System.Net;


namespace Str.Common.Extensions {

  public static class HttpWebResponseExtensions {

    public static Stream GetResponseStreamWithDecompression(this HttpWebResponse response) {
      Stream responseStream = response.GetResponseStream();

      if (responseStream == null) throw new Exception("Unable to get Response Stream.");

      if (response.ContentEncoding.ToLower().Contains("gzip")) responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
      else if (response.ContentEncoding.ToLower().Contains("deflate")) responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

      return responseStream;
    }

  }

}
