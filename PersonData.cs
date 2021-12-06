using System;

namespace Dictionary
{
    /// <summary>
    /// Класс PersonData задает данные через ввод консоли, которые в дальнейшем в ListPerson Передаст в Person'a
    /// </summary>
    public class PersonData
    {
        public int Id = 1;
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
            Console.WriteLine("Введите ФИО:");
            Name = Console.ReadLine();
            Console.WriteLine("Введите рост:");
            Height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите дату рождения:");
            _birthdayDate = Convert.ToDateTime(Console.ReadLine());
            Birthday = _birthdayDate.ToShortDateString();
            Console.WriteLine("Введите место рождения:");
            PlaceOfBirth = Console.ReadLine();
            CurrentDate = DateTime.Now;
            Age = DateTime.Now.Year - _birthdayDate.Year;
        }
    }
}