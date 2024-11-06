
namespace TechChallenge.API.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly CustomLoggerProviderProviderConfiguration _loggerConfig;
        public static bool IsFile { get; set; } = false;
        public CustomLogger(string loggerName, CustomLoggerProviderProviderConfiguration loggerConfig)
        {
            _loggerName = loggerName;
            _loggerConfig = loggerConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;        
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"Log de execução: {logLevel} : {eventId} : {formatter(state, exception)}";
            if (IsFile)
            {
                WriteTextFile(message);
            }
            else
                Console.WriteLine(message);

        }

        private void WriteTextFile(string message)
        {
            string path = Environment.CurrentDirectory + $@"\LOG-{DateTime.Now:yyyy-MM-dd}.txt";

            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path: Path.GetDirectoryName(path));
                File.Create(path).Dispose();
            }

            using StreamWriter writer = new(path, true);
            writer.WriteLine(message);
            writer.Close();
        }
    }
}
