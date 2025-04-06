using GameNightScoresRight.CommonDTOs;

namespace GameNightScoresRight.Accessors
{
    public interface IUserAccessor
    {
        Task<CreateUserResponse> CreateUser(CreateUserRequest account);
    }
}
