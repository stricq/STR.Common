using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Messages;


[SuppressMessage("ReSharper", "UnusedType.Global",   Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "This is a library.")]
public class ApplicationClosingMessage : MessageBase {

    public bool Cancel { get; set; }

}
