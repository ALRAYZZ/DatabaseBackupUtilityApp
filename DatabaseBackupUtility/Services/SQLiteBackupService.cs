using Amazon.S3;
using Amazon.S3.Transfer;
using Azure.Storage.Blobs;
using DatabaseBackupUtility.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public class SQLiteBackupService : BaseBackupService
	{
		public SQLiteBackupService(IStorageService storageService, ICompressionService compressionService)
			: base(storageService, compressionService)
		{
		}

		protected override string GetBackupFilePath(BackupOptions options)
		{
			return Path.Combine(options.BackupPath, "backup.sqlite");
		}

		protected override void PerformBackup(string sourceFilePath, string backupFilePath)
		{
			Console.WriteLine($"Performing backup for SQLite database from {sourceFilePath} to {backupFilePath}");
			File.Copy(sourceFilePath, backupFilePath, true);
		}
	}
}
