using Stringer.Auth.Domain.Models.Base;

namespace Stringer.Auth.Domain.Models;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
}