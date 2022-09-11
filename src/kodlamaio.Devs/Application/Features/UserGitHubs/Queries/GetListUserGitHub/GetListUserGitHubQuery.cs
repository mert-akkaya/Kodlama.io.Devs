using Application.Features.UserGitHubs.Models;
using Application.Features.UserOperationClaims.Models;
using Application.Features.UserOperationClaims.Queries.GetListUserOperationClaim;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserGitHubs.Queries.GetListUserGitHub
{
    public class GetListUserGitHubQuery:IRequest<UserGitHubListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GitListUserGitHubQueryHandler : IRequestHandler<GetListUserGitHubQuery, UserGitHubListModel>
        {
            private readonly IUserGitHubRepository _userGitHubRepository;
            private readonly IMapper _mapper;

            public GitListUserGitHubQueryHandler(IUserGitHubRepository userGitHubRepository, IMapper mapper)
            {
                _userGitHubRepository = userGitHubRepository;
                _mapper = mapper;
            }

            public async Task<UserGitHubListModel> Handle(GetListUserGitHubQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserGitHub> userGitHubs = await _userGitHubRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                UserGitHubListModel userGitHubListModel = _mapper.Map<UserGitHubListModel>(userGitHubs);
                return userGitHubListModel;
            }
        }
    }
}
