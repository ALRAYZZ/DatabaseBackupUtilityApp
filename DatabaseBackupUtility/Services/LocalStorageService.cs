using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	internal class LocalStorageService : IStorageService
	{
		public void Store(string sourceFilePath, string backupFilePath)
		{
			Console.WriteLine($"Storing backup locally at {backupFilePath}");
			File.Copy(sourceFilePath, backupFilePath, true);
		}
	}
}
