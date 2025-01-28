using Amazon.S3;
using Amazon.S3.Transfer;
using Azure.Storage.Blobs;
using DatabaseBackupUtility.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public class SQLiteBackupService : IDatabaseBackupService
	{
		private readonly IStorageService _storageService;

		public SQLiteBackupService(IStorageService storageService)
		{
			_storageService=storageService;
		}

		public void Backup(BackupOptions options)
		{
			string backupFilePath = Path.Combine(options.BackupPath, "backup.sqlite");
			
			_storageService.Store(options.ConnectionString, backupFilePath);

			if (options.Compress)
			{
				CompressBackup(backupFilePath);
			}

		}

		private void CompressBackup(string backupFilePath)
		{
			Console.WriteLine("Compressing backup file...");

			string compressedFilePath = backupFilePath + ".zip";

			try
			{
				using (var fileStream = new FileStream(compressedFilePath, FileMode.Create))
				using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true))
				{
					var zipArchiveEntry = archive.CreateEntryFromFile(backupFilePath, Path.GetFileName(backupFilePath));
				}

				Console.WriteLine("Backup successfully compressed.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error encountered when compressing backup file. Message:'{0}'", e.Message);
			}
		}
	}
}
