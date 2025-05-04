using BankApp;
namespace BankApp.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BankAccount account = new BankAccount("John Doe");
            account.Deposit(100.00m);
            Assert.AreEqual(100.00m, account.AccountBalance);
        }
    }
}