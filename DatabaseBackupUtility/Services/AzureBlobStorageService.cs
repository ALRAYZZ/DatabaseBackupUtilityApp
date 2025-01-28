using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public class AzureBlobStorageService : IStorageService
	{
		public void Store(string sourceFilePath, string backupFilePath)
		{
			Console.WriteLine("Storing backup in Azure Blob Storage...");

			var blobServiceClient = new BlobServiceClient("connectionString");
			var containerClient = blobServiceClient.GetBlobContainerClient("my-container");
			var blobClient = containerClient.GetBlobClient(Path.GetFileName(backupFilePath));

			try
			{
				using var fileStream = File.OpenRead(sourceFilePath);
				blobClient.Upload(fileStream, true);
				Console.WriteLine("Backup successfully uploaded to Azure Blob Storage.");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error encountered when uploading to Azure Blob Storage. Message: '{0}'", e.Message);
			}
		}
	}
}
