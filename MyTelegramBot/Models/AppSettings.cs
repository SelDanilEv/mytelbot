using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTelegramBot.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; }  = "https://telegrambotapp.azurewebsites.net:443/{0}";

        public static string Name { get; set; } = "starostatyt_bot";

        public static string Key { get; set; }  = "943201413:AAG74EaBbpxdTiYl98H_LNGRSnPrUFdm1sM";

    }
}