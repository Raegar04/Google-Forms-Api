using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Login request
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Gets or sets email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets password
    /// </summary>
    public string Password { get; set; }
}

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
