namespace Domain.Primitives;

/// <summary>
/// Type of the Base entity
/// </summary>
/// <typeparam name="T">Generic private key</typeparam>
public abstract class BaseEntity<T> 
{
    public T Id { get; set; }

    protected BaseEntity(T id) => Id = id;

    protected BaseEntity() {}
}