using DatabaseBackupUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public abstract class BaseBackupService : IDatabaseBackupService
	{
		private readonly IStorageService _storageService;
		private readonly ICompressionService _compressionService;

		protected BaseBackupService(IStorageService storageService, ICompressionService compressionService)
		{
			_storageService=storageService;
			_compressionService=compressionService;
		}

		public void Backup(BackupOptions options)
		{
			string backupFilePath = GetBackupFilePath(options);
			Console.WriteLine($"Backcup file path: {backupFilePath}");

			PerformBackup(options.ConnectionString, backupFilePath);
			Console.WriteLine("Backup performed successfully");

			_storageService.Store(backupFilePath, GetStorageDestinationPath(options, backupFilePath));
			Console.WriteLine("Backup stored successfully");


			if (options.Compress)
			{
				_compressionService.Compress(backupFilePath);
			}
		}

		protected virtual string GetStorageDestinationPath(BackupOptions options, string backupFilePath)
		{
			return Path.GetFileName(backupFilePath);
		}

		protected abstract string GetBackupFilePath(BackupOptions options);
		protected abstract void PerformBackup(string connectionString, string backupFilePath);

	}
}
