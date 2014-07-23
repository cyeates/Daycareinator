using Daycareinator.Data;
using Daycareinator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daycareinator.Domain.Services
{
    public interface IRecordsService
    {
        IEnumerable<Record> GetRecordsByType(RecordType recordType);
    }
    public class RecordsService : IRecordsService
    {
        private IUnitOfWork _uow;
        public RecordsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<Record> GetRecordsByType(RecordType recordType)
        {
            return _uow.Records.Find(r => r.RecordType == recordType);
        }
    }
}
