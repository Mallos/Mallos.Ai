namespace Mallos.Insight.Nancy
{
    class AppConfiguration : IAppConfiguration
    {
        public Logging Logging { get; set; }
    }
    
    class LogLevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }

    class Logging
    {
        public bool IncludeScopes { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}