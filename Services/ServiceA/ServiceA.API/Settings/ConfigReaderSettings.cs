namespace ServiceA.API.Settings
{
    public class ConfigReaderSettings:IConfigReaderSettings
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public int RefreshTimerIntervalInMs { get; set; }
    }
}
