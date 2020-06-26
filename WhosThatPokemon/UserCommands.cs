using System;
using System.Windows.Forms;

namespace WhosThatPokemon
{
    public class UserCommands
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


        public static string UserSearchByPassword(string value)
        {
            Table = "users";
            Column = "password";
            ColumnValue = "user";
            Value = value;

            var userOrPasswordSearch = new DataManager();
            return userOrPasswordSearch.DataReader(Table, Column, ColumnValue, Value);
        }

        public static string UserSearchByUser(string value)
        {
            Table = "users";
            Column = "user";
            ColumnValue = "user";
            Value = value;

            var userOrPasswordSearch = new DataManager();
            return userOrPasswordSearch.DataReader(Table, Column, ColumnValue, Value);
        }

        public static string PasswordSearchByUser(string value)
        {
            Table = "users";
            Column = "user";
            ColumnValue = "password";
            Value = value;

            var userOrPasswordSearch = new DataManager().DataReader(Table, Column, ColumnValue, Value);

            return userOrPasswordSearch;
        }

        public static string PointSearchByUser(string value)
        {
            Table = "users";
            Column = "user";
            ColumnValue = "points";
            Value = value;

            var userOrPasswordSearch = new DataManager().DataReader(Table, Column, ColumnValue, Value);

            return userOrPasswordSearch;
        }

        //Writes user password and points
        public static string UserPasswordPointWriter(string value1, string value2, string value3)
        {
            Table = "users";
            Column1 = "user";
            Column2 = "password";
            Column3 = "points";
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;

            var userOrPasswordSearch = new DataManager();
            return userOrPasswordSearch.DataWriter(Table, Column1, Column2, Column3, Value1,Value2, Value3);
        }
        public static string NewUserWriter(string value)
        {
            //Checks if user exists already
            if (String.IsNullOrWhiteSpace(UserSearchByUser(value)))
            {
                Table = "users";
                ColumnValue = "user";
                Value = value;
            }
            else
            {
                MessageBox.Show("User already Exists");
            }
            var newUser = new DataManager();
            return newUser.DataWriter(Table, ColumnValue, Value);
        }

        //Adds new password by user
        public static string NewPasswordWriter(string value1, string value2)
        {
            Table = "users";
            Column1 = "user";
            Column2 = "password";
            Value1 = value1;
            Value2 = value2;

            var userOrPasswordSearch = new DataManager();
            return userOrPasswordSearch.DataWriter(Table, Column1, Column2, Value1, Value2);
        }

        //Adds new points by user
        public static string NewPointsWriter(string value1, string value2)
        {
            Table = "users";
            Column1 = "user";
            Column2 = "points";
            Value1 = value1;
            Value2 = value2;

            var userOrPasswordSearch = new DataManager();
            return userOrPasswordSearch.DataWriter(Table, Column1, Column2, Value1, Value2);
        }

        //User Remover
        public static string UserRemover(string value)
        {
            var userRemover = "";
            try
            {
                Table = "users";
                Column = "user";
                ColumnValue = "user";
                Value = UserSearchByUser(value);

                userRemover = new DataManager().DataRemover(Table, ColumnValue, Value);
            }
            catch (Exception e)
            {
                MessageBox.Show("No User Found" + e);
            }

            return userRemover;

        }
    }
}