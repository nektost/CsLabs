using System;

namespace lab04
{
     public class LogEntry
    {
        public DateTime Timestamp;
        public string Level = "";
        public string Category = "";
        public string Message = "";
    }

    public class Program
    {
        public static LogEntry[] ParseLog(string[] lines)
        {
            LogEntry[] entries = new LogEntry[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                LogEntry entry = new LogEntry();

                entry.Timestamp = DateTime.Parse(line.Substring(0, 23));

                int firstOpenBracket = line.IndexOf('[');
                int firstCloseBracket = line.IndexOf(']', firstOpenBracket);
                int secondOpenBracket = line.IndexOf('[', firstCloseBracket);
                int secondCloseBracket = line.IndexOf(']', secondOpenBracket);

                entry.Level = line.Substring(firstOpenBracket + 1, firstCloseBracket - firstOpenBracket - 1);
                entry.Category = line.Substring(secondOpenBracket + 1, secondCloseBracket - secondOpenBracket - 1);
                entry.Message = line.Substring(secondCloseBracket + 2);
                entries[i] = entry;
            }

            return entries;
        }
        public static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
        {
            LogEntry[] temporaryResult = new LogEntry[entries.Length];
            int resultCount = 0;

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level)
                {
                    temporaryResult[resultCount] = entries[i];
                    resultCount++;
                }
            }
            return Trim(temporaryResult, resultCount);
        }
        public static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
        {
            LogEntry[] temporaryResult = new LogEntry[entries.Length];
            int resultCount = 0;

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Timestamp.Date == date.Date)
                {
                    temporaryResult[resultCount] = entries[i];
                    resultCount++;
                }
            }
            return Trim(temporaryResult, resultCount);


        }
        public static LogEntry[] FilterByCategory(LogEntry[] entries, string category)
        {
            LogEntry[] temporaryResult = new LogEntry[entries.Length];
            int resultCount = 0;

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Category == category)
                {
                    temporaryResult[resultCount] = entries[i];
                    resultCount++;
                }
            }
            return Trim(temporaryResult, resultCount);
        }
        public static LogEntry[] Search(LogEntry[] entries, string text)
        {
            LogEntry[] temporaryResult = new LogEntry[entries.Length];
            int resultCount = 0;

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Message.ToLower().Contains(text.ToLower()))
                {
                    temporaryResult[resultCount] = entries[i];
                    resultCount++;
                }
            }
            return Trim(temporaryResult, resultCount);
        }
        public static int CountByLevel(LogEntry[] entries, string level)
        {
            int count = 0;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Level == level)
                {
                    count++;
                }
            }

            return count;


        }
        public static string GetServerStatus(LogEntry[] entries)
        {
            bool hasFatalServer = false;
            bool hasError = false;
            for (int i = 0; i < entries.Length; i++)
            {
                if(entries[i].Level == "Fatal" && entries[i].Category == "Server")
                {
                    hasFatalServer = true;

                }

                if (entries[i].Level == "Error")
                {
                    hasError = true;

                }


            }
            if (hasFatalServer)
            {
                return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
            }

            if (hasError)
            {
                return "Есть ошибки: требуется проверка";
            }

            return "Сервер работает штатно";
        }
        private static LogEntry[] Trim(LogEntry[] temp, int count)
        {
            LogEntry[] result = new LogEntry[count];
            Array.Copy(temp, result, count);
            return result;
        }
        static void Main()
        {
            string[] lines = System.IO.File.ReadAllLines("event_server.log");
            LogEntry[] entries = ParseLog(lines);

            Console.WriteLine("Всего: " + entries.Length);
            Console.WriteLine("Info:    " + CountByLevel(entries, "Info"));
            Console.WriteLine("Warning: " + CountByLevel(entries, "Warning"));
            Console.WriteLine("Error:   " + CountByLevel(entries, "Error"));
            Console.WriteLine("Fatal:   " + CountByLevel(entries, "Fatal"));

            Console.WriteLine("Статус: " + GetServerStatus(entries));
        }
    }
}