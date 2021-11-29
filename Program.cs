using System;
using System.IO;

namespace Dictionary
{
    /// <summary>
    /// Класс через который будет выводиться программа
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Основной мейн
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("1 - Вписать данные в консоль.\n2 - Вписать данные в файл.");
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    OnConsoleClick();
                    break;
                case ConsoleKey.D2:
                    OnFileClick();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Вывод программы в консоль
        /// </summary>
        private static void OnConsoleClick()
        {
            do //Выполняем до выхода из программы
            {
                Console.Clear(); //Очищаем консоль
                ListPerson.GeneratePersonInConsole(); //Генерируем нового персона в консоль
                Console.WriteLine(ListPerson.TextOutput); //Выводим информаци. на экран

                Console.WriteLine("1 - Вписать нового участника\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }

        /// <summary>
        /// Вывод программы в файл
        /// </summary>
        private static void OnFileClick()
        {
            do //Выполняем до выхода из программы
            {
                Console.Clear(); //Очищаем консоль
                ListPerson.GeneratePersonInFile(); //Генерируем нового персона для файла

                var streamWriter =
                    new StreamWriter("db.txt", true); //Запускаем стримрайтер для записи\дозаписи\создания
                streamWriter.WriteLine(ListPerson.TextOutput); //Записываем в документ
                streamWriter.Close(); //Закрываем документ

                Console.WriteLine("1 - Вписать нового участника\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }
    }
}