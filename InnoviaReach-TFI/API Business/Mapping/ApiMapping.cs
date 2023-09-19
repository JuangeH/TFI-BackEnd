using API_Business.Request;
using AutoMapper;
using Core.Domain.ApplicationModels;
using Core.Domain.DTOs;
using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Helpers.ResultClasses;

namespace Api.Mapping
{
    public class ApiMapping : Profile
    {
        public ApiMapping()
        {
            CreateMap<VideojuegoModel, Apps>();
            CreateMap<Apps, VideojuegoModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.name));

            //CreateMap<Privileges, PrivilegesPutRequest>();
            //CreateMap<PrivilegesPutRequest, Privileges>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PrivilegeNewName))
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => src.concurrencyStamp));

            //CreateMap<Privileges, PrivilegesPostRequest>();
            //CreateMap<PrivilegesPostRequest, Privileges>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PrivilegeName));

            //CreateMap<IdentityResult, GenericResult<RegisterDto>>()
            //    .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(p => p.Description)))
            //    .ForPath(dest => dest.Data.Code, opt => opt.MapFrom(src => src.Errors.Select(p => p.Code)));

            //CreateMap<ChangePasswordDto, ChangePasswordRequest>();

        }
    }
}
