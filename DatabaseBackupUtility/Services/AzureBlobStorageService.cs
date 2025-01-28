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
		private readonly string _connectionString;
		private readonly string _containerName;

		public AzureBlobStorageService(string connectionString, string containerName)
		{
			_connectionString=connectionString;
			_containerName=containerName;
		}

		public void Store(string sourceFilePath, string destinationPath)
		{
			Console.WriteLine("Storing backup in Azure Blob Storage...");

			var blobServiceClient = new BlobServiceClient(_connectionString);
			var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
			var blobClient = containerClient.GetBlobClient(destinationPath);

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
