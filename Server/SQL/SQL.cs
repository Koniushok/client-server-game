using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Games;
using System.IO;
using System.Data;
using Command;


namespace Server
{
    static class SQL
    {
        //static string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\DATA.mdf;Integrated Security=True";
       //static string connection=@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\валера\Desktop\курсов\Client\Server\Database1.mdf;Integrated Security = True";
        static string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Database1.mdf;Integrated Security=True ";

        static SqlConnection sqlConnection = new SqlConnection();
        static object Block = new object();
        static bool Status { get; set; } = false;


        public static void Connection()
        {
            try
            {
                sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();
                Status = true;
                OccasionHandler.SQL("База данных подключена");
            }
            catch (Exception e)
            {
                OccasionHandler.Error(e);
            }
        }

        public static void Disconnected()
        {
            sqlConnection.Close();
            Status = false;
            OccasionHandler.SQL("База данных отключена");
        }

        public static void Sendcommand(string command)
        {
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                OccasionHandler.Error(e);
            }
        }

        public static ClientProfile Authorization(string login, string password)
        {
            lock (Block)
            {
                ClientProfile profile = null;
                SqlDataReader reader = null;

                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command = string.Format("SELECT Login,Name,Surname FROM Users WHERE Login=N'{0}' and Password=N'{1}'", login, password);
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {

                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            login = reader.GetString(0);
                            string name = reader.GetString(1);
                            string surname = reader.GetString(2);
                            profile = new ClientProfile(login, name, surname);
                            OccasionHandler.SQL(string.Format("Успешная авторизация (Login:'{0}')", login));
                        }
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }
                }

                if (reader != null)
                    reader.Close();

                if (profile == null)
                {
                    OccasionHandler.SQL(string.Format("Попытка не успешной авторизоваться под логином:{0} и паролем:{1}", login, password));
                }

                return profile;
            }
        }

        public static bool Registration(string login, string password, string name, string surname)
        {
            lock (Block)
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    if (!GetProfile(login))
                    {

                        string command = string.Format("INSERT INTO Users(Login,Password,Name,Surname) values(N'{0}',N'{1}',N'{2}',N'{3}')", login, password, name, surname);
                        SqlCommand cmd = new SqlCommand(command, sqlConnection);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            OccasionHandler.SQL(string.Format("Успешная Регистрация в базе данных(Login:'{0}')", login));
                            return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                OccasionHandler.SQL(string.Format("Не успешная Регистрация  в базе данных(Login:'{0}')", login));
                return false;
            }
        }

        public static GameTask GetTask(int number, TypeTask type,Game g)
        {
            lock (Block)
            {
                GameTask task = null;
                SqlDataReader reader = null;
                string command = "";
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    if (type == TypeTask.Answer)
                    {
                        command = string.Format("SELECT TOP({0}) Question,CorrectAnswer,Answer1,Answer2,Answer3,Answer4,id FROM AnswerTask ", number);
                        SqlCommand cmd = new SqlCommand(command, sqlConnection);
                        try
                        {

                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                
                                string question = reader.GetString(0);
                                int correctAnswer = reader.GetInt32(1);
                                string answer1 = reader.GetString(2);
                                string answer2 = reader.GetString(3);
                                string answer3 = reader.GetString(4);
                                string answer4 = reader.GetString(5);
                                int id = reader.GetInt32(6);
                                g.idTask = id;
                                task = new TaskAnswer(question, correctAnswer, answer1, answer2, answer3, answer4);
                                OccasionHandler.SQL(string.Format("Выборка вопроса из БД"));
                            }
                        }
                        catch (Exception e)
                        {
                            OccasionHandler.Error(e);
                        }
                    }
                    else
                    {
                        command = string.Format("SELECT TOP({0}) Question,CorrectAnswer FROM NumericTask ", number);
                        SqlCommand cmd = new SqlCommand(command, sqlConnection);
                        try
                        {
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                string question = reader.GetString(0);
                                int correctAnswer = reader.GetInt32(1);
                                task = new NumericTask(question, correctAnswer);
                                OccasionHandler.SQL(string.Format("Выборка вопроса из БД"));
                            }
                        }
                        catch (Exception ex)
                        {
                            OccasionHandler.Error(ex);
                        }
                    }


                }

                if (reader != null)
                    reader.Close();

                return task;
            }
        }

        public static bool GetProfile(string login)
        {
            lock (Block)
            {
                ClientProfile profile = null;
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command = string.Format("SELECT Login FROM Users WHERE Login=N'{0}'", login);
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {

                            if (reader != null)
                                reader.Close();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }
                }

                if (reader != null)
                    reader.Close();

                return false;
            }
        }

        public static bool UpData(string login, string password, string name, string surname, bool PasswordTest)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command;
                    if (PasswordTest)
                    {
                        command = string.Format("UPDATE Users SET Password=N'{1}',Name=N'{2}',Surname=N'{3}' WHERE Login=N'{0}'", login, password, name, surname);
                        Console.WriteLine(command);
                    }
                    else
                    {
                        command = string.Format("UPDATE Users SET Name=N'{2}',Surname=N'{3}' WHERE Login=N'{0}'", login, password, name, surname);
                        Console.WriteLine(command);
                    }

                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Данные о пользователе обновлены в  БД"));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();

                return false;
            }
        }

        public static bool NewAnswerTask(TaskAnswer task)
        {
            lock (Block)
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string question = task.Question;
                    int correctAnswer = task.CorrectAnswer;
                    string answer1 = task.Answers[0];
                    string answer2 = task.Answers[1];
                    string answer3 = task.Answers[2];
                    string answer4 = task.Answers[3];
                    string command = string.Format("INSERT INTO AnswerTask(Question,CorrectAnswer,Answer1,Answer2,Answer3,Answer4) values(N'{0}',{1},N'{2}',N'{3}',N'{4}',N'{5}')", question, correctAnswer, answer1, answer2, answer3, answer4);
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Добавлен новый вопрос в БД"));
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return false;
            }
        }

        public static bool NewNumericeTask(NumericTask task)
        {
            lock (Block)
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string question = task.Question;
                    int correctAnswer = task.CorrectAnswer;
                    string command = string.Format("INSERT INTO AnswerTask(Question,CorrectAnswer) values(N'{0}',{1})", question, correctAnswer);
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Добавлен новый вопрос в БД"));
                        return true;
                    }
                    catch (Exception e)
                    {
                        OccasionHandler.Error(e);
                    }
                }
                return false;
            }
        }

        public static int NumberOfTask(TypeTask type)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                int k = 0;
                string command = "";
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {

                    if (type == TypeTask.Answer)
                    {
                        command = string.Format("SELECT COUNT(*) FROM AnswerTask ");
                        SqlCommand cmd = new SqlCommand(command, sqlConnection);
                        try
                        {
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                k = reader.GetInt32(0);
                                OccasionHandler.SQL(string.Format("Просмотр количества вопрос"));
                            }
                        }
                        catch (Exception e)
                        {
                            OccasionHandler.Error(e);
                        }
                    }
                    else
                    {
                        command = string.Format("SELECT COUNT(*) FROM FROM NumericTask ");
                        SqlCommand cmd = new SqlCommand(command, sqlConnection);
                        try
                        {
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                k = reader.GetInt32(1);
                                OccasionHandler.SQL(string.Format("Выборка вопроса из БД"));
                            }
                        }
                        catch (Exception ex)
                        {
                            OccasionHandler.Error(ex);
                        }
                    }


                }

                if (reader != null)
                    reader.Close();

                return k;
            }
        }

        public static void AddImage(string login, byte[] array)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command = string.Format("UPDATE Users SET Image=@Photo WHERE Login=N'{0}'", login);
                    //string command = string.Format("INSERT INTO Users(Login,Password,Name,Surname,Image) values('{0}','{1}','{2}','{3}',@Photo)","val3211223", "123", "123","456");

                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    SqlParameter par = new SqlParameter("@Photo", array);
                    par.SqlDbType = SqlDbType.Image;
                    cmd.Parameters.Add(par);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Добавлено изображение в БД"));
                        return;
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();

                return;
            }
        }

        public static byte[] GetImage(string login)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                byte[] imgData = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command = string.Format("SELECT Image FROM Users WHERE Login=N'{0}'", login);
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (!(reader[0] is System.DBNull))
                                imgData = (byte[])reader[0];
                            OccasionHandler.SQL(string.Format("Выгрузка изображения из БД (Login:'{0}')", login));
                        }
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }
                }

                if (reader != null)
                    reader.Close();



                return imgData;
            }
        }

        #region Статистика
        public static void AddNumGame(string login)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command;

                    command = string.Format("UPDATE Users SET NumGame=NumGame+1 WHERE Login=N'{0}'", login);

                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Данные о пользователе обновлены в  БД"));
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();
            }

        }

        public static void AddWinGame(string login)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command;

                    command = string.Format("UPDATE Users SET WinGame=WinGame+1 WHERE Login=N'{0}'", login);

                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Данные о пользователе обновлены в  БД"));
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();
            }

        }

        public static void AddLeaveGame(string login)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command;

                    command = string.Format("UPDATE Users SET LeaveGame=LeaveGame+1 WHERE Login=N'{0}'", login);

                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Данные о пользователе обновлены в  БД"));
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();
            }

        }

        public static void AddNumAnswersGame(string login, int num)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command;

                    command = string.Format("UPDATE Users SET NumAnswers=NumAnswers+{1} WHERE Login=N'{0}'", login, num);


                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Данные о пользователе обновлены в  БД"));
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();
            }

        }

        public static void AddCorrectAnswersGame(string login, int num)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command;

                    command = string.Format("UPDATE Users SET CorrectAnswer=CorrectAnswer+{1} WHERE Login=N'{0}'", login, num);

                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Данные о пользователе обновлены в  БД"));
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();
            }

        }

        public static Statistics GetStatistics(string login)
        {
            lock (Block)
            {
                Statistics stat = null;
                SqlDataReader reader = null;

                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command = string.Format("SELECT NumGame,WinGame,LeaveGame,NumAnswers,CorrectAnswer,Name,Surname FROM Users WHERE Login=N'{0}'", login);
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {
                            reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int NumGame = reader.GetInt32(0);
                            int WinGame = reader.GetInt32(1);
                            int LeaveGame = reader.GetInt32(2);
                            int NumAnswers = reader.GetInt32(3);
                            int CorrectAnswer = reader.GetInt32(4);
                            string Name = reader.GetString(5);
                            string SurName = reader.GetString(6);
                            stat = new Statistics(NumGame, WinGame, LeaveGame, NumAnswers, CorrectAnswer, Name, SurName, login);
                            OccasionHandler.SQL(string.Format("Извлечения статистики (Login:'{0}')", login));
                        }
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }
                }

                if (reader != null)
                    reader.Close();


                return stat;
            }
        }
        #endregion

        public static void AddRepor(ReporType type,string message,string login)
        {
            lock (Block)
            {
                SqlDataReader reader = null;
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command = string.Format("INSERT INTO  Report(message,type,login) values(N'{0}',{1},N'{2}')", message,(int)type,login);
                    
                    
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                   
                    try
                    {
                        cmd.ExecuteNonQuery();
                        OccasionHandler.SQL(string.Format("Пользователь {0} добавил информация для разработчиков в БД",login));
                        return;
                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }

                }

                if (reader != null)
                    reader.Close();

                return;
            }
        }

        public static void PrintRepor(int k)
        {
            lock (Block)
            {
                SqlDataReader reader = null;

                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    string command = string.Format("SELECT Id,message,type,login FROM Report");
                    SqlCommand cmd = new SqlCommand(command, sqlConnection);
                    try
                    {

                        reader = cmd.ExecuteReader();
                        OccasionHandler.SQL("Вывод информации от пользователей");
                        Console.WriteLine(new string('-', 60));
                        while (reader.Read())
                        {
                            
                            int id = reader.GetInt32(0);
                            string message = reader.GetString(1);
                            int type = reader.GetInt32(2);
                            string login = reader.GetString(3);
                            if (type == k)
                            {
                                Console.WriteLine("Report:");
                                Console.WriteLine(new string('-', 20));
                                Console.WriteLine("ID:{0}", id);
                                Console.WriteLine("Message:{0}", message);
                                Console.WriteLine("Type:{0}", (ReporType)type);
                                Console.WriteLine("Login:{0}", login);
                                Console.WriteLine(new string('-', 20));
                                Console.WriteLine();
                            }

                        }
                        Console.WriteLine(new string('-', 60));


                    }
                    catch (Exception ex)
                    {
                        OccasionHandler.Error(ex);
                    }
                }

                if (reader != null)
                    reader.Close();            
            }
        }
    }

}
