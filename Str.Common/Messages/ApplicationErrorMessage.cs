using System;
using System.Diagnostics.CodeAnalysis;


namespace Str.Common.Messages {

  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public class ApplicationErrorMessage : MessageBase {

    public ApplicationErrorMessage() {
      OpenErrorWindow = true;
    }

    public bool OpenErrorWindow { get; set; }

    public string HeaderText { get; set; }

    public string ErrorMessage { get; set; }

    public Exception Exception { get; set; }

  }

}
