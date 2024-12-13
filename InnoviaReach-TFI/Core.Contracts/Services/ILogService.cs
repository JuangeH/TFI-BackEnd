using Core.Domain.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface ILogService
    {
        public Task<List<LogTableModel>> ObtenerLogs();
        public Task<List<LogTableModel>> ObtenerBusinessLogs();
    }
}
