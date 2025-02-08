using GameNightScoresRight.Accessors;
using GameNightScoresRight.ControllerDTOs;
using CD = GameNightScoresRight.CommonDTOs;

namespace GameNightScoresRight.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountAccessor _accountAccessor;

        public AccountManager(IAccountAccessor accountAccessor)
        {
            _accountAccessor = accountAccessor;
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var createAccountRequest = new CD.CreateAccountRequest
            {
                EmailAddress = request.EmailAddress,
                Role = request.Role
            };

            var account = await _accountAccessor.CreateAccount(createAccountRequest);

            return new CreateAccountResponse();
        }
    }
}
