using LK_Teacher.Modules.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LK_Teacher.Modules.Utility
{
    public static class DBApi
    {
        public const string ACTIVE_PROFILE = "active";

        public const string WAIT_PROFILE = "wait";

        public const string FIRST_TIME_PROFILE = "first_time";

        private static MySqlConnection connection = null;

        public static string EMAIL_TEACHER { get; set; }

        public static string ID_TEACHER { get; set; }

        public static string ConnectionString { get; private set; } = null;

        public static bool IsConnection
        {
            get
            {
                try
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message,"Проблемы с соединением",MessageBoxButton.OK,MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public static void InitializeApi(string conStr)
        {
            ConnectionString = conStr;
            connection = new MySqlConnection(ConnectionString);

        }

        //Insert query

        public static int AddNewEvent(string title, DateTime dateTimeEvent,string description,int typeEvent)
        {
            try
            {
                connection.Open();
                string query = $"INSERT INTO events (id_event ,title_event ,date_event ,description_event ,type_event,status_event,id_teacher) VALUES (  0 ,'{title}' , '{dateTimeEvent.ToString("yyyy-MM-dd H:mm:ss")}','{description}' ,{typeEvent}, true, '{ID_TEACHER}');";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT LAST_INSERT_ID();",connection);
                int result = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return result;
            }
            catch(MySqlException e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public static void Registration(string email, string login, string password, string fname, string lname, string mname, string phone_number)
        {
            login = UtilFunctions.ValidateSqlValue(login);
            password = UtilFunctions.ValidateSqlValue(password);
            fname = UtilFunctions.ValidateSqlValue(fname);
            lname = UtilFunctions.ValidateSqlValue(lname);
            mname = UtilFunctions.ValidateSqlValue(mname);

            connection.Open();
            string query = $"INSERT INTO teachers (id_teacher ,email_teacher ,login_teacher ,password_teacher ,fname_teacher,lname_teacher,mname_teacher,phone_number_teacher, status_profile_teacher) VALUES (  0 ,'{email}' , '{login}','{password}' ,'{fname}', '{lname}', '{mname}', '{phone_number}', 3);";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        //Select query

        internal static Hashtable Authorization(string email, string password)
        {
            var hashTable = new Hashtable();
            connection.Open();
            string query = $"SELECT * from teachers where email_teacher = '{email}' and password_teacher = '{password}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    hashTable.Add(reader.GetName(i), reader[i].ToString());
                }
            }
            reader.Close();
            connection.Close();
            return hashTable;
        }

        public static Hashtable GetEventWhereDate(DateTime dateTimeEvent)
        {
            var hashTable = new Hashtable();
            connection.Open();
            string query = $"SELECT * from events where date_event = '{dateTimeEvent.ToString("yyyy-MM-dd H:mm:ss")}' and id_teacher = '{ID_TEACHER}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                for (int i =0; i< reader.FieldCount;i++)
                {
                    hashTable.Add(reader.GetName(i),reader[i].ToString());
                }
            }
            reader.Close();
            connection.Close();
            return hashTable;
        }

        public static bool HasSameField(string nameTable, string nameColumn, string value)
        {
            value = UtilFunctions.ValidateSqlValue(value);
            bool result;
            connection.Open();
            string query = $"SELECT * from `{nameTable}` where `{nameColumn}` = '{value}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            if (reader.HasRows)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            reader.Close();
            connection.Close();
            return result;
        }

        public static Hashtable GetDataEvent(int idEvent)
        {
            var hashTable = new Hashtable();
            connection.Open();
            string query = $"SELECT * from events where id_event = {idEvent} and id_teacher = '{ID_TEACHER}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    hashTable.Add(reader.GetName(i), reader[i].ToString());
                }
            }
            reader.Close();
            connection.Close();
            return hashTable;
        }

        //Update query

        internal static void UpdateEvent(int idEvent, string titleEvent, DateTime dayOfEvent, string description, int type)
        {
            connection.Open();
            string query = $"UPDATE events set title_event = '{titleEvent}', description_event = '{description}', type_event = '{type}'  where  id_event = '{idEvent}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        internal static void UpdateStatusEvent(int idEvent, bool statusEvent)
        {
            connection.Open();
            string query = $"UPDATE events set status_event = {statusEvent} where  id_event = '{idEvent}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        //Delete query

        internal static void DeleteEvent(int idEvent)
        {
            connection.Open();
            string query = $"DELETE from events where  id_event = '{idEvent}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}
