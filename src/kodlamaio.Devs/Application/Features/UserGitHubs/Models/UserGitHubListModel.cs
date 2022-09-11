using Application.Features.UserGitHubs.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserGitHubs.Models
{
    public class UserGitHubListModel:BasePageableModel
    {
        public ICollection<UserGitHubListDto> Items { get; set; }
    }
}
