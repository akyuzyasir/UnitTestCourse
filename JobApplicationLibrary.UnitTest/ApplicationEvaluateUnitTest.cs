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
        mockValidator.Setup(i => i.Country).Returns("TURKEY");


        var evaluator = new ApplicationEvaluator(mockValidator.Object);
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19},
            TechStackList = new List<string>() { "" },
            OfficeLocation = "ISTANBUL"

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
        mockValidator.Setup(i => i.Country).Returns("TURKEY");



        var evaluator = new ApplicationEvaluator(mockValidator.Object);
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19},
            TechStackList = new List<string>() 
            { 
                "C#", "RabbitMQ", "Microservice", "Visual Studio" 
            },
            YearsOfExperience = 16,
            OfficeLocation = "ISTANBUL"

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
        var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Strict);
        mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
        mockValidator.Setup(i => i.CheckConnectionToRemoteServer()).Returns(false);
        mockValidator.Setup(i => i.Country).Returns("TURKEY");


        var evaluator = new ApplicationEvaluator(mockValidator.Object);
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19 },
            OfficeLocation = "ISTANBUL"
        };

        // Act
        var appResult = evaluator.Evaluate(form);

        // Assert
        Assert.That(appResult, Is.EqualTo(ApplicationResult.TransferredToHR));

    }
    [Test]
    public void Application_WithOfficeLocation_TransferredToCTO()
    {
        // Arrange
        var mockValidator = new Mock<IIdentityValidator>();


        var evaluator = new ApplicationEvaluator(mockValidator.Object);
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19 },
            OfficeLocation = "ANKARA"
        };

        // Act
        var appResult = evaluator.Evaluate(form);

        // Assert
        Assert.That(appResult, Is.EqualTo(ApplicationResult.TransferredToCTO));

    }
    [Test]
    public void Application_WithoutCountry_TransferredToCTO()
    {
        // Arrange
        var mockValidator = new Mock<IIdentityValidator>();
        mockValidator.Setup(i => i.Country).Returns("SPAIN");


        var evaluator = new ApplicationEvaluator(mockValidator.Object);
        var form = new JobApplication()
        {
            Applicant = new Applicant() { Age = 19 },
            OfficeLocation = "ANKARA"
        };

        // Act
        var appResult = evaluator.Evaluate(form);

        // Assert
        Assert.That(appResult, Is.EqualTo(ApplicationResult.TransferredToCTO));

    }
}
