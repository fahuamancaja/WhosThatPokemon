using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WhosThatPokemon
{
    public class SQLAccessor
    {
        public static SQLiteConnection CreateConnection()
        {
            //var directory = AppDomain.CurrentDomain.BaseDirectory;
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var rawPath = directory + @"\database\pokemon.db";
            var realPath = rawPath.Replace(@"\\", @"\");
            var dataSource = "Data Source=" + realPath +"; Version = 3; New = True; Compress = True; ";
            var sqliteConnection =
                new SQLiteConnection(
                    dataSource);

            try
            {
                sqliteConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry unable to open" + Convert.ToString(ex));
            }

            return sqliteConnection;
        }
    }
}