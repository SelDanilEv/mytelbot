using System;
using System.Collections.Generic;
using System.Text;

using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBotConsole
{
    class Bot
    {
        public int State { get; set; } = 0; // 0- начало 
        public string inputMessage;
        public string outputMessage;
        public MessageEventArgs namebot;

        public Bot(MessageEventArgs chatid)
        {
            namebot = chatid;
        }

        public static Random rand = new Random();
        public List<int> vict = new List<int>();

        public int x = 0;

        public void prepareToAdd()
        {
            int y;
            if (int.TryParse(inputMessage, out y))
            {
                AddNewVict(y);
            }
            else
            {
                HelpsMethods.sendError(this.namebot);
            }
        }

        public void AddNewVict(int y)
        {
            if (x + y > Data.victims.Count)
                HelpsMethods.sendError(this.namebot);
            else
            {
                int i = 0;
                while (i != y)
                {
                    int r = rand.Next(0, Data.victims.Count);
                    bool flag = true;
                    foreach (int g in vict)
                    {
                        if (g == r) flag = false;
                    }
                    if (flag)
                    {
                        vict.Add(r);
                        i++;
                    }
                }
                outputMessage = "Молодые:\n";
                for (int w = 0; w < y; w++)
                {
                    outputMessage += Data.victims[vict[x + w]] + '\n';
                }
                x += y;
                if (x != Data.victims.Count)
                {
                    HelpsMethods.sendMess(outputMessage + "\nБудем добивать(Да/Нет)", this.namebot);
                    State = 2;
                }
                else
                {
                    outputMessage += "\nУже всех перебрали))";
                    HelpsMethods.sendMess(outputMessage, this.namebot);
                    this.x = 0;
                    this.vict.Clear();
                    State = 0;
                }
                vict.Sort();
            }
        }

        public int getCode(string str)
        {
            int x = 0;
            foreach (char ch in str)
            {
                x += (int)ch;
            }
            Console.WriteLine(x);
            return x;
        }

        public void analyze()
        {
            Console.WriteLine($"Analyze ON *************** {State} \n");
            if (inputMessage == "/home")
            {
                HelpsMethods.sendMess("И мы снова дома", this.namebot);
                State = 0;
            }
            else
                switch (State)
                {
                    case -3:
                        Data.Data_Base.reWriteFile(inputMessage);
                        HelpsMethods.sendMess("Будет сделано\n" +
                                                  "1-показать группу\n" +
                                                  "2-изменить группу\n" +
                                                  "3-изменить приоритет\n", this.namebot);
                        Data.Data_Base.ReadFile();
                        State = -2;
                        break;
                    case -2:
                        switch (inputMessage)
                        {
                            case "1":
                                outputMessage = null;
                                foreach (string victim in Data.victims)
                                    outputMessage += victim + '\n';
                                HelpsMethods.sendMess(outputMessage, this.namebot);
                                break;
                            case "2":
                                outputMessage = null;
                                foreach (string victim in Data.victims)
                                    outputMessage += victim + '\n';
                                HelpsMethods.sendMess("Скопируй и отошли  мне как надо", this.namebot);
                                HelpsMethods.sendMess(outputMessage, this.namebot);
                                State = -3;
                                break;
                            case "3":
                                break;
                            default:
                                HelpsMethods.sendError(this.namebot);
                                break;
                        }
                        break;
                    case -1:
                        if (getCode(inputMessage) == 496)
                        {
                            HelpsMethods.sendMess("Привет Админушка\n" +
                                                  "1-показать группу\n" +
                                                  "2-изменить группу\n" +
                                                  "3-изменить приоритет\n",
                                                  this.namebot
                                                  );
                            State = -2;
                        }
                        else
                        {
                            HelpsMethods.sendError(this.namebot);
                        }

                        break;
                    case 0:
                        switch (inputMessage)
                        {
                            case "/admin":
                                HelpsMethods.sendMess("А ну ка пароль!", this.namebot);
                                State = -1;
                                break;
                            default:
                                outputMessage = $"Всего жертв: {Data.victims.Count}" + "\nСкольких будем отчислять?";
                                HelpsMethods.sendMess(outputMessage, this.namebot);
                                State = 1;
                                break;
                        }

                        break;
                    case 1:
                        prepareToAdd();
                        break;
                    case 2:
                        switch (inputMessage)
                        {
                            case "Да":
                                outputMessage = "Мне нравятся твои мысли! \nСколько еще будем душить?" + "\nОсталось бедных: " + (Data.victims.Count - x).ToString();
                                HelpsMethods.sendMess(outputMessage, this.namebot);

                                State = 1;
                                break;
                            case "Нет":
                                HelpsMethods.sendMess("Ну ок", this.namebot);
                                this.vict.Clear();
                                this.x = 0;
                                State = 0;
                                break;
                            default:
                                HelpsMethods.sendError(this.namebot);
                                break;
                        }
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
        }
    }
}
