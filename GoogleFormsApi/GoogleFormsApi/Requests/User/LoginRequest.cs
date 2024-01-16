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
    [Required]
    [EmailAddress(ErrorMessage = "Email didn't pass complience check")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets password
    /// </summary>
    [Required]
    public string Password { get; set; }
}
