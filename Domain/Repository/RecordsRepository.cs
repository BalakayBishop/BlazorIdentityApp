using DataAccess.DbRepository;
using DataAccess.Entities;
using DataAccess.IDbRepository;
using Domain.IRepository;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class RecordsRepository : IRecordsRepository
    {
        private readonly IDbRecordsRepository _dbRecordsRepository;
        public RecordsRepository(IDbRecordsRepository dbRecordsRepository)
        {
            _dbRecordsRepository = dbRecordsRepository;
        }

        public async Task<RecordsViewModel> CreateAsync(RecordsViewModel record)
        {
            try
            {
                Records dbRecord = new()
                {
                    Title = record.Title,
                    Description = record.Description,
                    CreatedDate = DateTime.UtcNow,
                };
                 _dbRecordsRepository.Create(dbRecord);
                record.Id = dbRecord.Id;

                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null!;
            }
        }

        public async Task<List<RecordsViewModel>> ReadAllAsync()
        {
            try
            {
                List<Records> dbRecords = await _dbRecordsRepository.GetAll();

                if (dbRecords is not null)
                {
                    List<RecordsViewModel> records = new();
                    foreach (Records dbRecord in dbRecords)
                    {
                        RecordsViewModel record = new()
                        {
                            Id = dbRecord.Id,
                            Title = dbRecord.Title,
                            Description = dbRecord.Description,
                            CreatedDate = dbRecord.CreatedDate,
                        };

                        records.Add(record);
                    }

                    return records;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<RecordsViewModel> ReadAsync(int id)
        {
            try
            {
                Records dbRecord = await _dbRecordsRepository.GetById(id);

                if (dbRecord is not null)
                {
                    RecordsViewModel record = new()
                    {
                        Id = dbRecord.Id,
                        Title = dbRecord.Title,
                        Description = dbRecord.Description,
                        CreatedDate = dbRecord.CreatedDate,
                    };

                    return record;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<RecordsViewModel> UpdateAsync(RecordsViewModel record)
        {
            if (record is not null)
            {
                try
                {
                    Records dbRecord = await _dbRecordsRepository.GetById(record.Id);
                    if (dbRecord is not null)
                    {
                        dbRecord.Title = record.Title;
                        dbRecord.Description = record.Description;

                        await _dbRecordsRepository.Update(dbRecord);

                        return record;
                    }
                }
                catch
                {
                    return null!;
                }
            }

            return null!;
        }

        public Task<List<RecordsViewModel>> UpdateRangeAsync(List<RecordsViewModel> records)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(RecordsViewModel record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(List<RecordsViewModel> records)
        {
            throw new NotImplementedException();
        }
    }
}
