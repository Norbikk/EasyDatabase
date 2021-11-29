using System;

namespace Dictionary
{
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
            Height = Convert.ToInt32(Console.ReadLine());
            _birthdayDate = Convert.ToDateTime(Console.ReadLine());
            Birthday = _birthdayDate.ToShortDateString();
            PlaceOfBirth = Console.ReadLine();
            CurrentDate = DateTime.Now;
            Age = DateTime.Now.Year - _birthdayDate.Year;
        }
    }
}