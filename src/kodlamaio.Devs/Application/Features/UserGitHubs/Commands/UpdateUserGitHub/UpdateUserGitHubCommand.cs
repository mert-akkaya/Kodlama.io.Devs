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

namespace Application.Features.UserGitHubs.Commands.UpdateUserGitHub
{
    public class UpdateUserGitHubCommand : IRequest<UpdatedUserGitHubDto>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProfileUrl { get; set; }

        public class UpdateUserGithubCommandHandler : IRequestHandler<UpdateUserGitHubCommand, UpdatedUserGitHubDto>
        {
            private readonly IUserGitHubRepository _userGitHubRepository;
            private readonly IMapper _mapper;

            public UpdateUserGithubCommandHandler(IUserGitHubRepository userGitHubRepository, IMapper mapper)
            {
                _userGitHubRepository = userGitHubRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedUserGitHubDto> Handle(UpdateUserGitHubCommand request, CancellationToken cancellationToken)
            {
                UserGitHub userGitHub = _mapper.Map<UserGitHub>(request);
                UserGitHub updatedUserGitHub = await _userGitHubRepository.UpdateAsync(userGitHub);
                UpdatedUserGitHubDto updatedUserGithubDto = _mapper.Map<UpdatedUserGitHubDto>(updatedUserGitHub);
                return updatedUserGithubDto;
            }
        }
    }
}
