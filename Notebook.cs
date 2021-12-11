using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary
{
    public class Notebook
    {
        private static List<Person> _members = new List<Person>(); //Инициализируем лист
        private static ConsoleWorker _consoleWorker = new();
        private const string Separator = "#";

        /// <summary>
        /// Записывает полученный результат в файл
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void WriteInFile(string path)
        {
            var streamWriter = new StreamWriter(path);
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
        public string OutputPersonInfo(int id)
        {
            return _members[id].ToString();
        }

        /// <summary>
        /// Выводит весь лист персонов
        /// </summary>
        public string OutputListPerson()
        {
            string s = null;
            for (var i = 0; i < _members.Count; i++)
            {
                s += _members[i] + "\n";
            }

            return s;
        }

        /// <summary>
        /// Заменяем персона на нового
        /// </summary>
        /// <param name="id">Вписываемый айди</param>
        public void ChangePerson(int id)
        {
            var person = _consoleWorker.PersonDataInput();
            person.Id = id + 1;
            _members[id] = new Person(person);
        }

        /// <summary>
        /// Удаляет человека и перенумировывает
        /// </summary>
        /// <param name="id">вписываемый айди</param>
        public void RemoveAndSetId(int id)
        {
            RemovePerson(id);
            SetNewIdPersons();
        }

        /// <summary>
        /// Выборка диапазона дат
        /// </summary>
        /// <param name="startTime">Начальная дата</param>
        /// <param name="endTime">Конечная дата</param>
        /// <returns>Возвращает строку листа человек в выбранном диапазоне дат</returns>
        public string GetChosenDates(DateTime startTime, DateTime endTime)
        {
            string s = null;
            for (var i = 0; i < _members.Count; i++)
            {
                if (_members[i].CurrentDate >= startTime && _members[i].CurrentDate <= endTime)
                {
                    s += _members[i] + "\n";
                }
            }

            return s;
        }

        /// <summary>
        /// Выгружаем из файла в лист
        /// </summary>
        public void AddPersonsFromFile(string path)
        {
            var person = new PersonData();
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(Separator);

                person.Id = Convert.ToInt32(data[0]);
                person.CurrentDate = Convert.ToDateTime(data[1]);
                person.Name = data[2];
                person.Age = Convert.ToInt32(data[3]);
                person.Height = Convert.ToInt32(data[4]);
                person.Birthday = data[5];
                person.PlaceOfBirth = data[6];

                _members.Add(new Person(person));
            }
        }

        /// <summary>
        /// Берет отсортированный по возрастанию лист с новыми ID
        /// </summary>
        public void GetSortedList()
        {
            ListSorting();
            SetNewIdPersons();
        }

        /// <summary>
        /// Берет отсортированный по убыванию лист с новыми ID
        /// </summary>
        public void GetSortedListDescend()
        {
            ListSortingDescending();
            SetNewIdPersons();
        }


        /// <summary>
        /// Удаляет выбранного человека из листа
        /// </summary>
        /// <param name="id">вписываемый айди</param>
        private void RemovePerson(int id)
        {
            _members.RemoveAt(id);
        }

        /// <summary>
        /// Сортировка по возрастанию
        /// </summary>
        private void ListSorting()
        {
            _members = _members.OrderBy(x => x.CurrentDate).ToList();
        }

        /// <summary>
        /// Сортировка по убыванию
        /// </summary>
        private void ListSortingDescending()
        {
            _members = _members.OrderByDescending(x => x.CurrentDate).ToList();
        }

        /// <summary>
        /// Выдает новые ID
        /// </summary>
        private void SetNewIdPersons()
        {
            for (var i = 0; i < _members.Count; i++)
            {
                _members[i].Id = i + 1;
            }
        }
    }
}