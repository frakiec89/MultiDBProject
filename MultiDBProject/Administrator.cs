using Microsoft.Data.SqlClient;
using System;

namespace MultiDBProject
{
    public class Administrator
    {
        public static void BackupDatabase()
        {
            // read connectionstring from config file
            var connectionString = @"Server=192.168.10.148;Database=myDataBase;
                User Id=stud;Password=stud";

            var database = "myDataBase";

            var backupFolder = @"\\192.168.10.160\задание\";

            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")

            var backupFileName = String.Format("{0}{1}-{2}.bak",
                backupFolder, sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd _ hh_mm"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query = 
                    String.Format("BACKUP DATABASE [{0}] TO DISK = N'{1}'  WITH NOFORMAT, NOINIT, NAME = N'{0}-Полная База данных Резервное копирование', SKIP, NOREWIND, NOUNLOAD, STATS = 10" ,
                    database , backupFileName );

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public  static void RecoveryDatabase(string  path)
        {

            var connectionString = @"Server=192.168.10.148;Database=master;
                User Id=stud;Password=stud";
            var database = "myDataBase";

            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query =
                    String.Format(
                        "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE" +
                        " [{0}] FROM  DISK = N'{1}' WITH  FILE = 1,  NOUNLOAD, REPLACE,  STATS =5 " +
                        "ALTER DATABASE [{0}] SET MULTI_USER",
                    database, path
                    );
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

    }
}
