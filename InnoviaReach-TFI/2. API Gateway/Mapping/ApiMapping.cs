using _2._API.Response;
using Api.Request;
using Api.Request.Privileges;

using AutoMapper;
using Core.Domain.ApplicationModels;
using Core.Domain.DTOs;
using Core.Domain.Models;
using Core.Domain.Request.Gateway;
using Core.Domain.Response.Business;
using Core.Domain.Response.Gateway;
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
            CreateMap<Users, RegisterRequest>();
            CreateMap<RegisterRequest, Users>();

            CreateMap<Privileges, PrivilegesPutRequest>();
            CreateMap<PrivilegesPutRequest, Privileges>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PrivilegeNewName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => src.concurrencyStamp));

            CreateMap<Privileges, PrivilegesPostRequest>();
            CreateMap<PrivilegesPostRequest, Privileges>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PrivilegeName));
            
            CreateMap<IdentityResult, GenericResult<RegisterDto>>()
                .ForMember(dest => dest.Errors, opt => opt.MapFrom(src => src.Errors.Select(p => p.Description)))
                .ForPath(dest => dest.Data.Code, opt => opt.MapFrom(src => src.Errors.Select(p => p.Code)));

            CreateMap<ChangePasswordDto, ChangePasswordRequest>();

            CreateMap<ChangePasswordRequest, ChangePasswordDto>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<LogTableModel, LogTableResponse>();

            CreateMap<Core.Domain.Models.Renderings, _2._API.Response.Renderings>();
            CreateMap<Core.Domain.Models.Rendering, _2._API.Response.Rendering>();
            CreateMap<Core.Domain.Models.KeyIdItem, _2._API.Response.KeyIdItem>();
            CreateMap<Core.Domain.Models.LogProperties, _2._API.Response.LogProperties>();
            CreateMap<Core.Domain.Models.EventId, _2._API.Response.EventId>();

            CreateMap<Users, UserResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            CreateMap<SteamAccountModel, SteamAccountResponse>()
                .ForMember(dest => dest.steamid, opt => opt.MapFrom(src => src.steamid))
                .ForMember(dest => dest.personaname, opt => opt.MapFrom(src => src.personaname))
                .ForMember(dest => dest.avatarfull, opt => opt.MapFrom(src => src.avatarfull))
                .ForMember(dest => dest.profileurl, opt => opt.MapFrom(src => src.profileurl));

            CreateMap<Users, UserConfigResponse>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.Idioma, opt => opt.MapFrom(src => src.Idioma))
               .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.CommunityBanned))
               .ForMember(dest => dest.Contraseña, opt => opt.Ignore());

            //CreateMap<Users, CultureResponse>()
            //   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            //   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //   .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));
        }
    }
}
