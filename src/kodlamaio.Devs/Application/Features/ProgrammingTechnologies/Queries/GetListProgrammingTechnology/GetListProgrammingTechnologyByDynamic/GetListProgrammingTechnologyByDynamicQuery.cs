using Application.Features.ProgrammingTechnologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology.GetListProgrammingTechnologyByDynamic
{
  
    public class GetListProgrammingTechnologyByDynamicQuery : IRequest<ProgrammingTechnologyListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListProgrammingTechnologyByDynamicQueryHandler : IRequestHandler<GetListProgrammingTechnologyByDynamicQuery, ProgrammingTechnologyListModel>
        {
            private readonly IProgrammingTechnologyRepository _programmingTechnologyRepository;
            private readonly IMapper _mapper;

            public GetListProgrammingTechnologyByDynamicQueryHandler(IProgrammingTechnologyRepository programmingTechnologyRepository, IMapper mapper)
            {
                _programmingTechnologyRepository = programmingTechnologyRepository;
                _mapper = mapper;
            }
            public async Task<ProgrammingTechnologyListModel> Handle(GetListProgrammingTechnologyByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingTechnology> programmingTechnologies = await _programmingTechnologyRepository.GetListByDynamicAsync(
                    dynamic:request.Dynamic,
                    include: p => p.Include(t => t.ProgrammingLanguage),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                    );
                ProgrammingTechnologyListModel mappedProgrammingTechnologies = _mapper.Map<ProgrammingTechnologyListModel>(programmingTechnologies);
                return mappedProgrammingTechnologies;
            }
        }
    }
}
