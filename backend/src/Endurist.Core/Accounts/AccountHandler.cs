using Endurist.Common.Models;
using Endurist.Core.Accounts.Mappers;
using Endurist.Core.Accounts.Models;
using Endurist.Core.Accounts.Requests;
using Endurist.Core.Services;
using Endurist.Data;
using Endurist.Data.Mongo.Documents;
using MediatR;
using MongoDB.Bson;

namespace Endurist.Core.Accounts;

public class AccountHandler: HandlerBase,
    IRequestHandler<CreateTokenRequest, DataResponse<TokenModel>>,
    IRequestHandler<RefreshTokenRequest, DataResponse<TokenModel>>
{
    private IEncryptionService EncryptionService { get; }
    private TokenProvider TokenProvider { get; }

    public AccountHandler(
        Storage storage,
        IEncryptionService encryptionService,
        TokenProvider tokenProvider)
        : base(storage)
    {
        EncryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
        TokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
    }

    public async Task<DataResponse<TokenModel>> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        var response = new DataResponse<TokenModel>();

        var account = await Storage.Accounts.GetByNameAsync(request.Name, cancellationToken);
        if (account is null)
        {
            return response;
        }

        var passwordH = await EncryptionService.ComputePasswordHashAsync("Password", account.Salt, cancellationToken);

        var verificationResult = await EncryptionService.VerifyPasswordAsync(request.Password, account.Salt, account.PasswordHash, cancellationToken);
        if (!verificationResult)
        {
            return response;
        }

        var accountModel = new AccountModel();
        AccountMapper.Map(account, accountModel);

        response.Data = new TokenModel
        {
            AccessToken = await TokenProvider.CreateAccessTokenAsync(accountModel, cancellationToken),
            RefreshToken = await TokenProvider.CreateRefreshTokenAsync(cancellationToken)
        };

        var tokenDocument = new TokenDocument { Token = response.Data.RefreshToken, AccountId = ObjectId.Parse(accountModel.Id) };
        await Storage.Tokens.InsertAsync(tokenDocument, cancellationToken);

        return response;
    }

    public async Task<DataResponse<TokenModel>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var response = new DataResponse<TokenModel>();

        var token = await Storage.Tokens.GetByTokenAsync(request.Token, cancellationToken);
        if (token is null)
        {
            return response;
        }

        var account = await Storage.Accounts.GetByIdAsync(token.AccountId.ToString(), cancellationToken);
        if (account is null)
        {
            return response;
        }

        var accountModel = new AccountModel();
        AccountMapper.Map(account, accountModel);

        response.Data = new TokenModel
        {
            AccessToken = await TokenProvider.CreateAccessTokenAsync(accountModel, cancellationToken),
            RefreshToken = await TokenProvider.CreateRefreshTokenAsync(cancellationToken)
        };

        await Storage.Tokens.DeleteAsync(token, cancellationToken);

        var tokenDocument = new TokenDocument { Token = response.Data.RefreshToken, AccountId = ObjectId.Parse(accountModel.Id) };
        await Storage.Tokens.InsertAsync(tokenDocument, cancellationToken);

        return response;
    }
}