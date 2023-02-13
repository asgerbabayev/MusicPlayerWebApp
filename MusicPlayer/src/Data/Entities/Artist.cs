using Data.Entities.Base;

namespace Data.Entities;

public class Artist : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Song> Songs { get; set; }
    public Artist() => Songs = new HashSet<Song>();
}
