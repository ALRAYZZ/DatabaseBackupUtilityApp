using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public class CompressionService : ICompressionService
	{
		public void Compress(string filePath)
		{
			Console.WriteLine("Compressing backup file...");

			string compressedFilePath = filePath + ".zip";

			try
			{
				using (var fileStream = new FileStream(compressedFilePath, FileMode.Create))
				using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create, true))
				{
					var zipArchiveEntry = archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
				}

				Console.WriteLine("Backup successfully compressed.");

				File.Delete(filePath);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error encountered when compressing backup file. Message:'{0}'", e.Message);
			}
		}
	}
}
