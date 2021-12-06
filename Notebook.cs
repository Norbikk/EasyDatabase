using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary
{
    /// <summary>
    /// Класс Лист персон имеет лист и выполняет все вычисления для дальнейшейго вывода на экран
    /// </summary>
    public static class Notebook
    {
        private static List<Person> _members = new List<Person>(); //Инициализируем лист
        private static readonly PersonData PersonData = new PersonData(); //Инициализируем записную книжку
        public static string TextOutput; //Инициализируем вывод информации

        /// <summary>
        /// Генерируем данные для вывода в файл
        /// </summary>
        public static void GeneratePersonInFile()
        {
            if (File.Exists("db.txt") && File.ReadLines("db.txt").Any()) //Если файл найден и есть строки
                PersonData.Id = int.Parse(File.ReadAllLines("db.txt").Last(x => true).Split("#").First()) +
                                1; //читаем все строки в документе, находим последний, делим на пробелы, находим первый символ, добавляем +1
            else
                PersonData.Id = 1; //В противном случае номер равен 1

            PersonData.PersonDataInput(); //Создаем персона
            var memInfo = new Person(PersonData.Id, PersonData.CurrentDate, PersonData.Name, PersonData.Age,
                PersonData.Height,
                PersonData.Birthday, PersonData.PlaceOfBirth).ToString(); //Присваиваем его данные меминфо
            TextOutput = memInfo; //Присваиваем данные + номер
        }

        /// <summary>
        /// Записывает полученный результат в файл
        /// </summary>
        public static void WriteInFile()
        {
            var streamWriter = new StreamWriter("db.txt");
            for (var i = 0; i < _members.Count; i++)
            {
                streamWriter.WriteLine(_members[i].ToString());
            }

            streamWriter.Close();
        }

        /// <summary>
        /// Выводит информацию нужного персона
        /// </summary>
        /// <param name="id">Вписываемый айди</param>
        public static string ReadPersonsInFile(int id)
        {
            return _members[id].ToString();
        }

        /// <summary>
        /// Выводит весь лист персонов, только не знаю как это сделать без вывода в консоль..
        /// </summary>
        public static void OutputListPerson()
        {
            for (var i = 0; i < _members.Count; i++)
            {
                Console.WriteLine(_members[i]);
            }
        }

        /// <summary>
        /// Удаляем выбранного персона из листа
        /// </summary>
        /// <param name="id">вписываемый айди</param>
        public static void RemovePerson(int id)
        {
            _members.RemoveAt(id);
            SetNewIdPersons();
        }

        /// <summary>
        /// Заменяем персона на нового
        /// </summary>
        /// <param name="id">Вписываемый айди</param>
        public static void ChangePerson(int id)
        {
            PersonData.PersonDataInput();
            _members[id] = new Person(id + 1, PersonData.CurrentDate, PersonData.Name, PersonData.Age,
                PersonData.Height, PersonData.Birthday, PersonData.PlaceOfBirth);
        }

        /// <summary>
        /// Выборка диапазона дат
        /// </summary>
        /// <param name="x">Начальная дата</param>
        /// <param name="y">Конечная дата</param>
        /// <returns></returns>
        public static string GetChosenDates(DateTime x, DateTime y)
        {
            string s = null;
            for (var i = 0; i < _members.Count; i++)
            {
                if (_members[i].CurrentDate >= x && _members[i].CurrentDate <= y)
                {
                    s += _members[i].ToString() + "\n";
                }
            }

            return s;
        }

        /// <summary>
        /// Сортировка по возрастанию
        /// </summary>
        public static void ListSorting()
        {
            _members = _members.OrderBy(x => x.CurrentDate).ToList();
            SetNewIdPersons();
            OutputListPerson();
        }

        /// <summary>
        /// Сортировка по убыванию
        /// </summary>
        public static void ListSortingDescending()
        {
            _members = _members.OrderByDescending(x => x.CurrentDate).ToList();
            SetNewIdPersons();
            OutputListPerson();
        }

        /// <summary>
        /// Выгружаем из файла в лист
        /// </summary>
        public static void AddPersonFromFileInList()
        {
            string[] lines = File.ReadAllLines("db.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split("#");

                PersonData.Id = Convert.ToInt32(data[0]);
                PersonData.CurrentDate = Convert.ToDateTime(data[1]);
                PersonData.Name = data[2];
                PersonData.Age = Convert.ToInt32(data[3]);
                PersonData.Height = Convert.ToInt32(data[4]);
                PersonData.Birthday = data[5];
                PersonData.PlaceOfBirth = data[6];


                _members.Add(new Person(PersonData.Id, PersonData.CurrentDate, PersonData.Name, PersonData.Age,
                    PersonData.Height, PersonData.Birthday, PersonData.PlaceOfBirth));
            }
        }

        /// <summary>
        /// Выдаем новые айдишники
        /// </summary>
        private static void SetNewIdPersons()
        {
            for (var i = 0; i < _members.Count; i++)
            {
                _members[i].Id = i + 1;
            }
        }
    }
}