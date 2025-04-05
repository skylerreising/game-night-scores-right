using GameNightScoresRight.CommonDTOs;

namespace GameNightScoresRight.Accessors
{
    public interface IUserAccessor
    {
        Task<CreateUserResponse> CreateAccount(CreateUserRequest account);
    }
}
