using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using CD = GameNightScoresRight.CommonDTOs;
using EF = GameNightScoresRight.EFDTOs;
using GameNightScoresRight.Data;

namespace GameNightScoresRight.Accessors
{
    public class AccountAccessor : IAccountAccessor
    {
        private readonly GameNightDbContext _db;

        public AccountAccessor(GameNightDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<CD.CreateAccountResponse> CreateAccount(CD.CreateAccountRequest createAccountRequest)
        {
            var newAccount = new EF.Account
            {
                EmailAddress = createAccountRequest.EmailAddress,
                Role = createAccountRequest.Role ?? 1,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
            };

            await _db.Accounts.AddAsync(newAccount);
            await _db.SaveChangesAsync();

            // get the new account to be mapped and returned
            var createdAccount = await _db.Accounts.FirstOrDefaultAsync(a => a.EmailAddress == newAccount.EmailAddress);
            return new CD.CreateAccountResponse
            {
                Id = createdAccount!.Id,
            };
        }
    }
}
