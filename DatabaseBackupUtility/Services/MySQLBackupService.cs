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
	public class MySQLBackupService : BaseBackupService
	{
		public MySQLBackupService(IStorageService storageService, ICompressionService compressionService)
			: base(storageService, compressionService)
		{
		}

		protected override string GetBackupFilePath(BackupOptions options)
		{
			return Path.Combine(options.BackupPath, "backup.sql");
		}

		protected override void PerformBackup(string connectionString, string backaupFilePath)
		{
			Console.WriteLine($"Performing bakcup for MySQL database from {connectionString} to {backaupFilePath}");

			Directory.CreateDirectory(Path.GetDirectoryName(backaupFilePath));

			string command = $"mysqldump -u {connectionString} --result-file=\'{backaupFilePath}\'";


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
