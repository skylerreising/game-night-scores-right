using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using CD = GameNightScoresRight.CommonDTOs;
using EF = GameNightScoresRight.EFDTOs;
using GameNightScoresRight.Data;
using AutoMapper;

namespace GameNightScoresRight.Accessors
{
    public class AccountAccessor : IAccountAccessor
    {
        private readonly GameNightDbContext _db;
        private readonly IMapper _mapper;

        public AccountAccessor(GameNightDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        public async Task<CD.CreateAccountResponse> CreateAccount(CD.CreateAccountRequest createAccountRequest)
        {
            var newAccount = _mapper.Map<EF.Account>(createAccountRequest);

            await _db.Accounts.AddAsync(newAccount);
            await _db.SaveChangesAsync();

            var response = _mapper.Map<CD.CreateAccountResponse>(newAccount);
            return response;
        }
    }
}
