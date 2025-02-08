using GameNightScoresRight.CommonDTOs;

namespace GameNightScoresRight.Accessors
{
    public interface IAccountAccessor
    {
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest account);
    }
}
