using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Utilities
{
	public static class CommandExecutor
	{
		public static int ExecuteCommand(string command)
		{
			var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
			{
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardError = true,
				RedirectStandardOutput = true
			};

			var process = new Process()
			{
				StartInfo = processInfo
			};

			process.OutputDataReceived += (sender, e) => Console.WriteLine("output>>" + e.Data);
			process.ErrorDataReceived += (sender, e) => Console.WriteLine("error>>" + e.Data);

			try
			{
				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();

				return process.ExitCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error executing command: {ex.Message}");
				return -1;
			}
			finally
			{
				process.Close();
			}
		}
	}
}
