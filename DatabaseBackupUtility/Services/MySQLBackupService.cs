﻿using DatabaseBackupUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackupUtility.Services
{
	public class MySQLBackupService : IDatabaseBackupService
	{
		public void Backup(BackupOptions options)
		{
			Console.WriteLine("Performing backup for MySQL...");
			throw new NotImplementedException();
		}
	}
}
