using AutoMapper;
using EF = GameNightScoresRight.EFDTOs;
using GameNightScoresRight.CommonDTOs;
using GameNightScoresRight.Data;

namespace GameNightScoresRight.Accessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly GameNightDbContext _db;
        private readonly IMapper _mapper;
        public UserAccessor(GameNightDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }
        public async Task<CreateUserResponse> CreateAccount(CreateUserRequest createUserRequest)
        {
            var newUser = _mapper.Map<EF.User>(createUserRequest);
            newUser.CreatedAt = DateTimeOffset.UtcNow;
            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            var response = _mapper.Map<CreateUserResponse>(newUser);
            return response;
        }
    }
}
