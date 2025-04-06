using AutoMapper;
using GameNightScoresRight.Accessors;
using GameNightScoresRight.ControllerDTOs;
using CD = GameNightScoresRight.CommonDTOs;

namespace GameNightScoresRight.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public UserManager(IUserAccessor userAccessor, IMapper mapper)
        {
            _userAccessor = userAccessor;
            _mapper = mapper;
        }
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            var createUserRequest = _mapper.Map<CD.CreateUserRequest>(request);

            var response = await _userAccessor.CreateUser(createUserRequest);

            return _mapper.Map<CreateUserResponse>(response);
        }
    }
}
