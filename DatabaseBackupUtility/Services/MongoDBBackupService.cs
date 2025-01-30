using DatabaseBackupUtility.Models;
using DatabaseBackupUtility.Utilities;
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
			try
			{
				CommandExecutor.ExecuteCommand(command);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error during MongoDB backup: {ex.Message}");
				throw;
			}
		}
	}
}
