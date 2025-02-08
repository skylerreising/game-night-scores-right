using GameNightScoresRight.ControllerDTOs;

namespace GameNightScoresRight.Managers
{
    public interface IAccountManager
    {
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
    }
}
