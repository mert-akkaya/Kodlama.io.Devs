using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserGithubRepository : EfRepositoryBase<UserGitHub, BaseDbContext>, IUserGitHubRepository

    {
        public UserGithubRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
