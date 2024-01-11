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
    [Required]
    [EmailAddress(ErrorMessage = "Email didn't pass complience check")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets password of the user
    /// </summary>
    [Required]
    [MinLength(6, ErrorMessage = "Password must contain at least 6 characters")]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets password confirmation
    /// </summary>
    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Gets or sets username of the user
    /// </summary>
    [Required]
    public string Username { get; set; }
}