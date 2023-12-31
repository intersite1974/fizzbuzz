﻿using Ardalis.GuardClauses;
using FizzBuzzCodeKata.Core.Models;
using FizzBuzzCodeKata.Core.Services;

namespace FizzBuzzCodeKata.Core.Rules;

/// <summary>
/// (S)(O)LI(D) - (S)ingle Responsibility Principle - Buzz rule does one thing and one thing only and that is to handle the logic for the Buzz rule.
///               (O)pen/Closed Principle - We allow extensions to the class - here we have extended the code to allow for validation
///               (D)ependency Injection - we do not hard wire the validation logic directly into this class, we inject the ValidationService instead
/// </summary>
public class BuzzRule : IFizzBuzzRule
{
    private readonly IValidationService _validationService;

    public BuzzRule(IValidationService validationService)
    {
        _validationService = Guard.Against.Null(validationService);
    }

    public string Execute(FizzbuzzModel model)
    {
        var validationResult = _validationService.ValidateInputModel(model);

        if (!string.IsNullOrEmpty(validationResult))
        {
            return validationResult;
        }

        return model.InputNumber % 5 == 0
            ? "Buzz"
            : string.Empty;
    }

    public bool RuleShouldRun(int number)
    {
        return !(number % 3 == 0 && number % 5 == 0);
    }
}
