namespace ECommerce.Domain.Core;

public abstract class Enumeration : IComparable
{
    public int Id { get; }
    public string Name { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration other)
            return false;

        return Id == other.Id &&
               GetType() == other.GetType();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

    public int CompareTo(object? other)
    {
        return Id.CompareTo(((Enumeration)other!).Id);
    }
}