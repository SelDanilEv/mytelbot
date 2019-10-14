using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace TelegramBotConsole
{
    class Data
    {
        public static string data_way = @"Data.txt";                   // количество вопросов // 

        public class Data_Base
        {
            public static void CreateFile()
            {
                using (StreamWriter sr = new StreamWriter(data_way, true, System.Text.Encoding.Default))
                {
                    sr.Close();
                }
            }

            public static void ReadFile()
            {
                victims.Clear();
                string str=" "; 
                using (StreamReader sr = new StreamReader(data_way, System.Text.Encoding.Default))
                {
                    while(str != null)
                    {
                        str = sr.ReadLine();
                        victims.Add(str);
                    }
                    victims.Remove(null);
                    sr.Close();
                }
            }

            public static void reWriteFile(string str)
            {
                using (StreamWriter sr = new StreamWriter(data_way))
                {
                    sr.Write(str);
                    sr.Close();
                }
            }
        }


        public static List<string> victims = new List<string> ();
        public static List<string> errors = new List<string> { "Что то не то", "Тебе походу очень плохо", "Вот сейчас совсем не понял", "А теперь на русском" };

    }
}
