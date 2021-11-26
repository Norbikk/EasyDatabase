using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Convert;

namespace Dictionary
{
    /// <summary>
    /// Класс персон, что имеет все данные, которые будут выводиться
    /// </summary>
    public class Person
    {
        private readonly string _name;
        private readonly int _age;
        private readonly int _height;
        private readonly string _birthday;
        private readonly string _placeOfBirth;
        private readonly DateTime _currentDate;

        /// <summary>
        /// Конструктор класса персон
        /// </summary>
        /// <param name="currentDate"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="height"></param>
        /// <param name="birthday"></param>
        /// <param name="placeofBirth"></param>
        public Person(DateTime currentDate, string name, int age, int height, string birthday, string placeofBirth)
        {
            _name = name;
            _age = age;
            _height = height;
            _birthday = birthday;
            _placeOfBirth = placeofBirth;
            _currentDate = currentDate;
        }

        /// <summary>
        /// Возвращает строку с информацией о созданном персоне
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"Дата добавления: {_currentDate} Имя: {_name} Возраст: {_age} Рост: {_height} Дата рождения: {_birthday} Город: {_placeOfBirth}";
    }

    /// <summary>
    /// Класс PersonData задает данные через ввод консоли, которые в дальнейшем в ListPerson Передаст в Person'a
    /// </summary>
    public class PersonData
    {
        public string Name;
        public int Age;
        public int Height;
        public string Birthday;
        public string PlaceOfBirth;
        public DateTime CurrentDate;
        private DateTime _birthdayDate;

        /// <summary>
        /// тут происходит ввод консоли
        /// </summary>
        public void PersonDataInput()
        {
            Name = Console.ReadLine();
            Height = ToInt32(Console.ReadLine());
            _birthdayDate = ToDateTime(Console.ReadLine());
            Birthday = _birthdayDate.ToShortDateString();
            PlaceOfBirth = Console.ReadLine();
            CurrentDate = DateTime.Now;
            Age = DateTime.Now.Year - _birthdayDate.Year;
        }
    }

    /// <summary>
    /// Класс Лист персон имеет лист и выполняет все вычисления для дальнейшейго вывода на экран
    /// </summary>
    public static class ListPerson
    {
        private static readonly List<Person> Members = new List<Person>(); //Инициализируем лист
        private static readonly PersonData PersonData = new PersonData(); //Инициализируем записную книжку
        public static string TextOutput; //Инициализируем вывод информации

        /// <summary>
        /// Генерируем данные для вывода в консоль
        /// </summary>
        public static void GeneratePersonInConsole()
        {
            PersonData.PersonDataInput(); //Вводим данные
            Members.Add(new Person(PersonData.CurrentDate, PersonData.Name, PersonData.Age, PersonData.Height,
                PersonData.Birthday, PersonData.PlaceOfBirth)); //Добавляем данные новому персону
            foreach (var mem in Members) //Проходимся по всем персонам в листе
            {
                var indexMember = Members.IndexOf(mem) + 1; //Задаем номер в таблице
                var memInfo = mem.ToString(); //присваиваем данные персона
                TextOutput = indexMember + " " + memInfo; //Присваиваем данные + номер
            }
        }

        /// <summary>
        /// Генерируем данные для вывода в файл
        /// </summary>
        public static void GeneratePersonInFile()
        {
            int indexMember; //Инициализируем порядковый номер
            if (File.Exists("db.txt") && File.ReadLines("db.txt").Any()) //Если файл найден и есть строки
                indexMember = int.Parse(File.ReadAllLines("db.txt").Last(x => true).Split(" ").First()) +
                              1; //читаем все строки в документе, находим последний, делим на пробелы, находим первый символ, добавляем +1
            else
                indexMember = 1; //В противном случае номер равен 1

            PersonData.PersonDataInput(); //Создаем персона
            var memInfo = new Person(PersonData.CurrentDate, PersonData.Name, PersonData.Age, PersonData.Height,
                PersonData.Birthday, PersonData.PlaceOfBirth).ToString(); //Присваиваем его данные меминфо
            TextOutput = indexMember + " " + memInfo; //Присваиваем данные + номер
        }
    }


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