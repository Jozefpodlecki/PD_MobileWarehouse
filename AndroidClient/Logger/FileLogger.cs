
using System;
using System.IO;

namespace Client.Logger
{
    public class FileLogger
    {
        public string _logFilePath;

        public FileLogger()
        {
            var directoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var logFileName = "log-" + DateTime.Now.ToString("yyyy-MM-dd-HH:mm.txt");
            
            _logFilePath = Path.Combine(directoryPath, logFileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(_logFilePath))
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine("Starting logging at " + DateTime.Now.ToString());
                }
            }
        }

        public void Log(string message)
        {
            using (var writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(DateTime.Now.ToString() + " : " + message);
            }
        }
    }
}