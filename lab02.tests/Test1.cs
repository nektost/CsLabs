using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsLabs; // Подключаем пространство имен, где лежит наш Program

namespace lab02.tests
{
    [TestClass]
    public class ServerTests
    {
        // ТЕСТ 1: Успешный запуск сервера
        [TestMethod]
        public void Test_ServerReady()
        {
            // Arrange (Подготовка) + Act (Действие)
            string result = Program.CheckServer("MyServer", 10, 5, 1000, "нет");

            // Assert (Проверка)
            Assert.AreEqual("СЕРВЕР MyServer ГОТОВ К ЗАПУСКУ.", result);
        }

        // ТЕСТ 2: Пустое название сервера
        [TestMethod]
        public void Test_EmptyName()
        {
            string result = Program.CheckServer("", 10, 5, 1000, "нет");
            Assert.AreEqual("ЗАПУСК НЕВОЗМОЖЕН: название сервера пустое.", result);
        }

        // ТЕСТ 3: Количество игроков равно 0
        [TestMethod]
        public void Test_ZeroPlayers()
        {
            string result = Program.CheckServer("MyServer", 0, 5, 1000, "нет");
            Assert.AreEqual("ЗАПУСК НЕВОЗМОЖЕН: количество игроков должно быть больше 0.", result);
        }

        // ТЕСТ 4: Игроков меньше минимально допустимого (3 < 5)
        [TestMethod]
        public void Test_NotEnoughPlayers()
        {
            string result = Program.CheckServer("MyServer", 3, 5, 1000, "нет");
            Assert.AreEqual("ЗАПУСК НЕВОЗМОЖЕН: игроков меньше минимально допустимого.", result);
        }

        // ТЕСТ 5: Недостаточно оперативной памяти
        // (10 игроков * 80 = 800 МБ нужно, а даем только 500)
        [TestMethod]
        public void Test_NotEnoughRam()
        {
            string result = Program.CheckServer("MyServer", 10, 5, 500, "нет");
            Assert.AreEqual(
                "ЗАПУСК НЕВОЗМОЖЕН: недостаточно оперативной памяти.\nНужно: 800 МБ, есть: 500 МБ.",
                result
            );
        }

        // ТЕСТ 6: Сервер защищен паролем
        [TestMethod]
        public void Test_PasswordProtected()
        {
            string result = Program.CheckServer("MyServer", 10, 5, 1000, "да");
            Assert.AreEqual("НУЖЕН АДМИНИСТРАТОР: сервер MyServer защищён паролем.", result);
        }

        // ТЕСТ 7: Граничный случай - игроков ровно столько, сколько нужно по минимуму
        [TestMethod]
        public void Test_ExactMinPlayers()
        {
            string result = Program.CheckServer("MyServer", 5, 5, 1000, "нет");
            Assert.AreEqual("СЕРВЕР MyServer ГОТОВ К ЗАПУСКУ.", result);
        }

        // ТЕСТ 8: Граничный случай - памяти ровно столько, сколько нужно
        // (10 игроков * 80 = 800 МБ, даем ровно 800)
        [TestMethod]
        public void Test_ExactRam()
        {
            string result = Program.CheckServer("MyServer", 10, 5, 800, "нет");
            Assert.AreEqual("СЕРВЕР MyServer ГОТОВ К ЗАПУСКУ.", result);
        }
    }
}