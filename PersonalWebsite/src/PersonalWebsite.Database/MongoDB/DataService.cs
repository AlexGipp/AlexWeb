using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace PersonalWebsite.Database.MongoDB
{
    public class DataService : IDataService
    {
        private IMongoDatabase _db;
        public DataService(string database)
        {
            var client = new MongoClient();
            _db = client.GetDatabase(database);
        }
        public async Task InsertRecordAsync<T>(string table, T record)
        {
            var collection = _db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
        }

        public async Task UpsertRecordAsync<T>(string table, Guid id, T record)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            await collection.ReplaceOneAsync(
                filter,
                record,
                new UpdateOptions {IsUpsert = true});
        }

        public async Task DeleteRecordAsync<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.DeleteOneAsync(filter);
        }

        public async Task<List<T>> LoadAllRecordsAsync<T>(string table)
        {
            var collection = _db.GetCollection<T>(table);
            var fullCollection = await collection.FindAsync(new BsonDocument());
            return fullCollection.ToList();
        }

        public async Task<T> LoadRecordByIdAsync<T>(string table, Guid id)
        {
            var collection = _db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await collection.FindAsync(filter);

            return result.First();
        }
    }
}