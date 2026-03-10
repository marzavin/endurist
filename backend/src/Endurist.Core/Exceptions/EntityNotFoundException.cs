namespace Endurist.Contracts.Exceptions;

public class EntityNotFoundException : ApplicationException
{
    public EntityNotFoundException(Type type, string id)
        : base($"Entity of type {type.Name} is not found by its id ('${id}').")
    { }
}
