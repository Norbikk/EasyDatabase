using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary
{
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
}