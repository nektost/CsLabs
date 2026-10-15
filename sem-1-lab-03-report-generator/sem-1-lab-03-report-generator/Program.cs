using System;
using System.IO;


namespace lab03
{
    public class Program
    {
        public static string BuildReport(string[] lines)
        {
            string date = "";
            int warnings = 0;
            int errors = 0;
            bool inside = false;
            string winner = "";
            int winnerScore = 0;
            int consolationScore = 0;
            string eventItem = "";

            foreach (string line in lines)
            {
                if (line.Contains("Событие началось"))
                {
                    inside = true;
                    string time = line.Substring(0, 23);
                    DateTime dt = DateTime.Parse(time);
                    date = dt.ToString("dd.MM.yyyy");
                }

                if (inside)
                {
                    if (line.Contains("объявлены победителями события"))
                    {
                        int winnerEnd = line.IndexOf(" объявлены победителями");
                        string before = line.Substring(0, winnerEnd);
                        int bracket = before.LastIndexOf(']');
                        winner = before.Substring(bracket + 1).Trim();

                    }

                    if (line.Contains("[Warning]")) warnings++;

                    if (line.Contains("[Error]")) errors++;

                    if (line.Contains("получили") && line.Contains("очков события") && !line.Contains("утешительную награду"))
                    {
                        const string startText = " получили ";
                        int start = line.IndexOf(startText) + startText.Length;
                        int end = line.IndexOf(" очков", start);
                        winnerScore = int.Parse(line.Substring(start, end - start));
                    }
                    if (line.Contains("утешительную награду"))
                    {
                        const string startText = "утешительную награду:";
                        int start = line.IndexOf(startText) + startText.Length;
                        int end = line.IndexOf(" очков", start);
                        consolationScore = int.Parse(line.Substring(start, end - start));
                    }

                    if (line.Contains("получили ивентовый предмет:"))
                    {
                        const string startText = " предмет: ";
                        int start = line.IndexOf(startText) + startText.Length;
                        eventItem = line.Substring(start);
                    }
                }
                if (line.Contains("закрыто"))
                {
                    break;
                }
            }

            return "# Итоги события: Восстание Ледяного Пламени\n\n" +
                   "Дата: " + date + "\n" +
                   "Победитель: " + winner + "\n" +
                   "Очки победителя: " + winnerScore + "\n" +
                   "Ивентовый предмет: " + eventItem + "\n" +
                   "Утешительная награда Железных волков: " + consolationScore + " очков\n" +
                   "Предупреждений во время события: " + warnings + "\n" +
                   "Ошибок во время события: " + errors;

        }
        static void Main()
        {
            string[] lines = File.ReadAllLines("event_server.log");
            string report = BuildReport(lines);
            Console.WriteLine(report);
        }
    }
}