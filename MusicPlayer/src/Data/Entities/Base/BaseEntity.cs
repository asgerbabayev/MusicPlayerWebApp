namespace Data.Entities.Base;

public class BaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}

public class BaseEntity : BaseEntity<int>
{
}