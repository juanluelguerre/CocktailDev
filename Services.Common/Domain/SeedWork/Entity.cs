namespace CocktailDev.Services.Common.Domain.SeedWork;

public abstract class Entity
{
    protected Entity()
    {
    }

    protected Entity(Guid id)
    {
        this.Id = id;
    }

    public Guid Id { get; }
}
