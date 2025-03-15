using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Messages;


[SuppressMessage("ReSharper", "UnusedMember.Global",               Justification = "This is a library.")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global",         Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "This is a library.")]
public class FileDownloadProgressMessage : MessageBase {

    public long BytesCurrent { get; set; }

    public long BytesTotal { get; set; }

    public bool IsComplete { get; set; }

    public double PercentComplete => BytesTotal == 0 ? 0 : (double)BytesCurrent / BytesTotal;

}


[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",       Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedMember.Global",                 Justification = "This is a library.")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global",           Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global",   Justification = "This is a library.")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global", Justification = "This is a library.")]
public class FileDownloadProgressMessage<T> : FileDownloadProgressMessage where T : class {

    public T? State { get; set; }

}
