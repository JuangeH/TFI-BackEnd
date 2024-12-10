using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ApplicationModels
{
    public class Users : IdentityUser
    {
        public Users()
        {
            trofeosModel = new HashSet<TrofeoModel>();
            adquicionesModel = new HashSet<AdquisicionModel>();
            tiempoDeJuegoModel = new HashSet<TiempoDeJuegoModel>();
            videojuegoInteresModel = new HashSet<VideojuegoInteresModel>();
            medioDePagoModels = new HashSet<MedioDePagoModel>();
            foroUsuarioVisitaModels = new HashSet<ForoUsuarioVisitaModel>();
            foroUsuarioFavoritoModels = new HashSet<ForoUsuarioFavoritoModel>();
            comentarioModels = new HashSet<ComentarioModel>();
            puntuacionModels = new HashSet<PuntuacionModel>();
            foroModels = new HashSet<ForoModel>();
            usuarioVisitaModels = new HashSet<UsuarioVisitaModel>();
            recomendacionUsuarioModels = new HashSet<RecomendacionUsuarioModel>();
            recomendacionVideojuegoModels = new HashSet<RecomendacionVideojuegoModel>();
            usuarioBaneadoModels = new HashSet<UsuarioBaneadoModel>();
        }
        public bool Active { get; set; }
        public bool CommunityBanned { get; set; }
        public UsersPrivileges UserPrivileges { get; set; }
        public virtual ICollection<RefreshToken> UserRefreshTokens { get; set; }

        //Agrego propiedades personalizadas
        public string Idioma { get; set; }
        public SteamAccountModel SteamAccountModel { get; set; }
        public SuscripcionUsuarioModel suscripcionUsuarioModel { get; set; }
        public ICollection<MedioDePagoModel> medioDePagoModels { get; set; }
        public ICollection<TrofeoModel> trofeosModel { get; set; }
        public ICollection<AdquisicionModel> adquicionesModel { get; set; }
        public ICollection<TiempoDeJuegoModel> tiempoDeJuegoModel { get; set; }
        public ICollection<VideojuegoInteresModel> videojuegoInteresModel { get; set; }
        public ICollection<ForoUsuarioVisitaModel> foroUsuarioVisitaModels { get; set; }
        public ICollection<ForoUsuarioFavoritoModel> foroUsuarioFavoritoModels { get; set; }
        public ICollection<ComentarioModel> comentarioModels { get; set; }
        public ICollection<PuntuacionModel> puntuacionModels { get; set; }
        public ICollection<ForoModel> foroModels { get; set; }
        public ICollection<UsuarioVisitaModel> usuarioVisitaModels { get; set; }
        public ICollection<RecomendacionUsuarioModel> recomendacionUsuarioModels { get; set; }
        public ICollection<RecomendacionVideojuegoModel> recomendacionVideojuegoModels { get; set; }
        public ICollection<UsuarioJuegoPerfilModel> usuarioJuegoPerfilModels { get; set; }
        public ICollection<UsuarioBaneadoModel> usuarioBaneadoModels { get; set; }
        public ICollection<UsuarioBaneadoModel> usuarioBaneadoAdminModels { get; set; }
    }
}
