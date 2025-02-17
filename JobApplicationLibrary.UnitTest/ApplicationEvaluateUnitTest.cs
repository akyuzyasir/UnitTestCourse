using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;
using Moq;

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
        var evaluator = new ApplicationEvaluator(null);
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

        // Moq library lets me create the scenario i want to test.
        var mockValidator = new Mock<IIdentityValidator>();
        // If IsValid method under IIdentityValidator comes with any string value, then make it return "true"
        mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

        var evaluator = new ApplicationEvaluator(mockValidator.Object);
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19},
            TechStackList = new List<string>() { "" }
        };

        // Act
        var appResult = evaluator.Evaluate(form);

        // Assert
        Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoRejected));

    }
    [Test]
    public void Application_WithTechStackOver75P_TransferredToAutoAccepted()
    {
        // Arrange
        var mockValidator = new Mock<IIdentityValidator>();
        mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);


        var evaluator = new ApplicationEvaluator(mockValidator.Object);
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
        var appResult = evaluator.Evaluate(form);

        // Assert
        Assert.That(appResult, Is.EqualTo(ApplicationResult.AutoAccepted));

    }
    [Test]
    public void Application_WithInValidIdentityNumber_TransferredToHR()
    {
        // Arrange
        var mockValidator = new Mock<IIdentityValidator>();
        mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);


        var evaluator = new ApplicationEvaluator(mockValidator.Object);
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19 }
        };

        // Act
        var appResult = evaluator.Evaluate(form);

        // Assert
        Assert.That(appResult, Is.EqualTo(ApplicationResult.TransferredToHR));

    }
}
