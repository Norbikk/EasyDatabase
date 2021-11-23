using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Convert;

namespace Dictionary
{
    public class Person
    {
        private string _name;
        private int _ages;
        private int _height;
        private string _birthday;
        private string _placeOfBirth;
        private DateTime _dateTime;

        public static Person GenerateFromInput() //Создает все данные Person'a
        {
            var person = new Person();

            Console.WriteLine("Введите ФИО: ");
            person._name = Console.ReadLine();          //Ввод на консоли задается в нейм
            Console.WriteLine("Введите рост: ");
            person._height = ToInt32(Console.ReadLine());  //Ввод на консоли задается в рост
            Console.WriteLine("Введите дату рождения: ");
            var birhdayDate = ToDateTime(Console.ReadLine());  //ввод на консоли задается в дату
            person._birthday = birhdayDate.ToShortDateString();   //Присваиваем дату дате рождения
            Console.WriteLine("Введите место рождения: ");
            person._placeOfBirth = Console.ReadLine();         //Ввод места рождения

            var birthdayDateInYear = birhdayDate.Year;      //присваеваем новой переменной год рождения
            var yearNow = DateTime.Now.Year;                //Создаем переменную с годом нынешним
            person._ages = yearNow - birthdayDateInYear;      //Считаем возраст с помощью вычитания года рождения из текущего
            person._dateTime = DateTime.Now;                  //Присваиваем время создания Person'a
            return person;                                    //Возвращаем данные Person'a
        }

        public string PrintInConsole() // Возвращает данные
        {
            return
                $"Дата добавления: {_dateTime} Имя: {_name} Возраст: {_ages} Рост: {_height} Дата рождения: {_birthday} Город: {_placeOfBirth}";
        }
    }

    internal class Program
    {
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
            }
        }

        private static void OnConsoleClick()
        {
            var members = new List<Person>(); //Создаем лист
            do //Выполняем до выхода из программы
            {
                Console.Clear();  
                var person = Person.GenerateFromInput(); //Создаем персона
                members.Add(person); //Добавляем в лист
                foreach (var mem in members) //Проходимся по всем персонам в листе
                {
                    var indexMember = members.IndexOf(mem) + 1; //Задаем номер в таблице
                    var memInfo = mem.PrintInConsole(); //присваиваем данные персона
                    var stringer = indexMember + " " + memInfo; //Присваиваем данные + номер
                    Console.WriteLine(stringer); //Выводим
                }

                Console.WriteLine("1 - Вписать нового участника\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }

        private static void OnFileClick()
        {
            var members = new List<Person>(); //Создаем лист

            do //Выполняем до выхода
            {
                Console.Clear();  
                int indexMember; //Инициализируем порядковый номер
                if (File.Exists("db.txt") && File.ReadLines("db.txt").Any()) //Если файл найден и есть строки
                    indexMember = int.Parse(File.ReadAllLines("db.txt").Last(x => true).Split(" ").First()) +
                                  1; //читаем все строки в документе, находим последний, делим на пробелы, находим первый символ, добавляем +1
                else
                    indexMember = 1; //В противном случае номер равен 1
                var streamWriter =
                    new StreamWriter("db.txt", true); //Запускаем стримрайтер для записи\дозаписи\создания
                var person = Person.GenerateFromInput(); //Создаем персона
                members.Add(person); //Добавляем его в список
                var memInfo = person.PrintInConsole(); //Присваиваем его данные меминфо
                var stringer = indexMember + " " + memInfo; //Присваиваем данные + номер
                streamWriter.WriteLine(stringer); //Записываем в документ
                streamWriter.Close(); //Закрываем документ

                Console.WriteLine("1 - Вписать нового участника\nДля выхода нажмите любую другую кнопку");
            } while (Console.ReadKey().Key == ConsoleKey.D1);
        }
    }
}