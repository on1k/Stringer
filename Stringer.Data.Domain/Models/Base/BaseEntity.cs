namespace Stringer.Data.Domain.Models.Base;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? OwnerId { get; set; }
}