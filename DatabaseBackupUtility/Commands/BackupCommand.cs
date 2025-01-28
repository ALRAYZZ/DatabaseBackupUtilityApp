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
				StorageType.AzureBlobStorage => new AzureBlobStorageService(options.AzureBlobStorageConnectionString, options.AzureBlobStorageContainerName),
				StorageType.AWSS3 => new AWSS3StorageService(options.AwsS3BucketName),
				_ => throw new NotSupportedException($"Storage type {options.Storage} is not supported")
			};

			ICompressionService compressionService = new CompressionService();

			IDatabaseBackupService backupService = options.DbType switch
			{
				DatabaseType.MySQL => new MySQLBackupService(storageService, compressionService),
				DatabaseType.PostgreSQL => new PostgreSQLBackupService(storageService, compressionService),
				DatabaseType.MongoDB => new MongoDBBackupService(storageService, compressionService),
				DatabaseType.SQLite => new SQLiteBackupService(storageService, compressionService),
				_ => throw new NotSupportedException($"Database type {options.DbType} is not supported")
			};

			backupService.Backup(options);
		}
	}
}
