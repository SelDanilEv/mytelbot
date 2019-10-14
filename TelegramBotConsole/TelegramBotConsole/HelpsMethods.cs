using System;
using System.Collections.Generic;
using System.Text;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBotConsole
{
    class HelpsMethods
    {
        public static Random rand = new Random();

        public static async void sendMess(string text, MessageEventArgs chid)
        {
            await Program.botClient.SendTextMessageAsync(chatId: chid.Message.Chat, text: text.ToString()).ConfigureAwait(false);
        }

        public static void sendError(MessageEventArgs chid)
        {
            sendMess(Data.errors[rand.Next(0, Data.errors.Count)], chid);
        }

        public static List<MessageEventArgs> messageEventArgs = new List<MessageEventArgs>();
        public static List<long> chatid = new List<long>();
        public static List<Bot> bots = new List<Bot>();

        public static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("================");
            Console.WriteLine(sender);
            Console.WriteLine(e.Message.Chat.Id);
            Console.WriteLine("================");

            if (!chatid.Contains(e.Message.Chat.Id))
            {
                chatid.Add(e.Message.Chat.Id);
                messageEventArgs.Add(e);
                Bot bot = new Bot(e);
                bots.Add(bot);
            }
            Bot currBot=new Bot(e);
            foreach (Bot b in bots)
                if (b.namebot.Message.Chat.Id == e.Message.Chat.Id)
                    currBot = b;
            currBot.inputMessage = (e.Message.Text).ToString();
            Console.WriteLine("Receive " + currBot.inputMessage + " \n***************\n");
            currBot.analyze();
        }
    }
}
