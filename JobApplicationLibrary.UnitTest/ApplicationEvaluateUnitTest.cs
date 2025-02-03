using JobApplicationLibrary.Models;
using NUnit.Framework;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {
        // There are naming rules for UnitTest
        // UnitOfWork_Condition_ExpectedResult
        // Condition_Result
        // FunctionUnderTest_ExpectedResult_UnderCondition
        [Test]
        public void Application_TransferredToAutoRejected_WithUnderAge()
        {
            // Arrange
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };
            // Action

            var appResult = evaluator.Evaluate(form);
            // Assert

            Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));

        }
    }
}
