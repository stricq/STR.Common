using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace STR.Common.Contracts {

  public class Enumeration<T> : IComparable where T : struct {

    #region Constructors

    protected Enumeration() { }

    protected Enumeration(T Value, string DisplayName) {
      this.Value       = Value;
      this.DisplayName = DisplayName;
    }

    #endregion Constructors

    #region Properties

    public T Value { get; }

    public string DisplayName { get; }

    #endregion Properties

    #region Overrides

    public override string ToString() {
      return DisplayName;
    }

    public override bool Equals(object obj) {
      Enumeration<T> otherValue = obj as Enumeration<T>;

      if (otherValue == null) return false;

      bool typeMatches = GetType() == obj.GetType();

      bool valueMatches = Value.Equals(otherValue.Value);

      return typeMatches && valueMatches;
    }

    public override int GetHashCode() {
      return Value.GetHashCode();
    }

    public static bool operator ==(Enumeration<T> a, Enumeration<T> b) {
      if (ReferenceEquals(a, b)) return true;

      if ((a is null) || (b is null)) return false;

      return a.CompareTo(b) == 0;
    }

    public static bool operator !=(Enumeration<T> a, Enumeration<T> b) {
      if (ReferenceEquals(a, b)) return false;

      if ((a is null) || (b is null)) return true;

      return a.CompareTo(b) != 0;
    }

    #endregion Overrides

    #region Public Methods

    public static IEnumerable<TOut> GetAll<TOut>() where TOut : Enumeration<T> {
      Type type = typeof(TOut);

      FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

      return fields.Select(f => f.GetValue(null)).Cast<TOut>();
    }

    public static TOut FromValue<TOut>(T value) where TOut : Enumeration<T> {
      TOut matchingItem = parse<TOut, T>(value, "Value", item => item.Value.Equals(value));

      return matchingItem;
    }

    public static TOut FromDisplayName<TOut>(string displayName) where TOut : Enumeration<T> {
      TOut matchingItem = parse<TOut, string>(displayName, "DisplayName", item => item.DisplayName == displayName);

      return matchingItem;
    }

    #endregion Public Methods

    #region Private Methods

    private static TOut parse<TOut, TIn>(TIn value, string description, Func<TOut, bool> predicate) where TOut : Enumeration<T> {
      TOut matchingItem = GetAll<TOut>().FirstOrDefault(predicate);

      if (matchingItem != null) return matchingItem;

      string message = $"'{value}' is not a valid {description} in {typeof(TOut)}";

      throw new Exception(message);
    }

    #endregion Private Methods

    #region IComparable Implementation

    public int CompareTo(object other) {
      return Comparer<T>.Default.Compare(Value, ((Enumeration<T>)other).Value);
    }

    #endregion IComparable Implementation

  }

  public class Enumeration : IComparable {

    #region Constructors

    protected Enumeration() { }

    protected Enumeration(int Value, string DisplayName) {
      this.Value       = Value;
      this.DisplayName = DisplayName;
    }

    #endregion Constructors

    #region Properties

    public int Value { get; }

    public string DisplayName { get; }

    #endregion Properties

    #region Overrides

    public override string ToString() {
      return DisplayName;
    }

    public override bool Equals(object obj) {
      Enumeration<int> otherValue = obj as Enumeration<int>;

      if (otherValue == null) return false;

      bool typeMatches = GetType() == obj.GetType();

      bool valueMatches = Value.Equals(otherValue.Value);

      return typeMatches && valueMatches;
    }

    public override int GetHashCode() {
      return Value.GetHashCode();
    }

    public static bool operator ==(Enumeration a, Enumeration b) {
      if (ReferenceEquals(a, b)) return true;

      if ((a is null) || (b is null)) return false;

      return a.CompareTo(b) == 0;
    }

    public static bool operator !=(Enumeration a, Enumeration b) {
      if (ReferenceEquals(a, b)) return false;

      if ((a is null) || (b is null)) return true;

      return a.CompareTo(b) != 0;
    }

    #endregion Overrides

    #region Public Methods

    public static IEnumerable<TOut> GetAll<TOut>() where TOut : Enumeration {
      Type type = typeof(TOut);

      FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

      return fields.Select(f => f.GetValue(null)).Cast<TOut>();
    }

    public static TOut FromValue<TOut>(int value) where TOut : Enumeration {
      TOut matchingItem = parse<TOut, int>(value, "Value", item => item.Value.Equals(value));

      return matchingItem;
    }

    public static TOut FromDisplayName<TOut>(string displayName) where TOut : Enumeration {
      TOut matchingItem = parse<TOut, string>(displayName, "DisplayName", item => item.DisplayName == displayName);

      return matchingItem;
    }

    #endregion Public Methods

    #region Private Methods

    private static TOut parse<TOut, TIn>(TIn value, string description, Func<TOut, bool> predicate) where TOut : Enumeration {
      TOut matchingItem = GetAll<TOut>().FirstOrDefault(predicate);

      if (matchingItem != null) return matchingItem;

      string message = $"'{value}' is not a valid {description} in {typeof(TOut)}";

      throw new Exception(message);
    }

    #endregion Private Methods

    #region IComparable Implementation

    public int CompareTo(object other) {
      return Comparer<int>.Default.Compare(Value, ((Enumeration)other).Value);
    }

    #endregion IComparable Implementation

  }

}
