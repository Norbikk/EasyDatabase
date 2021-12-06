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
            Notebook.AddPersonFromFileInList();
            Console.WriteLine(
                "1 - Вывести на экран человека по id.\n2 - Вписать данные в файл. \n3 - Выбрать диапазон дат. \n4 - Вывести список");
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    OutputFromFileDataInConsole();
                    break;
                case ConsoleKey.D2:
                    OnFileClick();
                    break;
                case ConsoleKey.D3:
                    ChosenDates();
                    break;
                case ConsoleKey.D4:
                    OutputPersons();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Выбранные даты
        /// </summary>
        private static void ChosenDates()
        {
            Console.WriteLine("Введите начальную дату");
            DateTime x = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите конечную дату");
            DateTime y = Convert.ToDateTime(Console.ReadLine());
            var s = Notebook.GetChosenDates(x, y);
            Console.WriteLine(s);
        }

        /// <summary>
        /// Основной метод вывода в консоль информации по ячейке
        /// </summary>
        private static void OutputFromFileDataInConsole()
        {
            Console.WriteLine("Введите айди который вывести");
            int id = Convert.ToInt32(Console.ReadLine()) - 1;
            string result = Notebook.ReadPersonsInFile(id);
            Console.WriteLine(result);


            Console.WriteLine("1-Редактировать данную ячейку\n2-Удалить данную ячейку\n3-Вывести таблицу");
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    Notebook.ChangePerson(id);
                    break;
                case ConsoleKey.D2:
                    AskRemoveOrNo(id);
                    break;
                case ConsoleKey.D3:
                    AskOutputInfo();
                    break;
            }

            Notebook.WriteInFile();
        }

        /// <summary>
        /// Вывод программы в файл
        /// </summary>
        private static void OnFileClick()
        {
            do //Выполняем до выхода из программы
            {
                Console.Clear(); //Очищаем консоль
                Notebook.GeneratePersonInFile(); //Генерируем нового персона для файла

                var streamWriter =
                    new StreamWriter("db.txt", true); //Запускаем стримрайтер для записи\дозаписи\создания
                streamWriter.WriteLine(Notebook.TextOutput); //Записываем в документ
                streamWriter.Close(); //Закрываем документ

                Console.WriteLine("1 - Вписать нового участника\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }


        /// <summary>
        /// Спрашивает удалить ли позицию
        /// </summary>
        /// <param name="id">номер позиции</param>
        private static void AskRemoveOrNo(int id)
        {
            Console.WriteLine("Удалить? Y/N");
            var consoleKeyInfo = Console.ReadKey().Key;
            if (consoleKeyInfo == ConsoleKey.Y)
            {
                Notebook.RemovePerson(id);
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
                Notebook.OutputListPerson();
            }
        }

        /// <summary>
        /// Выводит лист и сортирует по нажатию
        /// </summary>
        private static void OutputPersons()
        {
            Notebook.OutputListPerson();
            Console.WriteLine("1- Сортировать по возрастанию\n2-Сортировать по убыванию");
            var inputkey = Console.ReadKey().Key;
            switch (inputkey)
            {
                case ConsoleKey.D1:
                    Notebook.ListSorting();
                    break;
                case ConsoleKey.D2:
                    Notebook.ListSortingDescending();
                    break;
            }

            Notebook.WriteInFile();
        }
    }
}