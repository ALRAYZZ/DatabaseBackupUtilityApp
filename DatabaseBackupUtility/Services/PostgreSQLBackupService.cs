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
	public class PostgreSQLBackupService : BaseBackupService
	{
		public PostgreSQLBackupService(IStorageService storageService, ICompressionService compressionService)
			: base(storageService, compressionService) 
		{
		}
		protected override string GetBackupFilePath(BackupOptions options)
		{
			return Path.Combine(options.BackupPath, "backup.sql");
		}

		protected override void PerformBackup(string connectionString, string backupFilePath)
		{
			Console.WriteLine($"Performing backup for PostgreSQL database from {connectionString} to {backupFilePath}");

			Directory.CreateDirectory(Path.GetDirectoryName(backupFilePath));

			string command = $"pg_dump {connectionString} -f \'{backupFilePath}'";

			try
			{
				CommandExecutor.ExecuteCommand(command);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error during MySQL backup: {ex.Message}");
				throw;
			}
		}
	}
}
