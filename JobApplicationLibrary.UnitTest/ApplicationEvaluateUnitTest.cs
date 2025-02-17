using JobApplicationLibrary.Models;

namespace JobApplicationLibrary.UnitTest;

public class ApplicationEvaluateUnitTest
{
    // There are naming rules for UnitTest
    // UnitOfWork_Condition_ExpectedResult
    // Condition_Result
    // FunctionUnderTest_ExpectedResult_UnderCondition
    [Test]
    public void Application_WithUnderAge_TransferredToAutoRejected()
    {
        // Arrange
        // Setting up the test objects
        var evaluator = new ApplicationEvaluator();
        var form = new JobApplication()
        {
            Applicant = new Applicant()
            {
                Age = 17
            }
        };

        // Act
        // Invoking the method under test
        var appResult = evaluator.Evaluate(form);

        // Assert
        // Verifying that the outcome matches expectations.
        Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));

    }

    [Test]
    public void Application_WithNoTechStack_TransferredToAutoRejected()
    {
        // Arrange
        // Setting up the test objects
        var evaluator = new ApplicationEvaluator();
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19},
            TechStackList = new List<string>() { "" }
        };

        // Act
        // Invoking the method under test
        var appResult = evaluator.Evaluate(form);

        // Assert
        // Verifying that the outcome matches expectations.
        Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));

    }
    [Test]
    public void Application_WithTechStackOver75P_TransferredToAutoAccepted()
    {
        // Arrange
        // Setting up the test objects
        var evaluator = new ApplicationEvaluator();
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19},
            TechStackList = new List<string>() 
            { 
                "C#", "RabbitMQ", "Microservice", "Visual Studio" 
            },
            YearsOfExperience = 16
        };

        // Act
        // Invoking the method under test
        var appResult = evaluator.Evaluate(form);

        // Assert
        // Verifying that the outcome matches expectations.
        Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoAccepted));

    }
}
