namespace Data.Entities.Base;

public class BaseAuditableEntity : BaseEntity
{
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
