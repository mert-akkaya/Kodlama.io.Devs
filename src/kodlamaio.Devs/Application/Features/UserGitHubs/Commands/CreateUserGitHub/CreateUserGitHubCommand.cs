using Application.Features.UserGitHubs.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.CreateUserGitHub
{
    public class CreateUserGitHubCommand :IRequest<CreatedUserGitHubDto>
    {
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }

        public class CreateUserGitHubCommandHandler : IRequestHandler<CreateUserGitHubCommand, CreatedUserGitHubDto>
        {
            private readonly IUserGitHubRepository _userGitHubRepository;
            private readonly IMapper _mapper;

            public CreateUserGitHubCommandHandler(IUserGitHubRepository userGitHubRepository, IMapper mapper)
            {
                _userGitHubRepository = userGitHubRepository;
                _mapper = mapper;
            }

            public async Task<CreatedUserGitHubDto> Handle(CreateUserGitHubCommand request, CancellationToken cancellationToken)
            {
                UserGitHub userGitHub = _mapper.Map<UserGitHub>(request);
                UserGitHub createdUserGitHub = await _userGitHubRepository.AddAsync(userGitHub);
                CreatedUserGitHubDto createdUserGithubDto = _mapper.Map<CreatedUserGitHubDto>(createdUserGitHub);
                return createdUserGithubDto;
            }
        }
    }
}
