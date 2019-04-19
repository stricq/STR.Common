

namespace Str.Common.Messages {

  public class FileDownloadProgressMessage<T> : MessageBase {

    public long BytesCurrent { get; set; }

    public long BytesTotal { get; set; }

    public bool IsComplete { get; set; }

    public T State { get; set; }

  }

}
