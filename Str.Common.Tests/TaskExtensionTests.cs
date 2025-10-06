using Str.Common.Extensions;


namespace Str.Common.Tests;


[TestClass]
public class TaskExtensionTests {

    #region FireAndForget Tests

    [TestMethod, TestCategory("Unit")]
    public void FireAndForgetTaskNoActionSuccess() {
        MethodAsync().FireAndForget();
    }

    [TestMethod, TestCategory("Unit")]
    public void FireAndForgetTaskNoActionException() {
        MethodAsync(true).FireAndForget(); // Exception is dropped on the floor
    }

    [TestMethod, TestCategory("Unit")]
    public async Task FireAndForgetTaskActionSuccessAsync() {
        int callbackCount = 0;

        MethodAsync().FireAndForget(Callback);

        await Task.Delay(1000).Fire();

        Assert.AreEqual(0, callbackCount);

        return;

        void Callback(Exception ex) { ++callbackCount; }
    }

    [TestMethod, TestCategory("Unit")]
    public async Task FireAndForgetTaskActionExceptionAsync() {
        int callbackCount = 0;

        MethodAsync(true).FireAndForget(Callback);

        await Task.Delay(1000).Fire();

        Assert.AreEqual(1, callbackCount);

        return;

        void Callback(Exception ex) { ++callbackCount; }
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
