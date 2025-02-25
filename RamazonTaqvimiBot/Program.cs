using DataBaseConnection;
using DataBaseConnection.Entity;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace RamazonTaqvimiBot
{
    public class Program
    {
        private static string BotToken = "7599176140:AAHdMMEniaJB7z4LK2bgs0xaqTju7p1Ukpg";

        private static TelegramBotClient BotClient = new TelegramBotClient(BotToken);

        private static readonly MainContext _mainContext = new MainContext();

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Toshkent = new()
{
    { 1, (new TimeOnly(5, 40), new TimeOnly(18, 15)) },
    { 2, (new TimeOnly(5, 38), new TimeOnly(18, 17)) },
    { 3, (new TimeOnly(5, 37), new TimeOnly(18, 18)) },
    { 4, (new TimeOnly(5, 35), new TimeOnly(18, 19)) },
    { 5, (new TimeOnly(5, 33), new TimeOnly(18, 20)) },
    { 6, (new TimeOnly(5, 32), new TimeOnly(18, 21)) },
    { 7, (new TimeOnly(5, 30), new TimeOnly(18, 22)) },
    { 8, (new TimeOnly(5, 29), new TimeOnly(18, 24)) },
    { 9, (new TimeOnly(5, 27), new TimeOnly(18, 25)) },
    { 10, (new TimeOnly(5, 25), new TimeOnly(18, 26)) },
    { 11, (new TimeOnly(5, 24), new TimeOnly(18, 27)) },
    { 12, (new TimeOnly(5, 22), new TimeOnly(18, 28)) },
    { 13, (new TimeOnly(5, 20), new TimeOnly(18, 29)) },
    { 14, (new TimeOnly(5, 18), new TimeOnly(18, 30)) },
    { 15, (new TimeOnly(5, 17), new TimeOnly(18, 31)) },
    { 16, (new TimeOnly(5, 15), new TimeOnly(18, 32)) },
    { 17, (new TimeOnly(5, 13), new TimeOnly(18, 34)) },
    { 18, (new TimeOnly(5, 11), new TimeOnly(18, 35)) },
    { 19, (new TimeOnly(5, 10), new TimeOnly(18, 36)) },
    { 20, (new TimeOnly(5, 8), new TimeOnly(18, 37)) },
    { 21, (new TimeOnly(5, 6), new TimeOnly(18, 38)) },
    { 22, (new TimeOnly(5, 4), new TimeOnly(18, 39)) },
    { 23, (new TimeOnly(5, 2), new TimeOnly(18, 40)) },
    { 24, (new TimeOnly(5, 1), new TimeOnly(18, 41)) },
    { 25, (new TimeOnly(4, 59), new TimeOnly(18, 42)) },
    { 26, (new TimeOnly(4, 57), new TimeOnly(18, 43)) },
    { 27, (new TimeOnly(4, 55), new TimeOnly(18, 44)) },
    { 28, (new TimeOnly(4, 53), new TimeOnly(18, 46)) },
    { 29, (new TimeOnly(4, 52), new TimeOnly(18, 47)) },
    { 30, (new TimeOnly(4, 50), new TimeOnly(18, 48)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Namangan = new()
{
    { 1, (new TimeOnly(5, 30), new TimeOnly(18, 6)) },
    { 2, (new TimeOnly(5, 28), new TimeOnly(18, 8)) },
    { 3, (new TimeOnly(5, 27), new TimeOnly(18, 9)) },
    { 4, (new TimeOnly(5, 25), new TimeOnly(18, 10)) },
    { 5, (new TimeOnly(5, 23), new TimeOnly(18, 11)) },
    { 6, (new TimeOnly(5, 22), new TimeOnly(18, 12)) },
    { 7, (new TimeOnly(5, 20), new TimeOnly(18, 13)) },
    { 8, (new TimeOnly(5, 19), new TimeOnly(18, 15)) },
    { 9, (new TimeOnly(5, 17), new TimeOnly(18, 16)) },
    { 10, (new TimeOnly(5, 16), new TimeOnly(18, 17)) },
    { 11, (new TimeOnly(5, 15), new TimeOnly(18, 18)) },
    { 12, (new TimeOnly(5, 13), new TimeOnly(18, 19)) },
    { 13, (new TimeOnly(5, 11), new TimeOnly(18, 20)) },
    { 14, (new TimeOnly(5, 9), new TimeOnly(18, 21)) },
    { 15, (new TimeOnly(5, 8), new TimeOnly(18, 22)) },
    { 16, (new TimeOnly(5, 6), new TimeOnly(18, 23)) },
    { 17, (new TimeOnly(5, 4), new TimeOnly(18, 25)) },
    { 18, (new TimeOnly(5, 2), new TimeOnly(18, 26)) },
    { 19, (new TimeOnly(5, 1), new TimeOnly(18, 27)) },
    { 20, (new TimeOnly(4, 59), new TimeOnly(18, 28)) },
    { 21, (new TimeOnly(4, 57), new TimeOnly(18, 29)) },
    { 22, (new TimeOnly(4, 55), new TimeOnly(18, 30)) },
    { 23, (new TimeOnly(4, 53), new TimeOnly(18, 31)) },
    { 24, (new TimeOnly(4, 52), new TimeOnly(18, 32)) },
    { 25, (new TimeOnly(4, 50), new TimeOnly(18, 33)) },
    { 26, (new TimeOnly(4, 48), new TimeOnly(18, 34)) },
    { 27, (new TimeOnly(4, 46), new TimeOnly(18, 35)) },
    { 28, (new TimeOnly(4, 44), new TimeOnly(18, 37)) },
    { 29, (new TimeOnly(4, 43), new TimeOnly(18, 38)) },
    { 30, (new TimeOnly(4, 41), new TimeOnly(18, 38)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Jizzax = new()
{
    { 1, (new TimeOnly(5, 46), new TimeOnly(18, 22)) },
    { 2, (new TimeOnly(5, 44), new TimeOnly(18, 24)) },
    { 3, (new TimeOnly(5, 43), new TimeOnly(18, 25)) },
    { 4, (new TimeOnly(5, 41), new TimeOnly(18, 26)) },
    { 5, (new TimeOnly(5, 39), new TimeOnly(18, 27)) },
    { 6, (new TimeOnly(5, 38), new TimeOnly(18, 28)) },
    { 7, (new TimeOnly(5, 36), new TimeOnly(18, 29)) },
    { 8, (new TimeOnly(5, 35), new TimeOnly(18, 31)) },
    { 9, (new TimeOnly(5, 33), new TimeOnly(18, 32)) },
    { 10, (new TimeOnly(5, 31), new TimeOnly(18, 32)) },
    { 11, (new TimeOnly(5, 30), new TimeOnly(18, 33)) },
    { 12, (new TimeOnly(5, 28), new TimeOnly(18, 34)) },
    { 13, (new TimeOnly(5, 26), new TimeOnly(18, 35)) },
    { 14, (new TimeOnly(5, 24), new TimeOnly(18, 36)) },
    { 15, (new TimeOnly(5, 23), new TimeOnly(18, 37)) },
    { 16, (new TimeOnly(5, 21), new TimeOnly(18, 38)) },
    { 17, (new TimeOnly(5, 19), new TimeOnly(18, 40)) },
    { 18, (new TimeOnly(5, 17), new TimeOnly(18, 41)) },
    { 19, (new TimeOnly(5, 16), new TimeOnly(18, 42)) },
    { 20, (new TimeOnly(5, 16), new TimeOnly(18, 43)) },
    { 21, (new TimeOnly(5, 14), new TimeOnly(18, 44)) },
    { 22, (new TimeOnly(5, 12), new TimeOnly(18, 45)) },
    { 23, (new TimeOnly(5, 10), new TimeOnly(18, 46)) },
    { 24, (new TimeOnly(5, 9), new TimeOnly(18, 47)) },
    { 25, (new TimeOnly(5, 7), new TimeOnly(18, 48)) },
    { 26, (new TimeOnly(5, 5), new TimeOnly(18, 49)) },
    { 27, (new TimeOnly(5, 3), new TimeOnly(18, 50)) },
    { 28, (new TimeOnly(5, 1), new TimeOnly(18, 52)) },
    { 29, (new TimeOnly(5, 0), new TimeOnly(18, 53)) },
    { 30, (new TimeOnly(4, 58), new TimeOnly(18, 53)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Andijon = new()
{
    { 1, (new TimeOnly(5, 28), new TimeOnly(18, 3)) },
    { 2, (new TimeOnly(5, 26), new TimeOnly(18, 5)) },
    { 3, (new TimeOnly(5, 25), new TimeOnly(18, 6)) },
    { 4, (new TimeOnly(5, 23), new TimeOnly(18, 7)) },
    { 5, (new TimeOnly(5, 21), new TimeOnly(18, 8)) },
    { 6, (new TimeOnly(5, 20), new TimeOnly(18, 9)) },
    { 7, (new TimeOnly(5, 18), new TimeOnly(18, 10)) },
    { 8, (new TimeOnly(5, 17), new TimeOnly(18, 12)) },
    { 9, (new TimeOnly(5, 15), new TimeOnly(18, 13)) },
    { 10, (new TimeOnly(5, 13), new TimeOnly(18, 14)) },
    { 11, (new TimeOnly(5, 12), new TimeOnly(18, 15)) },
    { 12, (new TimeOnly(5, 10), new TimeOnly(18, 16)) },
    { 13, (new TimeOnly(5, 8), new TimeOnly(18, 17)) },
    { 14, (new TimeOnly(5, 6), new TimeOnly(18, 18)) },
    { 15, (new TimeOnly(5, 5), new TimeOnly(18, 19)) },
    { 16, (new TimeOnly(5, 3), new TimeOnly(18, 20)) },
    { 17, (new TimeOnly(5, 1), new TimeOnly(18, 22)) },
    { 18, (new TimeOnly(4, 59), new TimeOnly(18, 23)) },
    { 19, (new TimeOnly(4, 58), new TimeOnly(18, 24)) },
    { 20, (new TimeOnly(4, 57), new TimeOnly(18, 25)) },
    { 21, (new TimeOnly(4, 55), new TimeOnly(18, 26)) },
    { 22, (new TimeOnly(4, 53), new TimeOnly(18, 27)) },
    { 23, (new TimeOnly(4, 51), new TimeOnly(18, 28)) },
    { 24, (new TimeOnly(4, 50), new TimeOnly(18, 29)) },
    { 25, (new TimeOnly(4, 48), new TimeOnly(18, 30)) },
    { 26, (new TimeOnly(4, 46), new TimeOnly(18, 31)) },
    { 27, (new TimeOnly(4, 44), new TimeOnly(18, 32)) },
    { 28, (new TimeOnly(4, 42), new TimeOnly(18, 34)) },
    { 29, (new TimeOnly(4, 41), new TimeOnly(18, 35)) },
    { 30, (new TimeOnly(4, 39), new TimeOnly(18, 35)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Fargona = new()
{
    { 1, (new TimeOnly(5, 30), new TimeOnly(18, 5)) },
    { 2, (new TimeOnly(5, 28), new TimeOnly(18, 7)) },
    { 3, (new TimeOnly(5, 27), new TimeOnly(18, 8)) },
    { 4, (new TimeOnly(5, 25), new TimeOnly(18, 9)) },
    { 5, (new TimeOnly(5, 23), new TimeOnly(18, 10)) },
    { 6, (new TimeOnly(5, 22), new TimeOnly(18, 11)) },
    { 7, (new TimeOnly(5, 20), new TimeOnly(18, 12)) },
    { 8, (new TimeOnly(5, 19), new TimeOnly(18, 14)) },
    { 9, (new TimeOnly(5, 17), new TimeOnly(18, 15)) },
    { 10, (new TimeOnly(5, 16), new TimeOnly(18, 16)) },
    { 11, (new TimeOnly(5, 15), new TimeOnly(18, 17)) },
    { 12, (new TimeOnly(5, 13), new TimeOnly(18, 18)) },
    { 13, (new TimeOnly(5, 11), new TimeOnly(18, 19)) },
    { 14, (new TimeOnly(5, 9), new TimeOnly(18, 20)) },
    { 15, (new TimeOnly(5, 8), new TimeOnly(18, 21)) },
    { 16, (new TimeOnly(5, 6), new TimeOnly(18, 22)) },
    { 17, (new TimeOnly(5, 4), new TimeOnly(18, 24)) },
    { 18, (new TimeOnly(5, 2), new TimeOnly(18, 25)) },
    { 19, (new TimeOnly(5, 1), new TimeOnly(18, 26)) },
    { 20, (new TimeOnly(5, 0), new TimeOnly(18, 27)) },
    { 21, (new TimeOnly(4, 58), new TimeOnly(18, 28)) },
    { 22, (new TimeOnly(4, 56), new TimeOnly(18, 29)) },
    { 23, (new TimeOnly(4, 54), new TimeOnly(18, 30)) },
    { 24, (new TimeOnly(4, 53), new TimeOnly(18, 31)) },
    { 25, (new TimeOnly(4, 51), new TimeOnly(18, 32)) },
    { 26, (new TimeOnly(4, 49), new TimeOnly(18, 33)) },
    { 27, (new TimeOnly(4, 47), new TimeOnly(18, 34)) },
    { 28, (new TimeOnly(4, 45), new TimeOnly(18, 36)) },
    { 29, (new TimeOnly(4, 44), new TimeOnly(18, 37)) },
    { 30, (new TimeOnly(4, 42), new TimeOnly(18, 37)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Sirdaryo = new()
{
    { 1, (new TimeOnly(5, 42), new TimeOnly(18, 17)) },
    { 2, (new TimeOnly(5, 40), new TimeOnly(18, 19)) },
    { 3, (new TimeOnly(5, 39), new TimeOnly(18, 20)) },
    { 4, (new TimeOnly(5, 37), new TimeOnly(18, 21)) },
    { 5, (new TimeOnly(5, 35), new TimeOnly(18, 22)) },
    { 6, (new TimeOnly(5, 34), new TimeOnly(18, 23)) },
    { 7, (new TimeOnly(5, 32), new TimeOnly(18, 24)) },
    { 8, (new TimeOnly(5, 31), new TimeOnly(18, 26)) },
    { 9, (new TimeOnly(5, 29), new TimeOnly(18, 27)) },
    { 10, (new TimeOnly(5, 28), new TimeOnly(18, 28)) },
    { 11, (new TimeOnly(5, 27), new TimeOnly(18, 29)) },
    { 12, (new TimeOnly(5, 25), new TimeOnly(18, 30)) },
    { 13, (new TimeOnly(5, 23), new TimeOnly(18, 31)) },
    { 14, (new TimeOnly(5, 21), new TimeOnly(18, 32)) },
    { 15, (new TimeOnly(5, 20), new TimeOnly(18, 33)) },
    { 16, (new TimeOnly(5, 18), new TimeOnly(18, 34)) },
    { 17, (new TimeOnly(5, 16), new TimeOnly(18, 36)) },
    { 18, (new TimeOnly(5, 14), new TimeOnly(18, 37)) },
    { 19, (new TimeOnly(5, 13), new TimeOnly(18, 38)) },
    { 20, (new TimeOnly(5, 12), new TimeOnly(18, 39)) },
    { 21, (new TimeOnly(5, 10), new TimeOnly(18, 40)) },
    { 22, (new TimeOnly(5, 8), new TimeOnly(18, 41)) },
    { 23, (new TimeOnly(5, 6), new TimeOnly(18, 42)) },
    { 24, (new TimeOnly(5, 5), new TimeOnly(18, 43)) },
    { 25, (new TimeOnly(5, 3), new TimeOnly(18, 44)) },
    { 26, (new TimeOnly(5, 1), new TimeOnly(18, 45)) },
    { 27, (new TimeOnly(4, 59), new TimeOnly(18, 46)) },
    { 28, (new TimeOnly(4, 57), new TimeOnly(18, 48)) },
    { 29, (new TimeOnly(4, 56), new TimeOnly(18, 49)) },
    { 30, (new TimeOnly(4, 54), new TimeOnly(18, 50)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Samarqand = new()
{
    { 1, (new TimeOnly(5, 49), new TimeOnly(18, 25)) },
    { 2, (new TimeOnly(5, 47), new TimeOnly(18, 27)) },
    { 3, (new TimeOnly(5, 46), new TimeOnly(18, 28)) },
    { 4, (new TimeOnly(5, 44), new TimeOnly(18, 29)) },
    { 5, (new TimeOnly(5, 42), new TimeOnly(18, 30)) },
    { 6, (new TimeOnly(5, 41), new TimeOnly(18, 31)) },
    { 7, (new TimeOnly(5, 39), new TimeOnly(18, 32)) },
    { 8, (new TimeOnly(5, 38), new TimeOnly(18, 34)) },
    { 9, (new TimeOnly(5, 36), new TimeOnly(18, 35)) },
    { 10, (new TimeOnly(5, 35), new TimeOnly(18, 36)) },
    { 11, (new TimeOnly(5, 34), new TimeOnly(18, 37)) },
    { 12, (new TimeOnly(5, 32), new TimeOnly(18, 38)) },
    { 13, (new TimeOnly(5, 30), new TimeOnly(18, 39)) },
    { 14, (new TimeOnly(5, 28), new TimeOnly(18, 40)) },
    { 15, (new TimeOnly(5, 27), new TimeOnly(18, 41)) },
    { 16, (new TimeOnly(5, 25), new TimeOnly(18, 42)) },
    { 17, (new TimeOnly(5, 23), new TimeOnly(18, 44)) },
    { 18, (new TimeOnly(5, 21), new TimeOnly(18, 45)) },
    { 19, (new TimeOnly(5, 20), new TimeOnly(18, 46)) },
    { 20, (new TimeOnly(5, 20), new TimeOnly(18, 46)) },
    { 21, (new TimeOnly(5, 18), new TimeOnly(18, 47)) },
    { 22, (new TimeOnly(5, 16), new TimeOnly(18, 48)) },
    { 23, (new TimeOnly(5, 14), new TimeOnly(18, 49)) },
    { 24, (new TimeOnly(5, 13), new TimeOnly(18, 50)) },
    { 25, (new TimeOnly(5, 11), new TimeOnly(18, 51)) },
    { 26, (new TimeOnly(5, 9), new TimeOnly(18, 52)) },
    { 27, (new TimeOnly(5, 7), new TimeOnly(18, 53)) },
    { 28, (new TimeOnly(5, 5), new TimeOnly(18, 55)) },
    { 29, (new TimeOnly(5, 4), new TimeOnly(18, 56)) },
    { 30, (new TimeOnly(5, 2), new TimeOnly(18, 56)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Xorazm = new()
{
    { 1, (new TimeOnly(6, 14), new TimeOnly(18, 48)) },
    { 2, (new TimeOnly(6, 12), new TimeOnly(18, 50)) },
    { 3, (new TimeOnly(6, 11), new TimeOnly(18, 51)) },
    { 4, (new TimeOnly(6, 9), new TimeOnly(18, 52)) },
    { 5, (new TimeOnly(6, 7), new TimeOnly(18, 53)) },
    { 6, (new TimeOnly(6, 6), new TimeOnly(18, 54)) },
    { 7, (new TimeOnly(6, 4), new TimeOnly(18, 55)) },
    { 8, (new TimeOnly(6, 3), new TimeOnly(18, 57)) },
    { 9, (new TimeOnly(6, 1), new TimeOnly(18, 58)) },
    { 10, (new TimeOnly(5, 58), new TimeOnly(19, 0)) },
    { 11, (new TimeOnly(5, 57), new TimeOnly(19, 1)) },
    { 12, (new TimeOnly(5, 55), new TimeOnly(19, 2)) },
    { 13, (new TimeOnly(5, 53), new TimeOnly(19, 3)) },
    { 14, (new TimeOnly(5, 51), new TimeOnly(19, 4)) },
    { 15, (new TimeOnly(5, 50), new TimeOnly(19, 5)) },
    { 16, (new TimeOnly(5, 48), new TimeOnly(19, 6)) },
    { 17, (new TimeOnly(5, 46), new TimeOnly(19, 8)) },
    { 18, (new TimeOnly(5, 44), new TimeOnly(19, 9)) },
    { 19, (new TimeOnly(5, 43), new TimeOnly(19, 10)) },
    { 20, (new TimeOnly(5, 41), new TimeOnly(19, 12)) },
    { 21, (new TimeOnly(5, 39), new TimeOnly(19, 13)) },
    { 22, (new TimeOnly(5, 37), new TimeOnly(19, 14)) },
    { 23, (new TimeOnly(5, 35), new TimeOnly(19, 15)) },
    { 24, (new TimeOnly(5, 34), new TimeOnly(19, 16)) },
    { 25, (new TimeOnly(5, 32), new TimeOnly(19, 17)) },
    { 26, (new TimeOnly(5, 30), new TimeOnly(19, 18)) },
    { 27, (new TimeOnly(5, 28), new TimeOnly(19, 19)) },
    { 28, (new TimeOnly(5, 26), new TimeOnly(19, 21)) },
    { 29, (new TimeOnly(5, 25), new TimeOnly(19, 22)) },
    { 30, (new TimeOnly(5, 22), new TimeOnly(19, 24)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Qashqadaryo = new()
{
    { 1, (new TimeOnly(5, 54), new TimeOnly(18, 31)) },
    { 2, (new TimeOnly(5, 52), new TimeOnly(18, 33)) },
    { 3, (new TimeOnly(5, 51), new TimeOnly(18, 34)) },
    { 4, (new TimeOnly(5, 49), new TimeOnly(18, 35)) },
    { 5, (new TimeOnly(5, 47), new TimeOnly(18, 36)) },
    { 6, (new TimeOnly(5, 46), new TimeOnly(18, 37)) },
    { 7, (new TimeOnly(5, 44), new TimeOnly(18, 38)) },
    { 8, (new TimeOnly(5, 43), new TimeOnly(18, 40)) },
    { 9, (new TimeOnly(5, 41), new TimeOnly(18, 41)) },
    { 10, (new TimeOnly(5, 40), new TimeOnly(18, 41)) },
    { 11, (new TimeOnly(5, 39), new TimeOnly(18, 42)) },
    { 12, (new TimeOnly(5, 37), new TimeOnly(18, 43)) },
    { 13, (new TimeOnly(5, 35), new TimeOnly(18, 44)) },
    { 14, (new TimeOnly(5, 33), new TimeOnly(18, 45)) },
    { 15, (new TimeOnly(5, 32), new TimeOnly(18, 46)) },
    { 16, (new TimeOnly(5, 30), new TimeOnly(18, 47)) },
    { 17, (new TimeOnly(5, 28), new TimeOnly(18, 49)) },
    { 18, (new TimeOnly(5, 26), new TimeOnly(18, 50)) },
    { 19, (new TimeOnly(5, 25), new TimeOnly(18, 51)) },
    { 20, (new TimeOnly(5, 25), new TimeOnly(18, 51)) },
    { 21, (new TimeOnly(5, 23), new TimeOnly(18, 52)) },
    { 22, (new TimeOnly(5, 21), new TimeOnly(18, 53)) },
    { 23, (new TimeOnly(5, 19), new TimeOnly(18, 54)) },
    { 24, (new TimeOnly(5, 18), new TimeOnly(18, 55)) },
    { 25, (new TimeOnly(5, 16), new TimeOnly(18, 56)) },
    { 26, (new TimeOnly(5, 14), new TimeOnly(18, 57)) },
    { 27, (new TimeOnly(5, 12), new TimeOnly(18, 58)) },
    { 28, (new TimeOnly(5, 10), new TimeOnly(19, 0)) },
    { 29, (new TimeOnly(5, 9), new TimeOnly(19, 1)) },
    { 30, (new TimeOnly(5, 9), new TimeOnly(19, 1)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Buxoro = new()
{
    { 1, (new TimeOnly(5, 59), new TimeOnly(18, 35)) },
    { 2, (new TimeOnly(5, 57), new TimeOnly(18, 37)) },
    { 3, (new TimeOnly(5, 56), new TimeOnly(18, 38)) },
    { 4, (new TimeOnly(5, 54), new TimeOnly(18, 39)) },
    { 5, (new TimeOnly(5, 52), new TimeOnly(18, 40)) },
    { 6, (new TimeOnly(5, 51), new TimeOnly(18, 41)) },
    { 7, (new TimeOnly(5, 49), new TimeOnly(18, 42)) },
    { 8, (new TimeOnly(5, 48), new TimeOnly(18, 44)) },
    { 9, (new TimeOnly(5, 46), new TimeOnly(18, 45)) },
    { 10, (new TimeOnly(5, 45), new TimeOnly(18, 46)) },
    { 11, (new TimeOnly(5, 44), new TimeOnly(18, 47)) },
    { 12, (new TimeOnly(5, 42), new TimeOnly(18, 48)) },
    { 13, (new TimeOnly(5, 40), new TimeOnly(18, 49)) },
    { 14, (new TimeOnly(5, 38), new TimeOnly(18, 50)) },
    { 15, (new TimeOnly(5, 37), new TimeOnly(18, 51)) },
    { 16, (new TimeOnly(5, 35), new TimeOnly(18, 52)) },
    { 17, (new TimeOnly(5, 33), new TimeOnly(18, 54)) },
    { 18, (new TimeOnly(5, 31), new TimeOnly(18, 55)) },
    { 19, (new TimeOnly(5, 30), new TimeOnly(18, 56)) },
    { 20, (new TimeOnly(5, 30), new TimeOnly(18, 56)) },
    { 21, (new TimeOnly(5, 28), new TimeOnly(18, 57)) },
    { 22, (new TimeOnly(5, 26), new TimeOnly(18, 58)) },
    { 23, (new TimeOnly(5, 24), new TimeOnly(18, 59)) },
    { 24, (new TimeOnly(5, 23), new TimeOnly(19, 0)) },
    { 25, (new TimeOnly(5, 21), new TimeOnly(19, 1)) },
    { 26, (new TimeOnly(5, 19), new TimeOnly(19, 2)) },
    { 27, (new TimeOnly(5, 17), new TimeOnly(19, 3)) },
    { 28, (new TimeOnly(5, 15), new TimeOnly(19, 5)) },
    { 29, (new TimeOnly(5, 14), new TimeOnly(19, 6)) },
    { 30, (new TimeOnly(5, 12), new TimeOnly(19, 6)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Navoiy = new()
{
    { 1, (new TimeOnly(5, 56), new TimeOnly(18, 32)) },
    { 2, (new TimeOnly(5, 54), new TimeOnly(18, 34)) },
    { 3, (new TimeOnly(5, 53), new TimeOnly(18, 35)) },
    { 4, (new TimeOnly(5, 51), new TimeOnly(18, 36)) },
    { 5, (new TimeOnly(5, 49), new TimeOnly(18, 37)) },
    { 6, (new TimeOnly(5, 48), new TimeOnly(18, 38)) },
    { 7, (new TimeOnly(5, 46), new TimeOnly(18, 39)) },
    { 8, (new TimeOnly(5, 45), new TimeOnly(18, 41)) },
    { 9, (new TimeOnly(5, 43), new TimeOnly(18, 42)) },
    { 10, (new TimeOnly(5, 41), new TimeOnly(18, 42)) },
    { 11, (new TimeOnly(5, 40), new TimeOnly(18, 43)) },
    { 12, (new TimeOnly(5, 38), new TimeOnly(18, 44)) },
    { 13, (new TimeOnly(5, 36), new TimeOnly(18, 45)) },
    { 14, (new TimeOnly(5, 34), new TimeOnly(18, 46)) },
    { 15, (new TimeOnly(5, 33), new TimeOnly(18, 47)) },
    { 16, (new TimeOnly(5, 31), new TimeOnly(18, 48)) },
    { 17, (new TimeOnly(5, 29), new TimeOnly(18, 50)) },
    { 18, (new TimeOnly(5, 27), new TimeOnly(18, 51)) },
    { 19, (new TimeOnly(5, 26), new TimeOnly(18, 52)) },
    { 20, (new TimeOnly(5, 26), new TimeOnly(18, 53)) },
    { 21, (new TimeOnly(5, 24), new TimeOnly(18, 54)) },
    { 22, (new TimeOnly(5, 22), new TimeOnly(18, 55)) },
    { 23, (new TimeOnly(5, 20), new TimeOnly(18, 56)) },
    { 24, (new TimeOnly(5, 19), new TimeOnly(18, 57)) },
    { 25, (new TimeOnly(5, 17), new TimeOnly(18, 58)) },
    { 26, (new TimeOnly(5, 15), new TimeOnly(18, 59)) },
    { 27, (new TimeOnly(5, 13), new TimeOnly(19, 0)) },
    { 28, (new TimeOnly(5, 11), new TimeOnly(19, 2)) },
    { 29, (new TimeOnly(5, 10), new TimeOnly(19, 3)) },
    { 30, (new TimeOnly(5, 8), new TimeOnly(19, 3)) }
};

        private static readonly Dictionary<int, (TimeOnly Saharlik, TimeOnly Iftorlik)> Surxondaryo = new()
{
    { 1, (new TimeOnly(5, 49), new TimeOnly(18, 26)) },
    { 2, (new TimeOnly(5, 47), new TimeOnly(18, 28)) },
    { 3, (new TimeOnly(5, 46), new TimeOnly(18, 29)) },
    { 4, (new TimeOnly(5, 44), new TimeOnly(18, 30)) },
    { 5, (new TimeOnly(5, 42), new TimeOnly(18, 31)) },
    { 6, (new TimeOnly(5, 41), new TimeOnly(18, 32)) },
    { 7, (new TimeOnly(5, 39), new TimeOnly(18, 33)) },
    { 8, (new TimeOnly(5, 38), new TimeOnly(18, 35)) },
    { 9, (new TimeOnly(5, 36), new TimeOnly(18, 36)) },
    { 10, (new TimeOnly(5, 36), new TimeOnly(18, 36)) },
    { 11, (new TimeOnly(5, 35), new TimeOnly(18, 37)) },
    { 12, (new TimeOnly(5, 33), new TimeOnly(18, 38)) },
    { 13, (new TimeOnly(5, 31), new TimeOnly(18, 39)) },
    { 14, (new TimeOnly(5, 29), new TimeOnly(18, 40)) },
    { 15, (new TimeOnly(5, 28), new TimeOnly(18, 41)) },
    { 16, (new TimeOnly(5, 26), new TimeOnly(18, 42)) },
    { 17, (new TimeOnly(5, 24), new TimeOnly(18, 44)) },
    { 18, (new TimeOnly(5, 22), new TimeOnly(18, 45)) },
    { 19, (new TimeOnly(5, 21), new TimeOnly(18, 46)) },
    { 20, (new TimeOnly(5, 21), new TimeOnly(18, 45)) },
    { 21, (new TimeOnly(5, 19), new TimeOnly(18, 46)) },
    { 22, (new TimeOnly(5, 17), new TimeOnly(18, 47)) },
    { 23, (new TimeOnly(5, 15), new TimeOnly(18, 48)) },
    { 24, (new TimeOnly(5, 14), new TimeOnly(18, 49)) },
    { 25, (new TimeOnly(5, 12), new TimeOnly(18, 50)) },
    { 26, (new TimeOnly(5, 10), new TimeOnly(18, 51)) },
    { 27, (new TimeOnly(5, 8), new TimeOnly(18, 52)) },
    { 28, (new TimeOnly(5, 6), new TimeOnly(18, 54)) },
    { 29, (new TimeOnly(5, 5), new TimeOnly(18, 55)) },
    { 30, (new TimeOnly(5, 5), new TimeOnly(18, 54)) }
};



        private static readonly Dictionary<long, string> userForTaqvim = new Dictionary<long, string>();


        static void Main(string[] args)
        {

            var receiverOptions = new ReceiverOptions { AllowedUpdates = new[] { UpdateType.Message, UpdateType.CallbackQuery } };

            Console.WriteLine("Your bot is starting");


            BotClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions
            );


            Console.ReadKey();
        }




        public static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {


            if (update.Type == UpdateType.Message)
            {

                var message = update.Message;
                var user = message.Chat;
                Console.WriteLine($"{user.Id},  {user.FirstName}, {message.Text}");


                if (message.Text == "🌙 Saxarlik Duosi 🌙")
                {
                    await bot.SendTextMessageAsync(user.Id, "🌙 Saxarlik duosi:\r\n\r\n نَوَيْتُ أَنْ أَصُومَ صَوْمَ شَهْرِ رَمَضَانَ مِنَ الْفَجْرِ إِلَى الْمَغْرِبِ، خَالِصًا لِلَّهِ تَعَالَى. اللَّهُ أَكْبَرُ.ُ.ُ\r\n\r\nTarjimasi ->  Navaytu an asuma sovma shahri Ramazona minal fajri ilal mag‘ribi, xolisan lillahi ta’alaa. Allohu akbar.\r\n\r\nMa'nosi: Allohim, men Sening roziliging uchun ro‘za tutdim, Senga iymon keltirdim, Senga tavakkul qildim va Sen ato etgan rizq bilan saxarlik qildim. ", cancellationToken: cancellationToken);
                }

                if (message.Text == "🌅 Iftorlik Duosi 🌅")
                {
                    await bot.SendTextMessageAsync(user.Id, "🌅 Iftorlik duosi:\r\n\r\nاللَّهُمَّ لَكَ صُمْتُ وَبِكَ آمَنْتُ وَعَلَيْكَ تَوَكَّلْتُ وَعَلَى رِزْقِكَ أَفْطَرْتُ، فَاغْفِرْ لِي ذُنُوبِي يَا غَفَّارُ مَا قَدَّمْتُ وَمَا أَخَّرْتُ.\r\n\r\nTarjimasi ->  Allohumma laka sumtu va bika aamantu va ’alayka tavakkaltu va ’alaa rizqika aftortu, fag‘firliy zunubiy yaa G‘offaru maa qoddamtu va maa axxortu. \r\n\r\nMa'nosi: Allohim, men Sening roziliging uchun ro‘za tutdim, Senga iymon keltirdim, Senga tavakkul qildim va Sen ato etgan rizq bilan iftorlik qildim. Mening oldingi va keyingi gunohlarimni kechir.", cancellationToken: cancellationToken);
                }



                if (message.Text == "/start")
                {
                    var userObject = await _mainContext.Users.FirstOrDefaultAsync(u => u.TelegramUserId == user.Id);


                    if (userObject == null)
                    {
                        userObject = new UserEntity()
                        {
                            CreatedAt = DateTime.UtcNow,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IsBlocked = false,
                            PhoneNumber = null,
                            TelegramUserId = user.Id,
                            UpdatedAt = DateTime.UtcNow,
                            Username = user.Username
                        };
                        _mainContext.Users.Add(userObject);
                        _mainContext.SaveChanges();
                    }
                    else
                    {
                        if (user.FirstName != userObject.FirstName || user.LastName != userObject.LastName || user.Username != userObject.Username)
                        {
                            userObject.UpdatedAt = DateTime.UtcNow;
                        };
                        userObject.FirstName = user.FirstName;
                        userObject.LastName = user.LastName;
                        userObject.Username = user.Username;

                        _mainContext.SaveChanges();
                    }

                    var keyboard = new ReplyKeyboardMarkup(new[]
{
    new[]
    {
        new KeyboardButton("🌙 Saxarlik Duosi 🌙"),
        new KeyboardButton("🌅 Iftorlik Duosi 🌅"),
    },
})
                    { ResizeKeyboard = true };

                    await bot.SendTextMessageAsync(user.Id, "Assalomu Alaykum 👋\r\n\r\n🌙 Ramazon Taqvimi botiga xush kelibsiz! ", replyMarkup: keyboard);

                    var inlineMenu = new InlineKeyboardMarkup(new[]
                        {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Toshkent"),
                        InlineKeyboardButton.WithCallbackData("Namangan"),
                        InlineKeyboardButton.WithCallbackData("Jizzax")
                    },new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Andijon"),
                        InlineKeyboardButton.WithCallbackData("Farg'ona"),
                        InlineKeyboardButton.WithCallbackData("Sirdaryo")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Samarqand"),
                        InlineKeyboardButton.WithCallbackData("Xorazm"),
                        InlineKeyboardButton.WithCallbackData("Qashqadaryo"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Buxoro"),
                        InlineKeyboardButton.WithCallbackData("Navoiy"),
                        InlineKeyboardButton.WithCallbackData("Surxondaryo"),
                    },
                });

                    await bot.SendTextMessageAsync(user.Id, "🕌 Qaysi viloyatning Ramazon taqvimi kerak ?  ", replyMarkup: inlineMenu);

                    return;
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                var id = update.CallbackQuery.From.Id;

                var text = update.CallbackQuery.Data;

                CallbackQuery res = update.CallbackQuery;
                var day = 0;
                try
                {
                     day = int.Parse(text);
                }
                catch(Exception ex) 
                {
                    try
                    {
                        userForTaqvim.Add(id,text);
                    }
                    catch (Exception exc)
                    {
                        userForTaqvim.Remove(id);
                        userForTaqvim.Add(id, text);
                    }

                    var inlineMenu = new InlineKeyboardMarkup(new[]
                               {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("1"),
                        InlineKeyboardButton.WithCallbackData("2"),
                        InlineKeyboardButton.WithCallbackData("3"),
                        InlineKeyboardButton.WithCallbackData("4"),
                        InlineKeyboardButton.WithCallbackData("5"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("6"),
                        InlineKeyboardButton.WithCallbackData("7"),
                        InlineKeyboardButton.WithCallbackData("8"),
                        InlineKeyboardButton.WithCallbackData("9"),
                        InlineKeyboardButton.WithCallbackData("10"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("11"),
                        InlineKeyboardButton.WithCallbackData("12"),
                        InlineKeyboardButton.WithCallbackData("13"),
                        InlineKeyboardButton.WithCallbackData("14"),
                        InlineKeyboardButton.WithCallbackData("15"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("16"),
                        InlineKeyboardButton.WithCallbackData("17"),
                        InlineKeyboardButton.WithCallbackData("18"),
                        InlineKeyboardButton.WithCallbackData("19"),
                        InlineKeyboardButton.WithCallbackData("20"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("21"),
                        InlineKeyboardButton.WithCallbackData("22"),
                        InlineKeyboardButton.WithCallbackData("23"),
                        InlineKeyboardButton.WithCallbackData("24"),
                        InlineKeyboardButton.WithCallbackData("25"),
                    },
                    new[]
                    {

                        InlineKeyboardButton.WithCallbackData("26"),
                        InlineKeyboardButton.WithCallbackData("27"),
                        InlineKeyboardButton.WithCallbackData("28"),
                        InlineKeyboardButton.WithCallbackData("29"),
                        InlineKeyboardButton.WithCallbackData("30"),
                    },
                });

                    await bot.SendTextMessageAsync(id, "🗓 Kunni Tanlang : ", replyMarkup: inlineMenu);
                }

               
                    var taqvimState = userForTaqvim[id];
                    var times = GetTime(day, taqvimState);
                var result = $"\r\t🗓 {day}-Mart 🗓\n\n🕋 {("Ramazon Taqvimi".ToUpper())} 🕋\r\n\r\n" +
             $"📍Viloyat: {taqvimState} 📍\r\n\r\n" +
             $"🌙 Saharlik: {times.Item1} 🌙\r\n\n" +
             $"🌅  Iftorlik: {times.Item2} 🌅  ";

                await bot.SendTextMessageAsync(id, result, cancellationToken: cancellationToken);
            }
        }

        private static (string, string) GetTime(int day, string state)
        {
            if (state == "Toshkent")
            {
                var times = Toshkent[day];
                var timeSaxarlik = times.Saharlik;
                var timeIftorlik = times.Iftorlik;
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Namangan")
            {
                var times = Namangan[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(-10);
                var timeIftorlik = times.Iftorlik.AddMinutes(-9);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Jizzax")
            {
                var times = Jizzax[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(6);
                var timeIftorlik = times.Iftorlik.AddMinutes(7);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Andijon")
            {
                var times = Andijon[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(-12);
                var timeIftorlik = times.Iftorlik.AddMinutes(-12);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Farg'ona")
            {
                var times = Fargona[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(-10);
                var timeIftorlik = times.Iftorlik.AddMinutes(-10);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Sirdaryo")
            {
                var times = Sirdaryo[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(-2);
                var timeIftorlik = times.Iftorlik.AddMinutes(-2);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Samarqand")
            {
                var times = Samarqand[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(9);
                var timeIftorlik = times.Iftorlik.AddMinutes(10);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Xorazm")
            {
                var times = Xorazm[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(34);
                var timeIftorlik = times.Iftorlik.AddMinutes(33);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Qashqadaryo")
            {
                var times = Qashqadaryo[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(14);
                var timeIftorlik = times.Iftorlik.AddMinutes(16);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Buxoro")
            {
                var times = Buxoro[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(19);
                var timeIftorlik = times.Iftorlik.AddMinutes(20);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Navoiy")
            {
                var times = Navoiy[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(16);
                var timeIftorlik = times.Iftorlik.AddMinutes(17);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            else if (state == "Surxondaryo")
            {
                var times = Surxondaryo[day];
                var timeSaxarlik = times.Saharlik.AddMinutes(9);
                var timeIftorlik = times.Iftorlik.AddMinutes(11);
                return (timeSaxarlik.ToString("hh:mm"), timeIftorlik.ToString("hh:mm"));
            }
            return ("", "");
        }
        
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
