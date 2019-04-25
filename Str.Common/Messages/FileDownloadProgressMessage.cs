using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Messages {

  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  public class FileDownloadProgressMessage : MessageBase {

    public long BytesCurrent { get; set; }

    public long BytesTotal { get; set; }

    public bool IsComplete { get; set; }

    public double PercentComplete => BytesTotal == 0 ? 0 : (double)BytesCurrent / BytesTotal;

  }

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public class FileDownloadProgressMessage<T> : FileDownloadProgressMessage {

    public T State { get; set; }

  }

}
