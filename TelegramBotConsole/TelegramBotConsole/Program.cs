using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBotConsole
{
    class Program
    {
        public static ITelegramBotClient botClient;
        public static int state = 0;

        static void Main(string[] args)
        {
            botClient = new TelegramBotClient("943201413:AAEz6N02wYjZz5_axJuuvFTM902Z-frfEKc") { Timeout = TimeSpan.FromSeconds(20) };
            Data.Data_Base.CreateFile();
            Data.Data_Base.ReadFile();


            var me = botClient.GetMeAsync().Result;

            Console.WriteLine($"Bot id: {me.Id} . Bot Name: {me.FirstName}");

            botClient.OnMessage += HelpsMethods.Bot_OnMessage;
            botClient.StartReceiving();

            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }

    }
}
