using DatabaseBackupUtility.Models;
using DatabaseBackupUtility.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Commands
{
	public static class BackupCommand
	{
		public static void HandleBackup(BackupOptions options)
		{
			Console.WriteLine("Backup command executed");

			IStorageService storageService = options.Storage switch
			{
				StorageType.Local => new LocalStorageService(),
				StorageType.AzureBlobStorage => new AzureBlobStorageService(),
				StorageType.AWSS3 => new AWSS3StorageService(),
				_ => throw new NotSupportedException($"Storage type {options.Storage} is not supported")
			};



			IDatabaseBackupService backupService = options.DbType switch
			{
				DatabaseType.MySQL => new MySQLBackupService(),
				DatabaseType.PostgreSQL => new PostgreSQLBackupService(),
				DatabaseType.MongoDB => new MongoDBBackupService(),
				DatabaseType.SQLite => new SQLiteBackupService(storageService),
				_ => throw new NotSupportedException($"Database type {options.DbType} is not supported")
			};

			backupService.Backup(options);
		}
	}
}
