using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankApp;
using System.Linq;

namespace BankApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test the Deposit methods
        /// </summary>
        [TestMethod]
        public void TestDeposit_ValidAmount()
        {
            // Arrange
            IBankAccount account = new BankAccount();
            decimal initialBalance = 100m;
            decimal depositAmount = 50m;
            decimal expectedBalance = 150m;

            account.Deposit(initialBalance);

            //Act
            account.Deposit(depositAmount);

            //Assert
            Assert.AreEqual(expectedBalance, account.AccountBalance, "Deposit should have been deposited succesfully");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeposit_NegativeAmount()
        {
            //Arrange
            IBankAccount account = new BankAccount();
            decimal depositAmount = -50m;

            //Act
            account.Deposit(depositAmount);

            //Assert
        }

        [TestMethod]
        public void TestDeposit_ZeroAmount()
        {
            //Arrange
            IBankAccount account = new BankAccount();
            decimal initialBalance = 100m;
            decimal depositAmount = 0m;
            decimal expectedBalance = 100m;

            account.Deposit(initialBalance);

            //Act
            account.Deposit(depositAmount);

            //Assert
            Assert.AreEqual(expectedBalance, account.AccountBalance, "Balance should remain the same");
        }

        /// <summary>
        /// Test Withdraw Methods
        /// </summary>
        [TestMethod]
        public void TestWithdraw_ValidAmount()
        {
            //Arrange
            IBankAccount account = new BankAccount();
            decimal initialBalance = 100m;
            decimal withdrawAmount = 50m;
            decimal expectedBalance = 50m;

            account.Deposit(initialBalance);

            //Act
            account.Withdraw(withdrawAmount);

            //Assert
            Assert.AreEqual(expectedBalance, account.AccountBalance, "Balance should have been withdrawn succesfully");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWithdraw_NegativeAmount()
        {
            //Arrange
            IBankAccount account = new BankAccount();
            decimal withdrawAmount = -50m;

            //Act
            account.Withdraw(withdrawAmount);

            //Assert is handled by ExpectedException
        }

        [TestMethod]
        public void TestWithdraw_ZeroAmount()
        {
            //Arrange
            IBankAccount account = new BankAccount();
            decimal initialBalance = 100m;
            decimal withdrawAmount = 0m;
            decimal expectedBalance = 100m;

            account.Deposit(initialBalance);

            //Act
            account.Withdraw(withdrawAmount);

            //Assert
            Assert.AreEqual(expectedBalance, account.AccountBalance, "Balance should not change as withdraw is zero");
        }

        /// <summary>
        /// Test Transaction creates fine and returns the correct ToString for that type of Transaction
        /// </summary>
        [TestMethod]
        public void TestTransaction_Creation()
        {
            //Arrange
            decimal amount = 100m;
            string type = "Deposit";
            string description = "Test Description";
            string otherParty = "Test Party";
            decimal fee = 0m;
            DateTime date = DateTime.Now;

            //Act
            var transaction = new Transaction(amount, description , otherParty, date, fee, type);

            //Assert
            Assert.AreEqual(amount, transaction.TransactionAmount, "Transaction amount is incorrect.");
            Assert.AreEqual(type, transaction.TransactionType, "Transaction type is incorrect.");
            Assert.AreEqual(date, transaction.TransactionDate, "Transaction date is incorrect.");
            Assert.AreEqual($"{date.ToShortDateString(),-12}+ {amount.ToString("C2"),-18}  {otherParty,-15}{description,-40}", transaction.ToString(), "Transaction to string is incorrect.");
        }

        /// <summary>
        /// Test making and adding a card to a bank account
        /// </summary>
        [TestMethod]
        public void TestAddCard_Debit()
        {
            // Arrange
            var account = new BankAccount();
            string cardType = "Debit";

            // Act
            account.AddCard(cardType);

            // Assert
            Assert.AreEqual(1, account.CardCount, "Card count didnt update");
            Assert.AreEqual(cardType, account.Cards[0].CardType, "Wrong Card Type");
        }

        [TestMethod]
        public void TestAddCard_Credit()
        {
            // Arrange
            var account = new BankAccount();
            string cardType = "Credit";

            // Act
            account.AddCard(cardType);

            // Assert
            Assert.AreEqual(1, account.CardCount, "Card count didnt update");
            Assert.AreEqual(cardType, account.Cards[0].CardType, "Wrong Card Type");
            Assert.IsTrue(account.Cards[0].CreditLimit > 0, "Credit Limit not more than 0");
        }

        /// <summary>
        /// Testing Monthly Transaction Stats
        /// </summary>
        [TestMethod]
        public void TestSumTransactionAmounts_Deposits()
        {
            // Arrange
            var account = new BankAccount();
            account.Transactions.Add(new Transaction(100m, "Deposit", "Test Party", DateTime.Now, 0m, "Deposit"));
            account.Transactions.Add(new Transaction(50m, "Deposit", "Test Party", DateTime.Now, 0m, "Deposit"));
            account.Transactions.Add(new Transaction(200m, "Deposit", "Test Party", DateTime.Now, 0m, "Deposit"));

            // Act
            var totalDeposits = account.Transactions
                .Where(t => t.TransactionType == "Deposit")
                .Sum(t => t.TransactionAmount);

            // Assert
            Assert.AreEqual(350m, totalDeposits, "Incorrect Total Deposit Amounts");
        }

        [TestMethod]
        public void TestSumTransactionAmounts_Withdraws()
        {
            // Arrange
            var account = new BankAccount();
            account.Transactions.Add(new Transaction(100m, "Withdraw", "Test Party", DateTime.Now, 0m, "Withdraw"));
            account.Transactions.Add(new Transaction(50m, "Withdraw", "Test Party", DateTime.Now, 0m, "Withdraw"));
            account.Transactions.Add(new Transaction(200m, "Withdraw", "Test Party", DateTime.Now, 0m, "Withdraw"));

            // Act
            var totalWithdrawals = account.Transactions
                .Where(t => t.TransactionType == "Withdraw")
                .Sum(t => t.TransactionAmount);

            // Assert
            Assert.AreEqual(350m, totalWithdrawals, "Incorrect Total Withdrawal Amounts");
        }

        [TestMethod]
        public void TestAverageTransactionAmounts_Deposits()
        {
            // Arrange
            var account = new BankAccount();
            account.Transactions.Add(new Transaction(100m, "Deposit", "Test Party", DateTime.Now, 0m, "Deposit"));
            account.Transactions.Add(new Transaction(300m, "Deposit", "Test Party", DateTime.Now, 0m, "CryptoS"));
            account.Transactions.Add(new Transaction(200m, "Deposit", "Test Party", DateTime.Now, 0m, "Deposit"));

            // Act
            var Deposits = account.Transactions.Where(t => t.TransactionType == "Deposit" || t.TransactionType == "CryptoS");
            decimal averageDeposit = Deposits.Any() ? Deposits.Average(t => t.TransactionAmount) : 0;

            // Assert
            Assert.AreEqual(200m, averageDeposit, "Incorrect Average Deposit Amounts");
        }

        [TestMethod]
        public void TestAverageTransactionAmounts_Withdraws()
        {
            // Arrange
            var account = new BankAccount();
            account.Transactions.Add(new Transaction(100m, "Withdraw", "Test Party", DateTime.Now, 0m, "Withdraw"));
            account.Transactions.Add(new Transaction(300m, "Withdraw", "Test Party", DateTime.Now, 0m, "CryptoB"));
            account.Transactions.Add(new Transaction(200m, "Withdraw", "Test Party", DateTime.Now, 0m, "Withdraw"));

            // Act
            var Withdraws = account.Transactions.Where(t => t.TransactionType == "Withdraw" || t.TransactionType == "CryptoB");
            decimal averageWithdraw = Withdraws.Any() ? Withdraws.Average(t => t.TransactionAmount) : 0;

            // Assert
            Assert.AreEqual(200m, averageWithdraw, "Incorrect Average Withdraw Amounts");
        }
    }
}
 