using System;

namespace Sem1Lab01ServerMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Название сервера: ");
            string serverName = Console.ReadLine();

            Console.Write("Регион (одна буква): ");
            char region = char.Parse(Console.ReadLine());

            Console.Write("Максимум игроков: ");
            int maxPlayers = int.Parse(Console.ReadLine());

            Console.Write("Игроков онлайн: ");
            int onlinePlayers = int.Parse(Console.ReadLine());

            Console.Write("Средний пинг (мс): ");
            double ping = double.Parse(Console.ReadLine());

            Console.Write("Сервер включён? (true/false): ");
            bool isOnline = bool.Parse(Console.ReadLine());

            Console.Write("Время работы сервера в секундах: ");
            long uptimeSeconds = long.Parse(Console.ReadLine());

            Console.WriteLine("\n=================================");
            Console.WriteLine("       ПАСПОРТ СЕРВЕРА");
            Console.WriteLine("=================================");
            Console.WriteLine($"Название:        {serverName}");
            Console.WriteLine($"Регион:          {region}");
            Console.WriteLine($"Макс. игроков:   {maxPlayers}");
            Console.WriteLine($"Онлайн:          {onlinePlayers}");
            Console.WriteLine($"Пинг:            {ping} мс");
            Console.WriteLine($"Включён:         {isOnline}");
            Console.WriteLine($"Время работы(сек):    {uptimeSeconds}");
            Console.WriteLine("=================================");

            Console.ReadKey();
        }
    }
}