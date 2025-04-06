using Microsoft.EntityFrameworkCore;
using GameNightScoresRight.Accessors;
using GameNightScoresRight.Data;
using CD = GameNightScoresRight.CommonDTOs;
using Enums = GameNightScoresRight.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using AutoMapper;
using GameNightScoresRight.MappingProfiles;


namespace GameNightTests
{
    [TestClass]
    public sealed class AccessorTests
    {
        private GameNightDbContext _dbContext;
        private AccountAccessor _accountAccessor;
        private UserAccessor _userAccessor;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<GameNightDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new GameNightDbContext(options);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CommonEFMappingProfile>());
            _mapper = config.CreateMapper();
            _accountAccessor = new AccountAccessor(_dbContext, _mapper);
            _userAccessor = new UserAccessor(_dbContext, _mapper);
        }

        [TestMethod]
        public async Task CreateAccount_ShouldAddAccountToDatabase()
        {
            // Arrange
            var newAccountDto = new CD.CreateAccountRequest
            {
                EmailAddress = "test@example.com",
                Role = Enums.Role.User
            };

            // Act
            await _accountAccessor.CreateAccount(newAccountDto);

            var accountFromDb = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.EmailAddress == "test@example.com");

            // Assert
            accountFromDb.Should().NotBeNull("The account should have been added to the database.");
            accountFromDb.EmailAddress.Should().Be(newAccountDto.EmailAddress, "The EmailAddress should match the input.");
            accountFromDb.Role.Should().Be(newAccountDto.Role, "The Role should match the input.");
            accountFromDb.Id.GetType().Should().Be(typeof(Guid));
            accountFromDb.Id.Should().NotBe(Guid.Empty, "The Id should not be empty after account creation.");
            accountFromDb.UserId.GetType().Should().Be(typeof(Guid), "The UserId should be of type Guid.");
            accountFromDb.UserId.Should().NotBe(Guid.Empty, "The UserId should not be empty after account creation.");
            accountFromDb.Role.Should().Be(newAccountDto.Role);
            accountFromDb.IsDeleted.Should().BeFalse();
        }

        [TestMethod]
        public async Task CreateUser_ShouldCreateUser()
        {
            // Arrange
            // First, create an account to associate with the user
            var newAccountDto = new CD.CreateAccountRequest
            {
                EmailAddress = "test@example.com",
                Role = Enums.Role.User
            };

            var createAccountResponse = await _accountAccessor.CreateAccount(newAccountDto);

            CD.CreateUserRequest request = new()
            {
                UserId = createAccountResponse.UserId,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                PhoneNumber = "1234567890",
                Age = 30,
                Pronouns = "he/him",
                ProfilePictureUrl = "http://example.com/profile.jpg",
                Bio = "Hello, I'm John!",
            };

            // Act
            var response = await _userAccessor.CreateUser(request);

            // Assert
            response.Should().NotBeNull("The response should not be null after creating the user.");

            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(a => a.EmailAddress == newAccountDto.EmailAddress);
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == createAccountResponse.UserId);

            account.Should().NotBeNull("The account should exist in the database.");
            user.Should().NotBeNull("The user should have been created and exist in the database.");
            user.FirstName.Should().Be(request.FirstName, "The FirstName should match the input.");
            user.LastName.Should().Be(request.LastName, "The LastName should match the input.");
            user.Username.Should().Be(request.Username, "The Username should match the input.");
            user.PhoneNumber.Should().Be(request.PhoneNumber, "The PhoneNumber should match the input.");
            user.Age.Should().Be(request.Age, "The Age should match the input.");
            user.Pronouns.Should().Be(request.Pronouns, "The Pronouns should match the input.");
            user.ProfilePictureUrl.Should().Be(request.ProfilePictureUrl, "The ProfilePictureUrl should match the input.");
            user.Bio.Should().Be(request.Bio, "The Bio should match the input.");
            user.AccountId.Should().Be(account.Id, "The User's AccountId should match the created account's Id.");
            user.IsDeleted.Should().BeFalse("The user should not be marked as deleted.");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

    }
}
