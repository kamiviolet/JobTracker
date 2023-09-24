using JobTracker.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace JobTracker.Services
{
    public class JobTrackerService
    {
        public readonly IMongoCollection<JobEntry> _jobsCollection;

        public JobTrackerService(IOptions<DatabaseSetting> jobTrackerDatabaseSetting)
        {
            var mongoClient = new MongoClient(jobTrackerDatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(jobTrackerDatabaseSetting.Value.DatabaseName);
            _jobsCollection = mongoDatabase.GetCollection<JobEntry>(jobTrackerDatabaseSetting.Value.CollectionName);
        }

        public async Task<List<JobEntry>> GetAllEntries() => 
            await _jobsCollection.Find(_ => true).ToListAsync();
        public async Task<JobEntry?> GetEntryById(string id) => 
            await _jobsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<List<JobEntry>> GetEntryByLocation(string location) => 
            await _jobsCollection.Find(x => x.Location == location).ToListAsync();

        public async Task CreateEntry(JobEntry newJob) => 
            await _jobsCollection.InsertOneAsync(newJob);
        public async Task UpdateEntry(string id, JobEntry updatedJob) => 
            await _jobsCollection.ReplaceOneAsync(x => x.Id == id, updatedJob);
        public async Task RemoveEntry(string id) => 
            await _jobsCollection.DeleteOneAsync(x => x.Id == id);
    } 
}
