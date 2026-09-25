using System;

class Program
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
        int needRam = players * 80;
        Console.WriteLine();
        Console.WriteLine("Результат проверки:");
        if (name == "")
        {
            Console.WriteLine("ЗАПУСК НЕВОЗМОЖЕН: название сервера пустое.");
        }
        else if (players <= 0)
        {
            Console.WriteLine("ЗАПУСК НЕВОЗМОЖЕН: количество игроков должно быть больше 0.");
        }
        else if (players < minPlayers)
        {
            Console.WriteLine("ЗАПУСК НЕВОЗМОЖЕН: игроков меньше минимально допустимого.");
        }
        else if (needRam > ramMb)
        {
            Console.WriteLine("ЗАПУСК НЕВОЗМОЖЕН: недостаточно оперативной памяти.");
            Console.WriteLine("Нужно: " + needRam + " МБ, есть: " + ramMb + " МБ.");
        }
        else if (pass == "да")
        {
            Console.WriteLine("НУЖЕН АДМИНИСТРАТОР: сервер " + name + " защищён паролем.");
        }
        else
        {
            Console.WriteLine("СЕРВЕР " + name + " ГОТОВ К ЗАПУСКУ.");
        }

        Console.ReadLine();
    }
}










    }
}