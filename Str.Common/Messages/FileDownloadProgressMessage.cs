

namespace Str.Common.Messages {

  public class FileDownloadProgressMessage : MessageBase {

    public long BytesCurrent { get; set; }

    public long BytesTotal { get; set; }

    public bool IsComplete { get; set; }

  }

  public class FileDownloadProgressMessage<T> : FileDownloadProgressMessage {

    public T State { get; set; }

  }

}
