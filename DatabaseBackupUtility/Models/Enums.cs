using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Models
{
	public enum DatabaseType
	{
		MySQL,
		PostgreSQL,
		MongoDB,
		SQLite
	}

	public enum StorageType
	{
		Local,
		AWSS3,
		AzureBlobStorage
	}

	public enum ScheduleType
	{
		Daily,
		Weekly,
		Monthly
	}
}
