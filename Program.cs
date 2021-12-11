using System;

namespace Dictionary
{
    internal class Program
    {
        private static Notebook _notebook = new();
        private static ConsoleWorker _consoleWorker = new();

        /// <summary>
        /// Входная точка программы
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            _notebook.AddPersonsFromFile("db.txt");
            do
            {
                Console.WriteLine(
                    "\n1 - Вывести на экран человека по ID.\n2 - Вписать данные в файл. \n3 - Выбрать диапазон дат. \n4 - Вывести список");

                var input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.D1:
                        _consoleWorker.OutputPersonByIdAndWork();
                        _notebook.WriteInFile("db.txt");
                        break;
                    case ConsoleKey.D2:
                        _consoleWorker.WriteFromConsoleInFile("db.txt");
                        break;
                    case ConsoleKey.D3:
                        _consoleWorker.ChosenDateSpanOutput();
                        break;
                    case ConsoleKey.D4:
                        _consoleWorker.OutputPersonsAndSorting();
                        _notebook.WriteInFile("db.txt");
                        break;
                    default:
                        Console.WriteLine("Выбор от 1 до 4");
                        break;
                }

                Console.WriteLine("1 - Продолжить работу программы\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }
    }
}