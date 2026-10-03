using System;

namespace CsLabs
{
    public class Program
    {
        static void Main()
        {
            Console.Write("Введите название сервера: ");
            string name = Console.ReadLine();
            Console.Write("Введите количество игроков: ");
            int players = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите минимальное количество игроков: ");
            int minPlayers = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите объём оперативной памяти (МБ): ");
            int ramMb = Convert.ToInt32(Console.ReadLine());
            Console.Write("Защищён ли сервер паролем (да/нет): ");
            string pass = Console.ReadLine();

            string result = CheckServer(name, players, minPlayers, ramMb, pass);

            Console.WriteLine();
            Console.WriteLine("Результат проверки:");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static string CheckServer(string name, int players, int minPlayers, int ramMb, string pass)
        {
            int needRam = players * 80;

            if (name == "")
                return "ЗАПУСК НЕВОЗМОЖЕН: название сервера пустое.";

            if (players <= 0)
                return "ЗАПУСК НЕВОЗМОЖЕН: количество игроков должно быть больше 0.";

            if (players < minPlayers)
                return "ЗАПУСК НЕВОЗМОЖЕН: игроков меньше минимально допустимого.";

            if (needRam > ramMb)
                return "ЗАПУСК НЕВОЗМОЖЕН: недостаточно оперативной памяти.\n" +
                       $"Нужно: {needRam} МБ, есть: {ramMb} МБ.";

            if (pass == "да")
                return $"НУЖЕН АДМИНИСТРАТОР: сервер {name} защищён паролем.";

            return $"СЕРВЕР {name} ГОТОВ К ЗАПУСКУ.";
        }
    }
}