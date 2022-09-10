using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;
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

            public LoginCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
               
            }
        }
    }
}
