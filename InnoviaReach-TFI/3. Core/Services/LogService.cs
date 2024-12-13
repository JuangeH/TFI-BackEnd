using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class LogService : ILogService
    {
        private readonly IMongoCollection<LogTableModel> _logsCollection;

        public LogService(IMongoClient mongoClient, string databaseName, string collectionName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _logsCollection = database.GetCollection<LogTableModel>(collectionName);
        }

        public async Task<List<LogTableModel>> ObtenerLogs()
        {
            return await _logsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<List<LogTableModel>> ObtenerBusinessLogs()
        {
            // Filtrar por el nombre del evento de negocio
            var filter = Builders<LogTableModel>.Filter.Eq("Properties.EventId.Name", "Business");
            return await _logsCollection.Find(filter).ToListAsync();
        }
    }
}
