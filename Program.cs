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
                    OutputPersonById();
                    Notebook.WriteInFile("db.txt");
                    break;
                case ConsoleKey.D2:
                    WriteFromConsoleInFile("db.txt");
                    break;
                case ConsoleKey.D3:
                    ChooseDateSpan();
                    break;
                case ConsoleKey.D4:
                    OutputPersons();
                    Notebook.WriteInFile("db.txt");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
        }

        /// <summary>
        /// Вывод программы в файл
        /// </summary>
        private static void WriteFromConsoleInFile(string path)
        {
            do //Выполняем до выхода из программы
            {
                Console.Clear(); //Очищаем консоль
                var s = Notebook.GeneratePersons(path); //Генерируем нового персона для файла

                var streamWriter =
                    new StreamWriter(path, true); //Запускаем стримрайтер для записи\дозаписи\создания
                streamWriter.WriteLine(s); //Записываем в документ
                streamWriter.Close(); //Закрываем документ

                Console.WriteLine("1 - Вписать нового участника\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }

        /// <summary>
        /// Основной метод вывода в консоль информации по ячейке
        /// </summary>
        private static void OutputPersonById()
        {
            Console.WriteLine("Введите ID который вывести");
            int id = Convert.ToInt32(Console.ReadLine()) - 1;
            string result = Notebook.OutputPersonInfo(id);
            Console.WriteLine(result);


            Console.WriteLine("1-Редактировать данного человека\n2-Удалить данного человека\n3-Вывести таблицу");
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    Notebook.ChangePerson(id);
                    break;
                case ConsoleKey.D2:
                    AskRemoveOrNot(id);
                    break;
                case ConsoleKey.D3:
                    AskOutputInfo();
                    break;
            }
        }

        /// <summary>
        /// Выбранные даты
        /// </summary>
        private static void ChooseDateSpan()
        {
            Console.WriteLine("Введите начальную дату");
            DateTime startTime = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите конечную дату");
            DateTime endTime = Convert.ToDateTime(Console.ReadLine());
            var s = Notebook.GetChosenDates(startTime, endTime);
            Console.WriteLine(s);
        }

        /// <summary>
        /// Выводит лист и сортирует по нажатию
        /// </summary>
        private static void OutputPersons()
        {
            var s = Notebook.OutputListPerson();
            Console.WriteLine(s);
            Console.WriteLine("1- Сортировать по возрастанию\n2-Сортировать по убыванию");
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    Notebook.GetSortedList();
                    break;
                case ConsoleKey.D2:
                    Notebook.GetSortedListDescend();
                    break;
            }

            Console.WriteLine(s);
        }


        /// <summary>
        /// Спрашивает удалить ли позицию
        /// </summary>
        /// <param name="id">номер позиции</param>
        private static void AskRemoveOrNot(int id)
        {
            Console.WriteLine("Удалить? Y/N");
            var consoleKeyInfo = Console.ReadKey().Key;
            if (consoleKeyInfo == ConsoleKey.Y)
            {
                Notebook.RemoveAndSetID(id);
                Console.WriteLine("\nДанный человек удален из списка");
            }

            AskOutputInfo();
        }

        /// <summary>
        /// Спрашивает вывести ли список
        /// </summary>
        private static void AskOutputInfo()
        {
            Console.WriteLine("Вывести список? Y/N");
            var consoleKeyInfo = Console.ReadKey().Key;
            if (consoleKeyInfo == ConsoleKey.Y)
            {
                var s = Notebook.OutputListPerson();
                Console.WriteLine(s);
            }
        }
    }
}