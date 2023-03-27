namespace Str.Common.Contracts; 

public interface ITraversable<out T> {

  IEnumerable<T> Children { get; }

}