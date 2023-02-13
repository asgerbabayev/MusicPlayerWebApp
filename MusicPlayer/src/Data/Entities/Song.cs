using Data.Entities.Base;

namespace Data.Entities;

public class Song : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
}
