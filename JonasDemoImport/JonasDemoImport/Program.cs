using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace JonasDemoImport
{
	class Program
	{

		const int TableColumnNumber = 102;

		//Its header will be used as column name to created the Merged table
		const string TableColumnFromCsv = "MERGED2014_15_PP.csv";

		static void Main(string[] args)
		{
			try
			{
				var connectionString = GetConnectionString();

				try
				{
					Console.WriteLine("CSV importing job is started. Please wait for up to 2 minutes...");

					CreateTablesForImport(connectionString);
					InsertDataFromCSVsToDB(connectionString);

					Console.WriteLine("Importing CSV files to the Database is successful.");

				}
				catch(FileNotFoundException exc)
				{
					Console.WriteLine(exc.Message);
				}
				catch(SqlException exc)
				{
					Console.WriteLine(exc.Message);
				}
			}
			catch (InvalidOperationException exc)
			{
				Console.WriteLine(exc.Message);
			}
			catch(Exception exc)
			{
				Console.WriteLine("Other exception:" + exc.Message);
			}
		}
		

		public static string GetConnectionString()
		{
			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appSettings.json");

			var configuration = builder.Build();

			var connectionString = configuration.GetConnectionString("JonasDatabase");

			if (String.IsNullOrWhiteSpace(connectionString))
				throw new InvalidOperationException("Cannot retrieve Connection String from the configure file. Please make sure the connection string named: JonasDatabase in the appSettings.json file.");

			return connectionString;
		}

		public static string GetDataFolder()
		{
			var exePath = Path.GetDirectoryName(System.Reflection
							  .Assembly.GetExecutingAssembly().CodeBase);
			Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
			var appRoot = appPathMatcher.Match(exePath).Value;
			return appRoot + @"\data\";
		}


		public static void CreateTablesForImport(string connectionString)
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();

				//Drop 2 tables if they alread exist, then create the Merged table for csv inport, create MergedUniques table by copy the schema of the Merged table.
				//MergedUniques is the production table for multiple csv files import. The records are unique on the privary key UNITID and it will be used by the API demo.
				var sqlStatement =   @"DROP TABLE IF EXISTS dbo.Merged;   
									 DROP TABLE IF EXISTS dbo.MergedUniques;";
				using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
				{
					cmd.ExecuteNonQuery();
				}

				//Create the Merged table for the csv import using the header of MERGED2014_15_PP.csv in the data folder, and the first column is primary key.
				var csvFile = GetDataFolder() + TableColumnFromCsv;
				
				using (var reader = new StreamReader(csvFile))
				{
					var line = reader.ReadLine();

					var header = line.Split(',');


					sqlStatement = "CREATE TABLE dbo.Merged( ";

					for(int i = 0; i < TableColumnNumber; i++)
					{
						if (i == 0)
							sqlStatement += header[i] + " VARCHAR(255) NOT NULL PRIMARY KEY, ";
						else
							sqlStatement += header[i] + " VARCHAR(255), ";
					}

					//remove last , and then complete the sqlStatement

					sqlStatement = sqlStatement.Substring(0, sqlStatement.Length - 1) + ");";
				}

				using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
				{
					cmd.ExecuteNonQuery();
				}

				//Create MergedUniques table using the schema of Merged table. MergedUniques will save all unique inported records on the UNITID column
				sqlStatement = @"SELECT * INTO dbo.MergedUniques FROM dbo.Merged WHERE 1 = 2;
								ALTER TABLE dbo.MergedUniques ADD CONSTRAINT PK_MergedUniques PRIMARY KEY(UNITID);";


				using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		public static void InsertDataFromCSVsToDB(string connectionString)
		{

			var dataFolder = GetDataFolder();
			var csvFiles = Directory.GetFiles(dataFolder);

			foreach (var file in csvFiles)
				InsertOneCSV(connectionString, file);
		}


		public static void InsertOneCSV(string connectionString, string filePath)
		{
			Console.WriteLine($"Importing the csv file: {filePath}");

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();


				var sqlStatement = "TRUNCATE TABLE [dbo].[Merged]";
				using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
				{
					cmd.ExecuteNonQuery();
				}


				using (var reader = new StreamReader(filePath))
				{
					//Skip the firstline which is the Header Line...
					var line = reader.ReadLine();

					var header = line.Split(',');

					sqlStatement = @"INSERT INTO Merged (";

					for (int i = 0; i < TableColumnNumber; i++)
						sqlStatement += header[i] + ",";
					sqlStatement = sqlStatement.Substring(0, sqlStatement.Length - 1) + ") VALUES(";
					for (int i = 0; i < TableColumnNumber; i++)
						sqlStatement += "@" + header[i] + ",";
					sqlStatement = sqlStatement.Substring(0, sqlStatement.Length - 1) + ");";


					while (!reader.EndOfStream)
					{
						line = reader.ReadLine();
						var data = line.Split(',');

						using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
						{

							for (int i = 0; i < TableColumnNumber; i++)
								cmd.Parameters.AddWithValue("@" + header[i], data[i]);
								
							cmd.ExecuteNonQuery();
						}
					}					 
				}

				//insert data from Merged table to MergedUniques table for those records whose UNITID are not in the MergedUniques table
				sqlStatement = "INSERT INTO dbo.MergedUniques SELECT * FROM dbo.Merged WHERE UNITID NOT IN (SELECT UNITID FROM dbo.MergedUniques)";
				using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
				{
					cmd.ExecuteNonQuery();
				}

			}
		}
	}
}
