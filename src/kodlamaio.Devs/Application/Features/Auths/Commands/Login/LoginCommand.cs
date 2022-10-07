using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
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

        public string IPAddress { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {

            private readonly IUserRepository _userRepository;

            private readonly AuthBusinessRules _authBusinessRules;

            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            private readonly IAuthService _authService;

            private readonly IMapper _mapper;

            public LoginCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules,IUserOperationClaimRepository userOperationClaimRepository, IAuthService authService,IMapper mapper)
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _userOperationClaimRepository = userOperationClaimRepository;
                _authService = authService;
                _mapper = mapper;
            }
            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user = await _authBusinessRules.UserShouldExistWhenLogin(request.UserForLoginDto.Email);
                await _authBusinessRules.PasswordCheckWhenLogin(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt);

                AccessToken createdAccessToken = await _authService.CreateAccessToken(user);

                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);

                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoginedDto loginedDto = new()
                {
                    AccessToken = createdAccessToken,
                    RefreshToken = addedRefreshToken
                };

                return loginedDto;


            }
        }
    }
}
