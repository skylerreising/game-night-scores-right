using AutoMapper;
using GameNightScoresRight.Accessors;
using GameNightScoresRight.ControllerDTOs;
using CD = GameNightScoresRight.CommonDTOs;

namespace GameNightScoresRight.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountAccessor _accountAccessor;
        private readonly IMapper _mapper;

        public AccountManager(IAccountAccessor accountAccessor, IMapper mapper)
        {
            _accountAccessor = accountAccessor;
            _mapper = mapper;
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var createAccountRequest = _mapper.Map<CD.CreateAccountRequest>(request);

            var response = await _accountAccessor.CreateAccount(createAccountRequest);

            return _mapper.Map<CreateAccountResponse>(response);
        }
    }
}
