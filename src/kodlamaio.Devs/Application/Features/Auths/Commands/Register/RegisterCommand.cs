using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<AccessToken>
    { 
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
        {

            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly ITokenHelper _tokenHelper;


            public RegisterCommandHandler(IUserRepository userRepository,AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper)
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _tokenHelper = tokenHelper;
            }

            public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.UserCanNotBeDuplicatedWhenRegister(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
                User user = new User()
                {
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Email = request.UserForRegisterDto.Email,
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    Status = true,
                    AuthenticatorType = AuthenticatorType.None,
                };

                User createdUser = await _userRepository.AddAsync(user);
                AccessToken token = _tokenHelper.CreateToken(createdUser, new List<OperationClaim>());
                return token;

            }
        }
    }
}
