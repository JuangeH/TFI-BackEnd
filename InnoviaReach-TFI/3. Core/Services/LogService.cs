using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class LogService : GenericService<LogTableModel>, ILogService
    {
        public LogService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<ILogRepository>())
        {

        }

        public async Task<List<LogTableModel>> ObtenerLogs()
        {
            try
            {
                return (await _repository.Get(x => x.Id.ToString() != "")).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
