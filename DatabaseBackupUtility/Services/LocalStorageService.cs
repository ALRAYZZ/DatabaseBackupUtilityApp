using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	internal class LocalStorageService : IStorageService
	{
		public void Store(string sourceFilePath, string destinationPath)
		{
			Console.WriteLine($"Storing backup locally at {destinationPath}");
			File.Copy(sourceFilePath, destinationPath, true);
		}
	}
}
