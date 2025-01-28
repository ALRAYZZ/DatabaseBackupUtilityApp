using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public interface IStorageService
	{
		void Store(string sourceFilePath, string backupFilePath);
	}
}
