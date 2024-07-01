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

        /// <summary>
        /// Asynchronously creates a new record in the database.
        /// </summary>
        /// <param name="record">The record view model containing the data to be added.</param>
        /// <returns>The created record view model with the ID populated, or null if an error occurs.</returns>
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
                 await _dbRecordsRepository.Create(dbRecord);
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
                    var records = dbRecords.ConvertAll(dbRecord => new RecordsViewModel
                    {
                        Id = dbRecord.Id,
                        Title = dbRecord.Title,
                        Description = dbRecord.Description,
                        CreatedDate = dbRecord.CreatedDate,
                    });

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
