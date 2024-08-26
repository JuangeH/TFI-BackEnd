﻿using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IForoService : IGenericService<ForoModel>
    {
        public Task<List<ForoModel>> ObtenerForosGenerales();
        public Task<ForoModel> ObtenerForo(int id);
    }
}
