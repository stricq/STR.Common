using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Messages;


[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global", Justification = "This is a library.")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global",           Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global",   Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedMember.Global",                 Justification = "This is a library.")]
[SuppressMessage("ReSharper", "UnusedType.Global",                   Justification = "This is a library.")]
public class ApplicationErrorMessage : MessageBase {

    public bool OpenErrorWindow { get; set; } = true;

    public string? HeaderText { get; set; }

    public string? ErrorMessage { get; set; }

    public Exception? Exception { get; set; }

}
