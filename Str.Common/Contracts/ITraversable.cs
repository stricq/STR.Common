using System.Collections.ObjectModel;


namespace Str.Common.Contracts {

  public interface ITraversable<T> {

    ObservableCollection<T> Children { get; }

  }

}
