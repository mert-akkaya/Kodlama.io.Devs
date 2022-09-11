using Application.Features.UserGitHubs.Commands.DeleteUserGitHub;
using Application.Features.UserGitHubs.Commands.UpdateUserGitHub;
using Application.Features.UserGitHubs.Dtos;
using Application.Features.UserGitHubs.Models;
using Application.Features.Users.Commands.CreateUserGitHub;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserGitHubs.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserGitHub, CreatedUserGitHubDto>().ReverseMap();
            CreateMap<UserGitHub, CreateUserGitHubCommand>().ReverseMap();

            CreateMap<UserGitHub, UpdatedUserGitHubDto>().ReverseMap();
            CreateMap<UserGitHub, UpdateUserGitHubCommand>().ReverseMap();

            CreateMap<UserGitHub, DeletedUserGitHubDto>().ReverseMap();
            CreateMap<UserGitHub, DeleteUserGitHubCommand>().ReverseMap();

            CreateMap<UserGitHub, UserGitHubListDto>().ForMember(c => c.UserName, opt => opt.MapFrom(c => c.User.FirstName)).ReverseMap();

            CreateMap<IPaginate<UserGitHub>, UserGitHubListModel>().ReverseMap();
        }


    }
}
