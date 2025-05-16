using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace CorebrainCS
{
    /// <summary>
    /// Creates the main corebrain interface.
    /// </summary>
    /// <param name="pythonPath">Path to the python which works with the corebrain cli, for example if you create the ./.venv you pass the path to the ./.venv python executable</param>
    /// <param name="scriptPath">Path to the corebrain cli script, if you installed it globally you just pass the `corebrain` path</param>
    /// <param name="verbose"></param>
    public class CorebrainCS(string pythonPath = "python", string scriptPath = "corebrain", bool verbose = false)
    {
        private readonly string _pythonPath = Path.GetFullPath(pythonPath);
        private readonly string _scriptPath = Path.GetFullPath(scriptPath);
        private readonly bool _verbose = verbose;


        public string Help()
        {
            return ExecuteCommand("--help");
        }

        public string Version()
        {
            return ExecuteCommand("--version");
        }

        public string Configure()
        {
            return ExecuteCommand("--configure");
        }

        public string ListConfigs()
        {
            return ExecuteCommand("--list-configs");
        }

        public string RemoveConfig()
        {
            return ExecuteCommand("--remove-config");
        }

        public string ShowSchema()
        {
            return ExecuteCommand("--show-schema");
        }

        public string ExtractSchema()
        {
            return ExecuteCommand("--extract-schema");
        }

        public string ExtractSchemaToDefaultFile()
        {
            return ExecuteCommand("--extract-schema --output-file test");
        }
        
        public string ConfigID()
        {
            return ExecuteCommand("--extract-schema --config-id config");
        }

        public string Token(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Token cannot be empty or null.");

            string arguments = $"--token {token}";

            return ExecuteCommand(arguments);
        }

        public string ApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("API Key cannot be empty or null.");

            string arguments = $"--api-key {apiKey}";

            return ExecuteCommand(arguments);
        }

        



        public string ExecuteCommand(string arguments)
        {
            if (_verbose) Console.WriteLine($"Executing: {_pythonPath} {_scriptPath} {arguments}");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _pythonPath,
                    Arguments = $"\"{_scriptPath}\" {arguments}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (_verbose)
            {
                Console.WriteLine("Command output:");
                Console.WriteLine(output);
                if (!string.IsNullOrEmpty(error)) Console.WriteLine("Error output:\n" + error);
            }

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception($"Python CLI error: {error}");
            }

            return output.Trim();
        }
    }
}