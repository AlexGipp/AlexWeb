using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalWebsite.Database.MongoDB
{
    public interface IDataService
    {
        Task InsertRecordAsync<T>(string table, T record);

        Task UpsertRecordAsync<T>(string table, Guid id, T record);

        Task DeleteRecordAsync<T>(string table, Guid id);

        Task<List<T>> LoadAllRecordsAsync<T>(string table);

        Task<T> LoadRecordByIdAsync<T>(string table, Guid id);
    }
}