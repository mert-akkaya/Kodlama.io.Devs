using Application.Features.OperationClaims.Models;
using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetListUserOperationClaim
{

    public class GetListUserOperationClaimQuery : IRequest<UserOperationClaimListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, UserOperationClaimListModel>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMapper _mapper;

            public GetListUserOperationClaimQueryHandler(IUserOperationClaimRepository userOerationClaimRepository, IMapper mapper)
            {
                _userOperationClaimRepository = userOerationClaimRepository;
                _mapper = mapper;
            }

            public async Task<UserOperationClaimListModel> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                IPaginate<UserOperationClaim> userOperationClaims = await _userOperationClaimRepository.GetListAsync(include:c=>c.Include(u=>u.User).Include(u=>u.OperationClaim),index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                UserOperationClaimListModel userOperationClaimListModel = _mapper.Map<UserOperationClaimListModel>(userOperationClaims);
                return userOperationClaimListModel;
            }
        }
    }
}
