using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace WhosThatPokemon
{
    public class DataActions
    {
        public static string Table { get; set; }
        public static string Column { get; set; }
        public static string Column1 { get; set; }
        public static string Column2 { get; set; }
        public static string Column3 { get; set; }

        public static string ColumnValue { get; set; }
        public static string Value { get; set; }
        public static string Value1 { get; set; }
        public static string Value2 { get; set; }
        public static string Value3 { get; set; }

        public static string Message1 { get; set; }

        public string SqlReturn { get; set; }

        public static SQLiteConnection Conn { get; set; }

        public static SQLiteCommand SqLiteCmd { get; set; }
        public SQLiteDataReader SqLiteDataReader { get; set; }

        //public static SQLiteConnection DataInteraction()
        //{
        //    Conn = SQLAccessor.CreateConnection();
        //    return Conn;
        //}
        public static void DataInputs(string table)
        {
            Table = table;
        }
        public static void DataInputs(string table, string columnValue, string value)
        {
            Table = table;
            ColumnValue = columnValue;
            Value = value;
            Conn = SQLAccessor.CreateConnection();
            SqLiteCmd = Conn.CreateCommand();
        }

        public static void DataInputs(string table, string column, string columnValue, string value)
        {
            Table = table;
            Column = column;
            ColumnValue = columnValue;
            Value = value;
            Conn = SQLAccessor.CreateConnection();
            SqLiteCmd = Conn.CreateCommand();
        }

        public static void DataInputs(string table, string column1, string column2, string value1, string value2)
        {
            Table = table;
            Column1 = column1;
            Column2 = column2;
            Value1 = value1;
            Value2 = value2;
            Conn = SQLAccessor.CreateConnection();
            SqLiteCmd = Conn.CreateCommand();
        }
        public static void DataInputs(string table, string column1, string column2, string column3, string value1, string value2, string value3)
        {
            Table = table;
            Column1 = column1;
            Column2 = column2;
            Column3 = column3;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Conn = SQLAccessor.CreateConnection();
            SqLiteCmd = Conn.CreateCommand();
        }
    }

    class DataManager : DataActions
    {
        public string DataReader(string table, string column, string columnValue, string value)
        {
            DataInputs(table, column, columnValue, value);


            SqLiteCmd.CommandText = "SELECT " + ColumnValue + " FROM " + "'" + Table + "'" + " WHERE " + Column + " = " + "'" + Value + "'";

            SqLiteDataReader = SqLiteCmd.ExecuteReader();
            while (SqLiteDataReader.Read())
            {
                SqlReturn = SqLiteDataReader.GetString(0);
            }

            Conn.Close();
            //MessageBox.Show(SqlReturn);
            return SqlReturn;
        }

        public string RandomReader()
        {
            DataInputs("randomPokemon", "NULL", "pokemonNumber", "NULL");


            SqLiteCmd.CommandText = "SELECT " + ColumnValue + " FROM " + "'" + Table + "'";

            SqLiteDataReader = SqLiteCmd.ExecuteReader();
            while (SqLiteDataReader.Read())
            {
                SqlReturn = SqLiteDataReader.GetString(0);
            }

            Conn.Close();
            return SqlReturn;
        }

        public string DataWriter(string table, string columnValue, string value)
        {
            //RandomPokemon Writing
            if (table == "randomPokemon" && columnValue == "pokemonNumber")
            {
                DataInputs(table, "NULL", columnValue, value);
                SqLiteCmd.CommandText = @"INSERT INTO " + Table + "(" + ColumnValue + ") VALUES(" + "'" + Value + "'" + ")";

                return Convert.ToString(SqLiteCmd.ExecuteNonQuery());
            }

            //General Inserting into Table
            DataInputs(table, "NULL", columnValue, value);
            SqLiteCmd.CommandText = @"INSERT INTO " + Table + "(" + ColumnValue + ") VALUES(" + "'" + Value + "'" + ")";

            return Convert.ToString(SqLiteCmd.ExecuteNonQuery());
        }

        //User-Password or User-Points
        public string DataWriter(string table, string column1, string column2, string value1, string value2)
        {

            DataInputs(table, column1, column2,  value1, value2);
            SqLiteCmd.CommandText = @"UPDATE " + Table + " SET " + Column2 + " = " + "'" + Value2 + "'" + " WHERE " + Column1 + " = " + "'" + Value1 + "'";

            return Convert.ToString(SqLiteCmd.ExecuteNonQuery());

        }

        //User-Password-Points
        public string DataWriter(string table, string column1, string column2, string column3, string value1, string value2, string value3)
        {
            DataInputs(table, column1, column2,column3, value1,value2,value3);
            SqLiteCmd.CommandText = @"INSERT INTO " + Table + "(" + Column1 + "," + Column2 + "," + Column3 + ") VALUES(" + "'" + Value1 + "'" + "," + "'" + Value2 + "'" + "," + "'" + Value3 + "'" + ")";

            return Convert.ToString(SqLiteCmd.ExecuteNonQuery());
        }

        public string DataRemover(string table, string columnValue, string value)
        {
            DataInputs(table, columnValue, value);

            //var sqliteCmd = DataInteraction().CreateCommand();

            SqLiteCmd.CommandText = @"DELETE FROM " + Table + " WHERE " + ColumnValue + " = " + "'" + Value + "'";

            //SqLiteCmd.ExecuteNonQuery();
            return Convert.ToString(SqLiteCmd.ExecuteNonQuery());
        }
        public string DataRemover(string table)
        {
            DataInputs(table);

            //var sqliteCmd = DataInteraction().CreateCommand();

            var command = @"DELETE FROM " + Table;
            if (SqLiteCmd != null)
            {

                try
                {
                    SqLiteCmd.CommandText = command;
                    Convert.ToString(SqLiteCmd.ExecuteNonQuery());

                }
                catch (Exception e)
                {
                    Console.WriteLine("Random Pokemon Database was never opened" + e);
                    //throw;
                }

                Message1 = "All Random Pokemon have been deleted";
                Console.WriteLine(Message1);
            }

            else
            {
                //SqLiteCmd.ExecuteNonQuery();
                Message1 = "No Random Pokemon found";
                Console.WriteLine(Message1);
            }

            return Message1;
        }
    }
}