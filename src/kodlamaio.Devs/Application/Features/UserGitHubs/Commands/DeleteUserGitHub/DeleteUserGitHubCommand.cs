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

namespace Application.Features.UserGitHubs.Commands.DeleteUserGitHub
{

    public class DeleteUserGitHubCommand : IRequest<DeletedUserGitHubDto>
    {
        public int Id { get; set; }

        public class DeleteUserGitHubCommandHandler : IRequestHandler<DeleteUserGitHubCommand, DeletedUserGitHubDto>
        {
            private readonly IUserGitHubRepository _userGitHubRepository;
            private readonly IMapper _mapper;

            public DeleteUserGitHubCommandHandler(IUserGitHubRepository userGitHubRepository, IMapper mapper)
            {
                _userGitHubRepository = userGitHubRepository;
                _mapper = mapper;
            }

            public async Task<DeletedUserGitHubDto> Handle(DeleteUserGitHubCommand request, CancellationToken cancellationToken)
            {
                UserGitHub userGitHub = _mapper.Map<UserGitHub>(request);
                UserGitHub deletedUserGitHub = await _userGitHubRepository.DeleteAsync(userGitHub);
                DeletedUserGitHubDto deletedUserGithubDto = _mapper.Map<DeletedUserGitHubDto>(deletedUserGitHub);
                return deletedUserGithubDto;
            }
        }
    }

}
