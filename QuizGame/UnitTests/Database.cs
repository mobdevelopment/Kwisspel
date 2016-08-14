using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizGamePack.Model;
using System.Data.Entity;

namespace UnitTests
{
    [TestClass]
    public class Database
    {
        Context database;

        [TestInitialize]
        public void TestInitialize()
        {
            database = new Context();
        }

        [TestMethod]
        public void isConnected()
        {
            Assert.IsTrue(database.Database.Exists(), "Hello");
        }
    }
}
