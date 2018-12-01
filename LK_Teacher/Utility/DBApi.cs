using LK_Teacher.Modules.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace LK_Teacher.Modules.Utility
{
    public static class DBApi
    {
        private static MySqlConnection connection = null;

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
                    MessageBox.Show(e.Message, "Проблемы с соединением", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public static int AddNewEvent(string title, DateTime dateTimeEvent, string description, int typeEvent)
        {
            try
            {
                connection.Open();
                string query = $"INSERT INTO events (id_event ,title_event ,date_event ,description_event ,type_event,status_event,id_teacher) VALUES (  0 ,'{title}' , '{dateTimeEvent.ToString("yyyy-MM-dd H:mm:ss")}','{description}' ,{typeEvent}, true, '{UserModel.IdTeacher}');";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command = new MySqlCommand("SELECT LAST_INSERT_ID();", connection);
                int result = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return result;
            }
            catch (MySqlException e)
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

        public static Hashtable Authorization(string email, string password)
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
            string query = $"SELECT * from events where date_event = '{dateTimeEvent.ToString("yyyy-MM-dd H:mm:ss")}' and id_teacher = '{UserModel.IdTeacher}';";
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
            string query = $"SELECT * from events where id_event = {idEvent} and id_teacher = '{UserModel.IdTeacher}';";
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

        public static List<Hashtable> GetSubjects(int id_teacher)
        {
            var list = new List<Hashtable>();
            Hashtable hashtable;

            connection.Open();
            string query = $"SELECT s.id_subject, s.name_subject FROM lk_teachers.subjects s INNER JOIN lk_teachers.subjects_directions sd ON s.id_subject = sd.id_subject INNER JOIN lk_teachers.directions d ON d.id_direction = sd.id_direction INNER JOIN lk_teachers.teachers_directions td ON td.id_direction = d.id_direction INNER JOIN lk_teachers.teachers t ON t.id_teacher = td.id_teacher WHERE t.id_teacher =  { id_teacher} GROUP BY s.id_subject;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                hashtable = new Hashtable();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    hashtable.Add(reader.GetName(i), reader[i].ToString());
                }
                list.Add(hashtable);
            }
            reader.Close();
            connection.Close();
            return list;
        }

        public static List<Hashtable> GetSubjects(int id_teacher, int id_direction)
        {
            var list = new List<Hashtable>();
            Hashtable hashtable;

            connection.Open();
            string query = $"SELECT s.id_subject FROM lk_teachers.subjects s  INNER JOIN lk_teachers.subjects_directions sd ON s.id_subject = sd.id_subject  INNER JOIN lk_teachers.directions d ON sd.id_direction = d.id_direction  INNER JOIN lk_teachers.teachers_directions td ON d.id_direction = td.id_direction  INNER JOIN lk_teachers.teachers t ON td.id_teacher = t.id_teacher  WHERE t.id_teacher = {id_teacher} AND sd.id_direction = {id_direction};";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                hashtable = new Hashtable();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    hashtable.Add(reader.GetName(i), reader[i].ToString());
                }
                list.Add(hashtable);
            }
            reader.Close();
            connection.Close();
            return list;
        }

        public static List<Hashtable> GetDirections()
        {
            var list = new List<Hashtable>();
            Hashtable hashtable;

            connection.Open();
            string query = $"SELECT d.id_direction, d.name_direction FROM directions d;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                hashtable = new Hashtable();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    hashtable.Add(reader.GetName(i), reader[i].ToString());
                }
                list.Add(hashtable);
            }
            reader.Close();
            connection.Close();
            return list;
        }

        public static Hashtable GetTeacherData(int id_teacher)
        {
            var hashTable = new Hashtable();
            connection.Open();
            string query = $"SELECT  id_teacher, email_teacher, login_teacher, fname_teacher, lname_teacher, mname_teacher, phone_number_teacher, status_profile_teacher, image_profile_teacher, date_birth_teacher, quote_teacher, education_teacher  from teachers where id_teacher =  {id_teacher};";
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

        public static string GetDirectionName(int id_direction)
        {
            string result = "";
            connection.Open();
            string query = $"SELECT name_direction from directions where id_direction = {id_direction};";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result = reader[i].ToString();
                }
            }
            reader.Close();
            connection.Close();
            return result;
        }

        public static bool HasSubject(int id_teacher, int id_subject)
        {
            bool result = false;
            connection.Open();
            string query = $"SELECT * from teachers_subjects where id_teacher = {id_teacher} and id_subject = {id_subject};";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                result = true;
            }
            reader.Close();
            connection.Close();
            return result;

        }

        public static bool HasDirection(int id_teacher, int id_direction)
        {
            bool result = false;
            connection.Open();
            string query = $"SELECT * from teachers_directions where id_teacher = {id_teacher} and id_direction = {id_direction};";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                result = true;
            }
            reader.Close();
            connection.Close();
            return result;

        }

        //Universal query

        public static void SetUnsetDirection(int id_teacher, int id_direction, bool SetMode)
        {
            connection.Open();
            string query = "";
            if (SetMode)
            {
                query = $"INSERT INTO teachers_directions (id_teacher, id_direction) values ({id_teacher}, {id_direction});";
            }
            else
            {
                query = $"DELETE FROM teachers_directions WHERE id_teacher = {id_teacher} AND id_direction = {id_direction};";
            }

            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void SetUnsetSubject(int id_teacher, int id_subject, bool SetMode)
        {
            connection.Open();
            string query = "";
            if (SetMode)
            {
                query = $"INSERT INTO teachers_subjects (id_teacher, id_subject) values ({id_teacher}, {id_subject});";
            }
            else
            {
                query = $"DELETE FROM teachers_subjects WHERE id_teacher = {id_teacher} AND id_subject = {id_subject};";
            }

            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        //Update query

        public static void UpdateEvent(int idEvent, string titleEvent, DateTime dayOfEvent, string description, int type)
        {
            connection.Open();
            string query = $"UPDATE events set title_event = '{titleEvent}', description_event = '{description}', type_event = '{type}'  where  id_event = '{idEvent}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateStatusEvent(int idEvent, bool statusEvent)
        {
            connection.Open();
            string query = $"UPDATE events set status_event = {statusEvent} where  id_event = '{idEvent}';";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateTeacherProfile(int id_teacher, string fname, string lname, string mname, string phone_number, DateTime date_birth, string education, string quote)
        {
            connection.Open();
            string query = $"UPDATE teachers SET fname_teacher = '{fname}'  ,lname_teacher = '{lname}'  ,mname_teacher = '{mname}' ,phone_number_teacher = '{phone_number}'  ,status_profile_teacher = ('{UserModel.ACTIVE_PROFILE}')  ,date_birth_teacher = '{date_birth.ToString("yyyy-MM-dd")}' ,education_teacher = '{education}', quote_teacher = '{quote}' WHERE  id_teacher = {id_teacher};";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateImage(string name_image, int id_teacher)
        {
            connection.Open();
            string query = $"UPDATE teachers SET image_profile_teacher = '{name_image}' where id_teacher = {id_teacher};";
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

        public static void DeleteSubject(int id_teacher, int id_subject)
        {
            connection.Open();
            string query = $"DELETE from teachers_subjects where  id_teacher = {id_teacher} and id_subject = {id_subject};";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
}
