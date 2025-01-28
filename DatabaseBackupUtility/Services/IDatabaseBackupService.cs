using DatabaseBackupUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public interface IDatabaseBackupService
	{
		void Backup(BackupOptions options);
	}
}
