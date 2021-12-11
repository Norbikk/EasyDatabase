using System;
using System.IO;
using System.Linq;

namespace Dictionary
{
    public class ConsoleWorker
    {
        private static readonly Notebook Notebook = new();
        private const string Separator = "#";

        /// <summary>
        /// Ввод нового человека с консоли
        /// </summary>
        /// <returns>Возвращает сгенерированного персона</returns>
        internal PersonData PersonDataInput()
        {
            var person = new PersonData();
            Console.WriteLine("Введите ФИО:");
            person.Name = Console.ReadLine();
            Console.WriteLine("Введите рост:");
            person.Height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите дату рождения(формат дд.мм.гггг):");
            DateTime birthdayDate = Convert.ToDateTime(Console.ReadLine());
            person.Birthday = birthdayDate.ToShortDateString();
            Console.WriteLine("Введите место рождения:");
            person.PlaceOfBirth = Console.ReadLine();
            person.CurrentDate = DateTime.Now;
            person.Age = person.CurrentDate.Year - birthdayDate.Year;

            return person;
        }

        /// <summary>
        /// Вывод программы в файл
        /// </summary>
        internal void WriteFromConsoleInFile(string path)
        {
            do //Выполняем до выхода из программы
            {
                Console.Clear(); //Очищаем консоль
                var s = GeneratePersons(path); //Генерируем нового персона для файла

                var streamWriter =
                    new StreamWriter(path, true); //Запускаем стримрайтер для записи\дозаписи\создания
                streamWriter.WriteLine(s); //Записываем в документ
                streamWriter.Close(); //Закрываем документ

                Console.WriteLine("1 - Вписать нового участника\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }


        /// <summary>
        /// Выводит человека и предлагает дальнейшие действия
        /// </summary>
        internal void OutputPersonByIdAndWork()
        {
            Console.WriteLine("Введите ID который вывести");
            int id = Convert.ToInt32(Console.ReadLine()) - 1;
            var s = OutputPersonById(id);
            Console.WriteLine(s);
            AskWork(id);
        }

        /// <summary>
        ///Выводит людей в выбранном диапазоне
        /// </summary>
        internal void ChosenDateSpanOutput()
        {
            var dates = ChosenDateSpanByInput();
            Console.WriteLine(dates);
        }


        /// <summary>
        /// Выводит лист и предлагает сортировку
        /// </summary>
        internal void OutputPersonsAndSorting()
        {
            var persons = OutputPersons();
            Console.WriteLine(persons);
            var sort = AskAboutSorting();
            Console.WriteLine(sort);
        }

        /// <summary>
        /// Генерирует данные из консоли для дальнейшего использования
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>Возвращает Персона в строку</returns>
        private string GeneratePersons(string path)
        {
            var person = PersonDataInput();
            if (File.Exists(path) && File.ReadLines(path).Any()) //Если файл найден и есть строки
                person.Id = int.Parse(File.ReadAllLines(path).Last(x => true).Split(Separator).First()) +
                            1; //читаем все строки в документе, находим последний, делим на пробелы, находим первый символ, добавляем +1
            else
                person.Id = 1; //В противном случае номер равен 1

            var memInfo = new Person(person); //Присваиваем его данные меминфо
            return memInfo.ToString(); //Присваиваем данные + номер
        }

        /// <summary>
        /// Выбранные даты
        /// </summary>
        private string ChosenDateSpanByInput()
        {
            var (startDate, endDate) = InputDates();
            var dates = Notebook.GetChosenDates(startDate, endDate);
            return dates;
        }

        /// <summary>
        /// При вводе в консоль получает даты
        /// </summary>
        /// <returns>Возвращает начальную и конечную дату</returns>
        private (DateTime startTime, DateTime endTime) InputDates()
        {
            Console.WriteLine("Введите начальную дату(формат дд.мм.гггг)");
            var startDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите конечную дату(формат дд.мм.гггг)");
            var endDate = Convert.ToDateTime(Console.ReadLine());

            return (startDate, endDate);
        }


        /// <summary>
        /// метод вывода в консоль информации по человеку
        /// </summary>
        private string OutputPersonById(int id)
        {
            string result = Notebook.OutputPersonInfo(id);

            return result;
        }

        /// <summary>
        /// Возвращает лист лист в строку
        /// </summary>
        private string OutputPersons()
        {
            var output = Notebook.OutputListPerson();
            return output;
        }

        /// <summary>
        /// Выполняет сортировку по нажатию и возвращает в строку отсортированный лист
        /// </summary>
        private string AskAboutSorting()
        {
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
                default:
                    Console.WriteLine("Ввод только 1 или 2. Список не отсортирован.");
                    break;
            }


            var output = Notebook.OutputListPerson();
            return output;
        }

        /// <summary>
        /// Выполняет предложенные действия по нажатию
        /// </summary>
        /// <param name="id">Вписываемый айди</param>
        private void AskWork(int id)
        {
            Console.WriteLine("1-Редактировать данного человека\n2-Удалить данного человека\n3-Вывести таблицу");
            var input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    Notebook.ChangePerson(id);
                    break;
                case ConsoleKey.D2:
                    AskRemoveOrNot(id);
                    AskOutputInfo();
                    break;
                case ConsoleKey.D3:
                    AskOutputInfo();
                    break;
                default:
                    Console.WriteLine("Ввод только от 1 до 3");
                    break;
            }
        }

        /// <summary>
        /// Спрашивает удалить ли позицию
        /// </summary>
        /// <param name="id">номер позиции</param>
        private void AskRemoveOrNot(int id)
        {
            Console.WriteLine("Удалить? Y/N");
            var consoleKeyInfo = Console.ReadKey().Key;
            if (consoleKeyInfo == ConsoleKey.Y)
            {
                Notebook.RemoveAndSetId(id);
                Console.WriteLine("\nДанный человек удален из списка");
            }
        }

        /// <summary>
        /// Спрашивает вывести ли список
        /// </summary>
        private void AskOutputInfo()
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