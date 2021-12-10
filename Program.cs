using System;
using System.IO;

namespace Dictionary
{
    internal class Program
    {
        /// <summary>
        /// Входная точка программы
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Notebook.AddPersonsFromFile("db.txt");
            Console.WriteLine(
                "1 - Вывести на экран человека по ID.\n2 - Вписать данные в файл. \n3 - Выбрать диапазон дат. \n4 - Вывести список");
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    ConsoleWorker.OutputPersonByIdAndWork();
                    Notebook.WriteInFile("db.txt");
                    break;
                case ConsoleKey.D2:
                    ConsoleWorker.WriteFromConsoleInFile("db.txt");
                    break;
                case ConsoleKey.D3:
                    ConsoleWorker.ChosenDateSpanOutput();
                    break;
                case ConsoleKey.D4:
                    ConsoleWorker.OutputPersonsAndSorting();
                    Notebook.WriteInFile("db.txt");
                    break;
                default:
                    Console.WriteLine("Выбор только от 1-4");
                    return;
            }
        }
    }
}