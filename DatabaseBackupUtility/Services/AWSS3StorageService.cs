﻿using Amazon.S3.Transfer;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public class AWSS3StorageService : IStorageService
	{
		public void Store(string sourceFilePath, string backupFilePath)
		{
			Console.WriteLine("Storing backup in AWS S3...");

			var s3Client = new AmazonS3Client();
			var fileTransferUtility = new TransferUtility(s3Client);

			try
			{
				var fileTransferUtilityRequest = new TransferUtilityUploadRequest
				{
					FilePath = sourceFilePath,
					BucketName = "my-bucket",
					Key = Path.GetFileName(backupFilePath)
				};

				fileTransferUtility.Upload(fileTransferUtilityRequest);
				Console.WriteLine("Backup successfully uploaded to AWS S3.");
			}
			catch (AmazonS3Exception e)
			{
				Console.WriteLine("Error encountered on server. Message: '{0}' when writing a object", e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Unknown encountered on server. Message: '{0}' when writing an object", e.Message);
			}
		}
	}
}
