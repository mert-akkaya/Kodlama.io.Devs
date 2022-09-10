using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GatUserByEmail
{
    public class GetUserByEmailQuery:IRequest<User>
    {
        public string Email { get; set; }
        

        public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
        {
            private readonly IUserRepository _userRepository;
           

            public GetUserByEmailQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
              
            }

            public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
            {
                User user = await _userRepository.GetAsync(u => u.Email == request.Email);
                return user;
            }
        }
    }
}
