using Endurist.Core.Accounts.Models;
using Endurist.Data.Mongo.Documents;

namespace Endurist.Core.Accounts.Mappers;

internal static class AccountMapper
{
    public static void Map(AccountDocument document, AccountModel model)
    {
        model.Id = document.EntityId;
        model.Name = document.Name;
    }
}