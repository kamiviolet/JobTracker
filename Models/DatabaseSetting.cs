namespace JobTracker.Models
{
    public class DatabaseSetting
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;
    }
}
