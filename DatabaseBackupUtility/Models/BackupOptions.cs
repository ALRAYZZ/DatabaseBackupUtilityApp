using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Models
{
	public class BackupOptions
	{
		public DatabaseType DbType { get; set; }
		public string ConnectionString { get; set; }
		public string BackupPath { get; set; }
		public ScheduleType Schedule { get; set; }
		public bool Compress { get; set; }
		public StorageType Storage { get; set; }
		public string LogPath { get; set; }
		public string AwsS3BucketName { get; set; }
		public string AzureBlobStorageContainerName { get; set; }
		public string AzureBlobStorageConnectionString { get; set; }
	}
}
