namespace My_Weather_App.Source.Utils
{
    using System;
    using System.IO;

    public static class DotEnv        
    {
        // Loads the environment variables from a .env file
        // Adapted from https://dusted.codes/dotenv-in-dotnet
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                // skip comments
                if (line.StartsWith("#"))
                    continue;
                
                // split the line on the = sign
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                // malformed line
                if (parts.Length != 2)
                    continue;

                // get our key value pair
                var key = parts[0].Trim();
                var value = parts[1].Trim();

                // set the environment variable
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}