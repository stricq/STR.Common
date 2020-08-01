using System;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Str.Common.Extensions;


namespace Str.Common.Tests {

  [TestClass]
  public class TaskExtensionTests {

    #region FireAndForget Tests

    [TestMethod, TestCategory("Unit")]
    public void FireAndForgetTaskNoActionSuccess() {
      MethodAsync().FireAndForget();
    }

    [TestMethod, TestCategory("Unit")]
    public void FireAndForgetTaskNoActionException() {
      MethodAsync(true).FireAndForget(); // Exception is dropped and never propogates out
    }

    [TestMethod, TestCategory("Unit")]
    public async Task FireAndForgetTaskActionSuccess() {
      int callbackCount = 0;

      void callback(Exception ex) { ++callbackCount; }

      MethodAsync().FireAndForget(callback);

      await Task.Delay(1000);

      Assert.AreEqual(0, callbackCount);
    }

    [TestMethod, TestCategory("Unit")]
    public async Task FireAndForgetTaskActionException() {
      int callbackCount = 0;

      void callback(Exception ex) { ++callbackCount; }

      MethodAsync(true).FireAndForget(callback);

      await Task.Delay(1000);

      Assert.AreEqual(1, callbackCount);
    }

    #endregion FireAndForget Tests

    #region Private Methods

    private static async Task MethodAsync(bool throwException = false) {
      await Task.Delay(500).ConfigureAwait(false);

      if (throwException) throw new Exception();

      await Task.CompletedTask.ConfigureAwait(false);
    }

    #endregion Private Methods

  }

}
