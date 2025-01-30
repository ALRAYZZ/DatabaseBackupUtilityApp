using DatabaseBackupUtility.Commands;
using DatabaseBackupUtility.Models;
using System.CommandLine;
using System.CommandLine.Invocation;



class Program
{
	static async Task<int> Main(string[] args)
	{
		var rootCommand = new RootCommand("Database Backup Utility");

		var dbTypeOption = new Option<DatabaseType>(
			"--dbtype",
			"The type of the database (e.g., MySQL, PostgreSQL, MongoDB, SQLite)");

		var connectionStringOption = new Option<string>(
			"--connection-string",
			"The connection string to the database");

		var backupPathOption = new Option<string>(
			"--backup-path",
			"The path where the backup will be stored");

		var scheduleOption = new Option<ScheduleType>(
			"--schedule",
			"The schedule for automatic backups (e.g., daily, weekly, monthly)");

		var compressOption = new Option<bool>(
			"--compress",
			"Whether to compress the backup files");

		var storageOption = new Option<StorageType>(
			"--storage",
			"The storage service to use (e.g., local, AWS S3, Azure Blob Storage)");

		var logPathOption = new Option<string>(
			"--log-path",
			"The path where the log file will be stored");


		rootCommand.AddOption(dbTypeOption);
		rootCommand.AddOption(connectionStringOption);
		rootCommand.AddOption(backupPathOption);
		rootCommand.AddOption(scheduleOption);
		rootCommand.AddOption(compressOption);
		rootCommand.AddOption(storageOption);
		rootCommand.AddOption(logPathOption);

		rootCommand.SetHandler((DatabaseType dbtype, string connectionString, string backupPath, ScheduleType schedule, bool compress,StorageType storage,string logPath ) =>
		{
			// Here we are creating an instance of BackupOptions and passing the values from the command line arguments
			var options = new BackupOptions
			{
				DbType = dbtype,
				ConnectionString = connectionString,
				BackupPath = backupPath,
				Schedule = schedule,
				Compress = compress,
				Storage = storage,
				LogPath = logPath
			};
			BackupCommand.HandleBackup(options);
		},
		dbTypeOption, connectionStringOption, backupPathOption, scheduleOption, compressOption, storageOption, logPathOption);

		return await rootCommand.InvokeAsync(args);
	}
}