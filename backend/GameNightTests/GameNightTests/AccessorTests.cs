﻿using Microsoft.EntityFrameworkCore;
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
        private IMapper _mapper;

        [TestInitialize]
        public void TestInit()
        {
            // Create a new in-memory database with a unique name for each test run.
            var options = new DbContextOptionsBuilder<GameNightDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new GameNightDbContext(options);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CommonEFMappingProfile>());
            _mapper = config.CreateMapper();
            _accountAccessor = new AccountAccessor(_dbContext, _mapper);
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

            await _accountAccessor.CreateAccount(newAccountDto);

            // Act
            var accountFromDb = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.EmailAddress == "test@example.com");

            // Assert
            accountFromDb.Should().NotBeNull("The account should have been added to the database.");
            accountFromDb.EmailAddress.Should().Be(newAccountDto.EmailAddress, "The EmailAddress should match the input.");
            accountFromDb.Role.Should().Be(newAccountDto.Role, "The Role should match the input.");
            accountFromDb.Id.GetType().Should().Be(typeof(Guid));
            accountFromDb.UserId.Should().Be(null);
            accountFromDb.Role.Should().Be(newAccountDto.Role);
            accountFromDb.IsDeleted.Should().BeFalse();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

    }
}
