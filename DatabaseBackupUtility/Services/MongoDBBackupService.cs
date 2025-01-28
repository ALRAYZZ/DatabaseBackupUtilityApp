using DatabaseBackupUtility.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public class MongoDBBackupService : BaseBackupService
	{
		public MongoDBBackupService(IStorageService storageService, ICompressionService compressionService)
			: base(storageService, compressionService)
		{
		}

		protected override string GetBackupFilePath(BackupOptions options)
		{
			return Path.Combine(options.BackupPath, "backup");
		}

		protected override void PerformBackup(string connectionString, string backupFilePath)
		{
			Console.WriteLine($"Performing backup for MongoDB database from {connectionString} to {backupFilePath}");
			// Create the backup directory if it doesn't exist
			Directory.CreateDirectory(backupFilePath);
			// Construct the mongodump command
			string command = $"mongodump --uri=\'{connectionString}\' --out=\'{backupFilePath}\'";

			// Execute the command
			ExecuteCommand(command);
		}

		private void ExecuteCommand(string command)
		{
			var processInfo = new ProcessStartInfo("cmd.exe", "/c" + command)
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

			process.Start();
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
			process.WaitForExit();
			process.Close();
		}
	}
}
