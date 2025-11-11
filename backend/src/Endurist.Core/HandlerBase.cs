using Endurist.Data;

namespace Endurist.Core;

public abstract class HandlerBase
{
    protected Storage Storage { get; }

    protected HandlerBase(Storage storage)
    {
        Storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }
}
