using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public interface ICompressionService
	{
		void Compress(string filePath);
	}
}
