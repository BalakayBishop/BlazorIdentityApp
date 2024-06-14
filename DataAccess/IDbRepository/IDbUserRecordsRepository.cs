using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IDbRepository
{
    public interface IDbUserRecordsRepository : IDbBaseRepository<UserRecords>
    {
        Task<UserRecords> GetByPk(int UserId, int RecordId);
    }
}
