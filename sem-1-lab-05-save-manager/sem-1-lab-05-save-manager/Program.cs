using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace sem_1_lab_05_save_manager
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class SaveEntry
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SaveData
    {
        public string Character { get; set; }
        public string Map { get; set; }
        public Position Position { get; set; }
        public int Health { get; set; }
        public List<string> Inventory { get; set; }
    }

    public class Program
    {
        static string indexPath = "index.json";
        static string savesDir = "saves";
        static string backupsDir = "backups";

        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
        };

        static void Main()
        {
            if (!Directory.Exists(savesDir)) Directory.CreateDirectory(savesDir);
            if (!Directory.Exists(backupsDir)) Directory.CreateDirectory(backupsDir);

            List<SaveEntry> entries = LoadIndex(indexPath);

            while (true)
            {
                Console.WriteLine("\n1. Список  2. Посмотреть  3. Изменить  4. Бэкап  5. Восстановить  6. Удалить  7. Выход");
                Console.Write("Выбор: ");
                string choice = Console.ReadLine();

                if (choice == "7") break;

                if (choice == "1")
                {
                    ShowList(entries);
                    continue;
                }

                Console.Write("Имя сохранения: ");
                string name = Console.ReadLine();
                SaveEntry entry = FindByName(entries, name);

                if (entry == null)
                {
                    Console.WriteLine("Не найдено!");
                    continue;
                }

                if (choice == "2")
                {
                    SaveData save = LoadSave(savesDir, entry);
                    if (save != null) PrintSave(entry, save);
                }
                else if (choice == "3")
                {
                    SaveData save = LoadSave(savesDir, entry);
                    if (save != null) EditSave(entry, save, entries);
                }
                else if (choice == "4")
                {
                    CreateBackup(savesDir, backupsDir, entry);
                }
                else if (choice == "5")
                {
                    RestoreBackup(savesDir, backupsDir, entry);
                }
                else if (choice == "6")
                {
                    DeleteSave(savesDir, indexPath, entries, entry);
                }
            }
        }

        public static List<SaveEntry> LoadIndex(string path)
        {
            if (!File.Exists(path))
            {
                return new List<SaveEntry>();
            }
            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<SaveEntry>>(json, Settings);
            }
            catch
            {
                return new List<SaveEntry>();
            }
        }

        public static void ShowList(List<SaveEntry> entries)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine(entries[i].Name + " (" + entries[i].FileName + ")");
            }
        }

        public static SaveEntry FindByName(List<SaveEntry> entries, string name)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].Name.ToLower() == name.ToLower()) return entries[i];
            }
            return null;
        }

        public static SaveData LoadSave(string dir, SaveEntry entry)
        {
            string path = Path.Combine(dir, entry.FileName);
            if (!File.Exists(path)) return null;
            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<SaveData>(json, Settings);
            }
            catch
            {
                return null;
            }
        }

        public static void PrintSave(SaveEntry entry, SaveData save)
        {
            Console.WriteLine("--- " + entry.Name + " ---");
            Console.WriteLine("Карта: " + save.Map);
            Console.WriteLine("Х: " + save.Position.X + " У: " + save.Position.Y);
            Console.WriteLine("Здоровье: " + save.Health);
            Console.WriteLine("Инвентарь: " + string.Join(", ", save.Inventory));
        }

        public static void EditSave(SaveEntry entry, SaveData save, List<SaveEntry> entries)
        {
            Console.WriteLine("Новая карта (Enter - пропустить):");
            string input = Console.ReadLine();
            if (input != "") save.Map = input;

            Console.WriteLine("Новый X (Enter - пропустить):");
            input = Console.ReadLine();
            if (input != "") save.Position.X = int.Parse(input);

            Console.WriteLine("Новый Y (Enter - пропустить):");
            input = Console.ReadLine();
            if (input != "") save.Position.Y = int.Parse(input);

            Console.WriteLine("Новое здоровье (Enter - пропустить):");
            input = Console.ReadLine();
            if (input != "") save.Health = int.Parse(input);

            Console.WriteLine("Новый инвентарь через запятую (Enter - пропустить):");
            input = Console.ReadLine();
            if (input != "") save.Inventory = new List<string>(input.Split(','));

            SaveGame(savesDir, entry, save);

            entry.UpdatedAt = DateTime.Now;
            SaveIndex(indexPath, entries);
            Console.WriteLine("Сохранено!");
        }

        public static void SaveGame(string dir, SaveEntry entry, SaveData save)
        {
            string json = JsonConvert.SerializeObject(save, Formatting.Indented, Settings);
            File.WriteAllText(Path.Combine(dir, entry.FileName), json);
        }

        public static void SaveIndex(string path, List<SaveEntry> entries)
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented, Settings);
            File.WriteAllText(path, json);
        }

        public static void CreateBackup(string sDir, string bDir, SaveEntry entry)
        {
            File.Copy(Path.Combine(sDir, entry.FileName), Path.Combine(bDir, entry.FileName), true);
            Console.WriteLine("Бэкап создан!");
        }

        public static void RestoreBackup(string sDir, string bDir, SaveEntry entry)
        {
            File.Copy(Path.Combine(bDir, entry.FileName), Path.Combine(sDir, entry.FileName), true);
            Console.WriteLine("Восстановлено!");
        }

        public static void DeleteSave(string sDir, string iPath, List<SaveEntry> entries, SaveEntry entry)
        {
            string path = Path.Combine(sDir, entry.FileName);
            if (File.Exists(path)) File.Delete(path);

            entries.Remove(entry);
            SaveIndex(iPath, entries);
            Console.WriteLine("Удалено!");
        }
    }
}