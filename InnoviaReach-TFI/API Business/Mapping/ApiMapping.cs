using API_Business.Request;
using API_Business.Response;
using AutoMapper;
using Core.Domain.ApplicationModels;
using Core.Domain.DTOs;
using Core.Domain.Models;
using Core.Domain.Models.Nueva_Base;
using Core.Domain.Response.Business;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
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
            //CreateMap<VideojuegoModel, Apps>();
            //CreateMap<Apps, VideojuegoModel>()
            //    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.name));

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

            CreateMap<VideojuegoModel, VideojuegoResponse>()
                .ForMember(dest => dest.VideojuegoEstilo, opt => opt.MapFrom(src => src.videojuegoEstiloModels))
                .ForMember(dest => dest.VideojuegoGenero, opt => opt.MapFrom(src => src.videojuegoGeneroModels));
            //.ForMember(dest => dest.Plataforma, opt => opt.MapFrom(src => src.Plataforma));

            CreateMap<EstiloModel, EstiloResponse>();
            CreateMap<GeneroModel, GeneroResponse>();
            CreateMap<PlataformaModel, PlataformaResponse>();

            CreateMap<VideojuegoEstiloModel, VideojuegoEstiloResponse>()
                .ForMember(dest => dest.estilo, opt => opt.MapFrom(src => src.estiloModel));

            CreateMap<VideojuegoGeneroModel, VideojuegoGeneroResponse>()
                .ForMember(dest => dest.genero, opt => opt.MapFrom(src => src.generoModel));

            CreateMap<ComentarioModel, ComentarioResponse>()
                .ForMember(dest => dest.Creador, opt => opt.MapFrom(src => src.usuario.UserName))
                .ForMember(dest => dest.ComentarioPadre_Codigo, opt => opt.MapFrom(src => src.ComentarioPadre_ID))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Comentario_ID))
                .ForMember(dest => dest.Contenido, opt => opt.MapFrom(src => src.Contenido))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.CantidadVotos, opt => opt.MapFrom(src => src.puntuacionModels.Count))
                .ForMember(dest => dest.PromedioPuntaje, opt => opt.MapFrom(src => src.puntuacionModels.Any()
                    ? src.puntuacionModels.Average(p => p.Puntaje)
                    : 0.0));

            CreateMap<ForoModel, ForoResponse>()
                .ForMember(dest => dest.NombreVideoJuego, opt => opt.MapFrom(src => src.videojuego.Nombre))
                .ForMember(dest => dest.NombreUsuarioCreador, opt => opt.MapFrom(src => src.usuario.UserName))
                .ForMember(dest => dest.Visitas, opt => opt.MapFrom(src => src.foroUsuarioVisitaModels.Count))
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Foro_ID))
                .ForMember(dest => dest.favorito, opt => opt.MapFrom(src => src.foroUsuarioFavoritoModels))

                .ForMember(dest => dest.favorito, opt => opt.MapFrom((src, dest, _, context) =>
                    src.foroUsuarioFavoritoModels.Any(f => f.User_ID == (string)context.Items["LoggedUserID"])))

                .ForMember(dest => dest.CantidadComentarios, opt => opt.MapFrom(src => src.comentarioModels.Count));

            CreateMap<VideojuegoModel, VideojuegoForoReponse>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                 .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Videojuego_ID));

            CreateMap<VideojuegoModel, VideojuegoCatalogoResponse>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.AppRawgId))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Imagen, opt => opt.MapFrom(src => src.Imagen));

            CreateMap<VideojuegoModel, VideojuegoCatalogoDetalleResponse>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Imagen, opt => opt.MapFrom(src => src.Imagen))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.FechaSalida, opt => opt.MapFrom(src => src.FechaSalida))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.videojuegoTagModels.Select(vt => vt.tagModel.Nombre)))
                .ForMember(dest => dest.Generos, opt => opt.MapFrom(src => src.videojuegoGeneroModels.Select(vg => vg.generoModel.Nombre)))
                .ForMember(dest => dest.Plataformas, opt => opt.MapFrom(src => src.videojuegoPlataformaModels.Select(vp => vp.plataformaModel.Nombre)))
                .ForMember(dest => dest.Tiendas, opt => opt.MapFrom(src => src.videojuegoTiendaModels.Select(vt => vt.tiendaModel.Dominio)));

            CreateMap<Genre, GeneroModel>()
                 .ForMember(dest => dest.GenreRawgID, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug));

            CreateMap<Platform, PlataformaModel>()
                 .ForMember(dest => dest.PlatformRawgID, opt => opt.MapFrom(src => src.Plataforma_Id))
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                 .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug));

            CreateMap<Rating, RatingModel>()
                 .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.CantidadVotos, opt => opt.MapFrom(src => src.Count))
                 .ForMember(dest => dest.Porcentaje, opt => opt.MapFrom(src => src.Percent));

            CreateMap<Tag, TagModel>()
                 .ForMember(dest => dest.TagRawgId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug));

            CreateMap<StoreInfo, TiendaModel>()
                 .ForMember(dest => dest.StoreRawgId, opt => opt.MapFrom(src => src.id))
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
                 .ForMember(dest => dest.Dominio, opt => opt.MapFrom(src => src.Domain));

            //// Mapeo de Users a UsuarioClusterModel
            //CreateMap<Users, UsuarioClusterModel>()
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.ClusterID, opt => opt.MapFrom(src => src.ClusterID ?? 0)) // Usa 0 si es nulo
            //    .ForMember(dest => dest.GameGenres, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.GameGenresJson)
            //        ? new Dictionary<string, int>()
            //        : JsonConvert.DeserializeObject<Dictionary<string, int>>(src.GameGenresJson)))
            //    .ForMember(dest => dest.GameTags, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.GameTagsJson)
            //        ? new Dictionary<string, int>()
            //        : JsonConvert.DeserializeObject<Dictionary<string, int>>(src.GameTagsJson)))
            //    .ForMember(dest => dest.GameHistory, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.GameHistoryJson)
            //        ? new List<string>()
            //        : JsonConvert.DeserializeObject<List<string>>(src.GameHistoryJson)));

            //CreateMap<Users, UsuarioClusterModel>()
            //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.GameGenresVector, opt => opt.MapFrom(src => MapearGameGenresVector(src, _allGenres)))
            //.ForMember(dest => dest.GameTagsVector, opt => opt.MapFrom(src => MapearGameTagsVector(src, _allTags)))
            //.ForMember(dest => dest.GameHistory, opt => opt.MapFrom(src => MapearGameHistory(src)))
            //.ForMember(dest => dest.ClusterID, opt => opt.MapFrom(src => src.ClusterID ?? 0));


            //// Mapeo de UsuarioClusterModel a Users
            //CreateMap<UsuarioClusterModel, Users>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            //    .ForMember(dest => dest.ClusterID, opt => opt.MapFrom(src => src.ClusterID))
            //    .ForMember(dest => dest.GameGenresJson, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.GameGenres)))
            //    .ForMember(dest => dest.GameTagsJson, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.GameTags)))
            //    .ForMember(dest => dest.GameHistoryJson, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.GameHistory)));

            CreateMap<VideojuegoModel, VideojuegoClusterModel>()
            .ForMember(dest => dest.Videojuego_ID, opt => opt.MapFrom(src => src.Videojuego_ID))
            .ForMember(dest => dest.AppRawgId, opt => opt.MapFrom(src => src.AppRawgId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Caracteristicas, opt => opt.MapFrom(src => ConcatenarCaracteristicas(src))) // Concatenar características
            .ForMember(dest => dest.ClusterID, opt => opt.Ignore()) // Ignorar ClusterID, lo calculará el modelo
            .ForMember(dest => dest.CaracteristicasVector, opt => opt.Ignore()); // Ignorar CaracteristicasVector, lo calculará el modelo

            CreateMap<VideojuegoModel, VideojuegoRecModel>()
            .ForMember(dest => dest.Videojuego_ID, opt => opt.MapFrom(src => src.Videojuego_ID))
            .ForMember(dest => dest.AppRawgId, opt => opt.MapFrom(src => src.AppRawgId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.ClusterID, opt => opt.MapFrom(src => src.ClusterID))
            .ForMember(dest => dest.CaracteristicasVector, opt => opt.MapFrom(src => src.CaracteristicasVector));

            CreateMap<VideojuegoClusterModel, VideojuegoModel>()
            .ForMember(dest => dest.AppRawgId, opt => opt.MapFrom(src => src.AppRawgId))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<RecomendacionVideojuegoModel, RecomendacionVideojuegoResponse>()
            .ForMember(dest => dest.VideojuegoReferencia,
                       opt => opt.MapFrom(src => src.videojuegoReferencia.Nombre)) // Mapeo al nombre del videojuego de referencia
            .ForMember(dest => dest.VideojuegoRecomendado,
                       opt => opt.MapFrom(src => src.videojuegoRecomendado.Nombre)) // Mapeo al nombre del videojuego recomendado
            .ForMember(dest => dest.VideojuegoRecomendadoImagen,
                       opt => opt.MapFrom(src => src.videojuegoRecomendado.Imagen)) // Mapeo a la imagen del videojuego recomendado
            .ForMember(dest => dest.TipoRecomendacion,
                       opt => opt.MapFrom(src => src.TipoRecomendacion)); // Mapeo a la imagen del videojuego recomendado

            CreateMap<RecomendacionUsuarioModel, RecomendacionUsuarioResponse>()
            .ForMember(dest => dest.VideojuegoRecomendado,
                       opt => opt.MapFrom(src => src.videojuego.Nombre)) // Mapea el nombre del videojuego recomendado
            .ForMember(dest => dest.VideojuegoRecomendadoImagen,
                       opt => opt.MapFrom(src => src.videojuego.Imagen))
            .ForMember(dest => dest.TipoRecomendacion,
                       opt => opt.MapFrom(src => src.TipoRecomendacion)); // Mapea la imagen del videojuego recomendado




        }
        private string ConcatenarCaracteristicas(VideojuegoModel src)
        {
            // Concatenación personalizada de características
            var plataformas = src.videojuegoPlataformaModels != null
                ? string.Join(", ", src.videojuegoPlataformaModels.Select(p => p.plataformaModel.Nombre))
                : string.Empty;

            var generos = src.videojuegoGeneroModels != null
                ? string.Join(", ", src.videojuegoGeneroModels.Select(g => g.generoModel.Nombre))
                : string.Empty;

            var tags = src.videojuegoTagModels != null
                ? string.Join(", ", src.videojuegoTagModels.Select(t => t.tagModel.Nombre))
                : string.Empty;

            //var ratings = src.ratingModels != null
            //    ? string.Join(" ", src.ratingModels.Select(r => $"{r.Titulo}:{r.CantidadVotos}Votos({r.Porcentaje}%)"))
            //    : string.Empty;

            return $"Plataformas: {plataformas} Generos: {generos} Tags: {tags}";
        }
    }
}
