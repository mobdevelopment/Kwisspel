using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizSpel.ViewModel;

namespace UnitTest_QuizSpel
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void AddQuestionTest()
        {
            // Arrange
            QuizManagementViewModel qmvm = new QuizManagementViewModel();
            var i = qmvm.Questions.Count;

            // Act
            qmvm.SelectedQuestion.Id = 1;
            qmvm.SelectedQuestion.Text = "this is a test question";

            qmvm.AddQuestion();

            var j = qmvm.Questions.Count;

            // Assert
            Assert.AreEqual((i + 1), j);
        }

        [TestMethod]
        public void DeleteQuestionTest()
        {
            // Arrange
            QuizManagementViewModel qmvm = new QuizManagementViewModel();
            var i = qmvm.Questions.Count;

            // Act
            qmvm.DeleteQuestion();
            var j = qmvm.Questions.Count;

            // Assert
            Assert.AreEqual((i - 1), j);
        }
    }
}
