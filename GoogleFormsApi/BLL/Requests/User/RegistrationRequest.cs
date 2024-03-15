using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Registration request
/// </summary>
public class RegistrationRequest
{
    /// <summary>
    /// Gets or sets email of the user
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets password of the user
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets password confirmation
    /// </summary>
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Gets or sets username of the user
    /// </summary>
    public string Username { get; set; }
}

public class RegistrationValidator : AbstractValidator<RegistrationRequest> 
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}