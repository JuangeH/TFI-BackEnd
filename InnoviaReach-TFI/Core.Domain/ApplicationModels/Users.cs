﻿using Core.Domain.Models;
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
            foroUsuarioModels = new HashSet<ForoUsuarioModel>();
            comentarioModels = new HashSet<ComentarioModel>();
        }
        public bool Active { get; set; }
        public UsersPrivileges UserPrivileges { get; set; }
        public virtual ICollection<RefreshToken> UserRefreshTokens { get; set; }

        //Agrego propiedades personalizadas

        public string Idioma { get; set; }
        public string Estilo_preferido { get; set; }
        public string Genero_preferido { get; set; }
        public bool Actualizaciones { get; set; }
        public bool Descuentos { get; set; }
        public SuscripcionUsuarioModel suscripcionUsuarioModel { get; set; }
        public ICollection<MedioDePagoModel> medioDePagoModels { get; set; }
        public ICollection<TrofeoModel> trofeosModel { get; set; }
        public ICollection<AdquisicionModel> adquicionesModel { get; set; }
        public ICollection<TiempoDeJuegoModel> tiempoDeJuegoModel { get; set; }
        public ICollection<VideojuegoInteresModel> videojuegoInteresModel { get; set; }
        public ICollection<ForoUsuarioModel> foroUsuarioModels { get; set; }
        public ICollection<ComentarioModel> comentarioModels { get; set; }
    }
}
