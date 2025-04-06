using GameNightScoresRight.ControllerDTOs;

namespace GameNightScoresRight.Managers
{
    public interface IUserManager
    {
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
    }
}
