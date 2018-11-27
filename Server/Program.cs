using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Games;
using Command;

namespace Server
{
    class MyClass
    {

    }
    class Program
    {
        void Fun1()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter w = new BinaryWriter(stream);
            BinaryReader r = new BinaryReader(stream);


            for (int i = 0; i < 256; i++)
            {
                w.Write("1");
            }



            Console.WriteLine("позиция-{0}", stream.Position);
            Console.WriteLine("длинна-{0}", stream.Length);
            //byte a = 10;
            //w.Write(a);
            stream.Position = 0;

            byte[] ar = stream.GetBuffer();
            Console.WriteLine(ar.Length);
            MemoryStream s = new MemoryStream(ar, 0, 256, true, true);
            BinaryReader r2 = new BinaryReader(s);

            try
            {
                Console.WriteLine(r2.ReadString());
                //r.ReadInt32();

                Console.WriteLine(s.Position);
                Console.WriteLine(s.ReadByte());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Image()
        {

            byte[] img = new byte[20];



            int imgLength = 0;

            byte[] buffer = new byte[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 };
            byte[] buffer2 = new byte[10] { 0, 111, 222, 123, 44, 55, 66, 7, 8, 9 };
            buffer.Take(5).ToArray().CopyTo(img, 0);

            foreach (byte item in img)
            {
                Console.WriteLine(item);
            }
            buffer2.Take(5).ToArray().CopyTo(img, 5);
            Console.WriteLine("1111111111");
            Console.WriteLine();
            Console.WriteLine();
            foreach (byte item in img)
            {
                Console.WriteLine(item);
            }
            Console.Read();


        }

        static void Error(string text)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = color;
            Console.ReadKey();
            Console.Clear();
        }

        static void CreateServer()
        {
            int port;
            while (true)
            {
                Console.WriteLine("Сreate server");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("Введите port сервера (диапазон от 3000 да 4000 )");
                try
                {
                    port = int.Parse(Console.ReadLine());
                    if (port >= 3000 && port < 4000)
                    {

                    }
                    else
                    {
                        Error("Недопустимый диапазон");
                        //Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                }
                catch
                {
                    Error("Неверный формат");
                    //Console.ReadKey();
                    Console.Clear();
                    continue;

                }

                Server server = new Server(port);
                if (MainServer.Start())
                {
                    return;
                }
                else
                {
                    Error("Не удалось создать сервер");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

        }

        static void Command()
        {
            while (true)
            {
                //ConsoleKey key = Console.ReadKey(true).Key;
                //if (ConsoleKey.Enter == key)
                //    OccasionHandler.print = false;

                string com = Console.ReadLine();
                com = com.ToLower();
                lock (OccasionHandler.blockObject)
                {
                    switch (com)
                    {
                        case "help":
                        case "/help":
                            {
                                Console.WriteLine(new string('-',80));
                                Console.WriteLine("Список команд:");
                                Console.WriteLine("Clear - очистить консоль");
                                Console.WriteLine("Exit - выход");
                                Console.WriteLine("Stop - отключить сервер");
                                Console.WriteLine("Inf - список информации по процессам");
                                Console.WriteLine("Report - список сообщений от пользователей");
                                Console.WriteLine("Client - информация о пользователях на сервере");
                                Console.WriteLine(new string('-', 80));
                                break;
                            }
                        case "client":
                            {
                                Console.WriteLine(new string('-', 80));
                                if(MainServer.Server.Clients.Count==0)
                                {
                                    Console.WriteLine("Пользователей на сервере нет");
                                }
                                else
                                {
                                    Console.WriteLine("Количество пользователей:{0}", MainServer.Server.Clients.Count);
                                    Console.WriteLine("Пользователи:");
                                    foreach (Client client in MainServer.Server.Clients)
                                    {
                                        Console.WriteLine(new string('-', 30));
                                        Console.WriteLine("User{0}:", MainServer.Server.Clients.IndexOf(client));
                                        if (client.Profile != null)
                                            Console.WriteLine("Login:{0}", client.Profile.Login);
                                        Console.WriteLine("IP-Port:{0}", client.Socket.RemoteEndPoint);

                                        Console.WriteLine(new string('-', 30));

                                    }
                                }
                                
                                Console.WriteLine(new string('-', 80));
                                break;

                            }
                        case "clear":
                        case "Clear":
                        case "/clear":
                        case "/Clear":
                            {
                                Console.Clear();
                                break;
                            }
                        case "exit":
                        case "Exit":
                            {
                                return;
                            }
                        case "Stop":
                        case "stop":
                            {
                                MainServer.StopToServer();
                                break;
                            }
                        case "":
                            {
                                OccasionHandler.print = !OccasionHandler.print;
                                break;
                            }
                        case "inf":
                        case "Inf":
                            {
                                ConsoleColor color = Console.ForegroundColor;
                                Console.WriteLine(new string('-', 80));

                                Console.ForegroundColor = OccasionHandler.ClientEventColor;
                                Console.WriteLine("ClientEvent- события происходящие при взаимодействии с пользователем");

                                Console.ForegroundColor = OccasionHandler.ErrorColor;
                                Console.WriteLine("Error - все обработанные исключения");

                                Console.ForegroundColor = OccasionHandler.ServerEventColor;
                                Console.WriteLine("ServerEvent - события происходящие на сервере");

                                Console.ForegroundColor = OccasionHandler.SendColor;
                                Console.WriteLine("Send- отправка команды пользователю");

                                Console.ForegroundColor = OccasionHandler.SQLColor;
                                Console.WriteLine("SQLEVENT - события происходящие при взаимодействии с БД");

                                Console.ForegroundColor = OccasionHandler.GameServerColor;
                                Console.WriteLine("GameServerEVENT - события на игровом сервере");

                                Console.ForegroundColor = OccasionHandler.GameColor;
                                Console.WriteLine("GameEvent - события процесса игры");
                                Console.ForegroundColor = color;
                                Console.WriteLine(new string('-', 80));
                                break;
                            }
                        case "Report":
                        case "report":
                            {
                                Console.WriteLine(new string('-',30));
                                Console.WriteLine("Type:");
                                Console.WriteLine("{0} - {1}",ReporType.другое, (int)ReporType.другое);
                                Console.WriteLine("{0} - {1}", ReporType.недоработка, (int)ReporType.недоработка);
                                Console.WriteLine("{0} - {1}", ReporType.правонарушение, (int)ReporType.правонарушение);
                                Console.WriteLine("{0} - {1}", ReporType.предложения, (int)ReporType.предложения);
                                Console.WriteLine("{0} - {1}", ReporType.сбой_программы, (int)ReporType.сбой_программы);
                                Console.WriteLine(new string('-', 30));
                                Console.WriteLine("Введите номер типа сообщения:");
                                int k = -1;
                                try
                                {
                                    k = int.Parse(Console.ReadLine());
                                }
                                catch
                                {

                                }
                                SQL.PrintRepor(k);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Такой команды нет");
                                break;
                            }
                    }

                }
            }
        }

        static void Main(string[] args)
        {
            //for (int i = 9600; i < 9700; i++)
            //{
            //    Console.Write((char)i+i.ToString());
            //}
            Console.Title = "Server";
            //Console.WriteLine(DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss"));


            CreateServer();
            Console.WriteLine("Далее" + (char)9658);
            Console.ReadKey(true);
            Console.Clear();

            Console.WriteLine("Подключение база данных...");
            SQL.Connection();
            Console.WriteLine("Далее" + (char)9658);
            Console.ReadKey(true);
            Console.Clear();

            Console.WriteLine("Сервер готов к использованию");
            Console.WriteLine("Port={0}", MainServer.Server.Port);
            Console.WriteLine(new string('-', 80));

            Command();
            SQL.Disconnected();
            MainServer.StopToServer();
        }
    }
}
