using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {

            private readonly IUserRepository _userRepository;

            private readonly AuthBusinessRules _authBusinessRules;

            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            private readonly ITokenHelper _tokenHelper;

            private readonly IMapper _mapper;

            public LoginCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules,IUserOperationClaimRepository userOperationClaimRepository,ITokenHelper tokenHelper,IMapper mapper)
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _userOperationClaimRepository = userOperationClaimRepository;
                _tokenHelper = tokenHelper;
                _mapper = mapper;
            }
            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user = await _authBusinessRules.UserShouldExistWhenLogin(request.UserForLoginDto.Email);
                await _authBusinessRules.PasswordCheckWhenLogin(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(u => u.UserId == user.Id, include: i => i.Include(i => i.OperationClaim));
                AccessToken accessToken = _tokenHelper.CreateToken(user, userOperationClaims.Items.Select(u => u.OperationClaim).ToList());

                LoginedDto loginedDto = _mapper.Map<LoginedDto>(user);
                loginedDto.AccessToken = accessToken;

                return loginedDto;


            }
        }
    }
}
