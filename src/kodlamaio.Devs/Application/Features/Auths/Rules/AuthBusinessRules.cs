using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> UserShouldExistWhenLogin(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(b => b.Email == email);
            if (result.Count<=0) throw new BusinessException("User with requested email doesn't exist.");

            return result.Items.First();
        }

        public async Task PasswordCheckWhenLogin(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            bool result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (!result) throw new BusinessException("Credentials doesn't match.");
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);

            if (user != null) throw new BusinessException("Mail already exist");

        }
    }
}
