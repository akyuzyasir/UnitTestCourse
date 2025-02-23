﻿using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;

namespace JobApplicationLibrary;

public class ApplicationEvaluator
{
    private const int minAge = 18;
    private const int autoAcceptedYearOfExperience = 15;
    private List<string> techStackList = new() { "C#", "RabbitMQ", "Microservice", "Visual Studio" };
    private readonly IIdentityValidator _identityValidator;

    public ApplicationEvaluator(IIdentityValidator identityValidator)
    {
        _identityValidator = identityValidator;
    }

    public ApplicationResult Evaluate(JobApplication form)
    {
        if(form.Applicant.Age < minAge)
            return ApplicationResult.AutoRejected;

        if (form.OfficeLocation != "ISTANBUL")
            return ApplicationResult.TransferredToCTO;
        
        var connectionSucceed = _identityValidator.CheckConnectionToRemoteServer();
        var validIdentity = _identityValidator.IsValid(form.Applicant.IdentityNumber);

        if (!validIdentity)
            return ApplicationResult.TransferredToHR;

        var sr = GetTechStackSimilarityRate(form.TechStackList);

        if(sr < 25)
            return ApplicationResult.AutoRejected;

        if (sr > 75 && form.YearsOfExperience > autoAcceptedYearOfExperience)
            return ApplicationResult.AutoAccepted;

        return ApplicationResult.AutoAccepted;
    }

    private int GetTechStackSimilarityRate(List<string> techStacks)
    {
        var matchedCount = 
            techStacks
                .Where(i => techStackList.Contains(i, StringComparer.OrdinalIgnoreCase))
                .Count();

        return (int)((double)matchedCount / techStackList.Count) * 100;
    }
}

public enum ApplicationResult
{
    AutoRejected,
    TransferredToHR,
    TransferredToLead,
    TransferredToCTO,
    AutoAccepted
}
