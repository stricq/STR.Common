using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

using Str.Common.Messages;


namespace Str.Common.Extensions {
  //
  // From an answer on Stack Overflow
  //
  // https://stackoverflow.com/questions/1540658/net-asynchronous-stream-read-write/4139427#4139427
  //
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public static class StreamExtensions {

    private const int DefaultBufferSize = 32768;

    public static async Task CopyToAsync<T>(this Stream input, Stream output, int bufferSize = DefaultBufferSize, FileDownloadProgressMessage<T> message = null, Action<FileDownloadProgressMessage<T>> callback = null) {
      await input.CopyToAsync(output, bufferSize, message as FileDownloadProgressMessage, callback as Action<FileDownloadProgressMessage>);
    }

    public static async Task CopyToAsync(this Stream input, Stream output, int bufferSize = DefaultBufferSize, FileDownloadProgressMessage message = null, Action<FileDownloadProgressMessage> callback = null) {
      if (!input.CanRead)   throw new InvalidOperationException("Input stream must be open for reading.");
      if (!output.CanWrite) throw new InvalidOperationException("Output stream must be open for writing.");

      if (bufferSize < 1) throw new ArgumentException("Argument may not be 0 or negative.", nameof(bufferSize));

      byte[][] buf = { new byte[bufferSize], new byte[bufferSize] };

      int[] bufl = { 0, 0 };

      int bufno = 0;
      int total = 0;

      Task<int> read = input.ReadAsync(buf[bufno], 0, buf[bufno].Length);

      Task write = null;

      while(true) {
        //
        // wait for the read operation to complete
        //
        bufl[bufno] = await read;
        //
        // if zero bytes read, the copy is complete
        //
        if (bufl[bufno] == 0) break;

        total += bufl[bufno];

        if (message != null) message.BytesCurrent = total;

        callback?.Invoke(message);
        //
        // wait for the in-flight write operation, if one exists, to complete
        // the only time one won't exist is after the very first read operation completes
        //
        if (write != null) await write;
        //
        // start the new write operation
        //
        write = output.WriteAsync(buf[bufno], 0, bufl[bufno]);
        //
        // toggle the current, in-use buffer
        // and start the read operation on the new buffer.
        //
        // Changed to use XOR to toggle between 0 and 1.
        // A little speedier than using a ternary expression.
        //
        bufno ^= 1; // bufno = ( bufno == 0 ? 1 : 0 ) ;

        read = input.ReadAsync(buf[bufno], 0, buf[bufno].Length);
      }
      //
      // wait for the final in-flight write operation, if one exists, to complete
      // the only time one won't exist is if the input stream is empty.
      //
      if (write != null) await write;

      if (message != null) message.IsComplete = true;

      callback?.Invoke(message);
    }

  }

}
